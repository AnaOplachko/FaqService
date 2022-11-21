using FaqService.Application.Models;
using FaqService.ComponentTests.ExternalEnvironment;
using FaqService.ComponentTests.Helpers;
using FaqService.ComponentTests.Hooks.Common;
using TechTalk.SpecFlow;

namespace FaqService.ComponentTests.Steps.AdminTestsSteps;

[Binding]
public class When : Common
{
    [When(
        @"Администратор добавляет новую категорию с именем ""(.*)"" и идентификатором родительской категории ""(.*)""")]
    public async Task WhenАдминистраторДобавляетНовуюКатегориюСИменемИИдентификаторомРодительскойКатегории(string name,
        int? parentId)
    {
        Section = new SectionModel { Name = name, ParentId = parentId };

        var request = new RequestMessageBuilder().WithCreateSectionCommand(name, parentId).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор запрашивает все категории")]
    public async Task WhenАдминистраторЗапрашиваетВсеКатегории()
    {
        var request = new RequestMessageBuilder().WithAdminGetAllSectionsQuery().Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор обновляет категорию с Id ""(.*)"" имя ""(.*)"" и Id родительской категории ""(.*)""")]
    public async Task WhenАдминистраторОбновляетКатегориюСIdИмяИIdРодительскойКатегории(int id, string name,
        int? parentId)
    {
        Section = new SectionModel { Id = id, Name = name, ParentId = parentId };

        var request = new RequestMessageBuilder().WithUpdateSectionCommand(id, name, parentId).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор обновляет категорию на корректное имя ""(.*)"" и существуещего родителя")]
    public async Task WhenАдминистраторОбновляетКатегориюНаКорректноеИмяИСуществуещегоРодителя(string name)
    {
        var id = Section!.Id;
        var newParent = SectionsWithSubs
            .Where(x => x.ParentId is null)
            .FirstOrDefault(x => x.Id != Section.ParentId);

        Section = new SectionModel { Id = id, Name = name, ParentId = newParent!.Id };

        var request = new RequestMessageBuilder().WithUpdateSectionCommand(id, name, newParent.Id).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор обновляет категорию с некорректным идентификатором")]
    public async Task WhenАдминистраторОбновляетКатегориюСНекорректнымИдентификатором()
    {
        var id = Int32.MaxValue;
        var name = "new name";

        var request = new RequestMessageBuilder().WithUpdateSectionCommand(id, name, null).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор обновляет категорию с некорректным идентификатором родителя")]
    public async Task WhenАдминистраторОбновляетКатегориюСНекорректнымИдентификаторомРодителя()
    {
        var id = Section!.Id;
        var name = "new name";
        var parentId = int.MaxValue;

        var request = new RequestMessageBuilder().WithUpdateSectionCommand(id, name, parentId).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор обновляет категорию нарушая вложенность подкатегорий")]
    public async Task WhenАдминистраторОбновляетКатегориюНарушаяВложенностьПодкатегорий()
    {
        var sectionToUpdate = SectionsWithSubs
            .FirstOrDefault(section => section.Subsections.Count != 0);
        var newParent = SectionsWithSubs
            .Where(section => section.ParentId == null)
            .FirstOrDefault(section => section.Id != sectionToUpdate!.Id);
        var name = "new name";

        var request = new RequestMessageBuilder()
            .WithUpdateSectionCommand(sectionToUpdate!.Id, name, newParent!.Id).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор запрашивает категорию по id")]
    public async Task WhenАдминистраторЗапрашиваетКатегориюПоId()
    {
        var request = new RequestMessageBuilder().WithAdminGetSectionByIdQuery(Section!.Id).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор запрашивает категорию с некорректным id")]
    public async Task WhenАдминистраторЗапрашиваетКатегориюСНекорректнымId()
    {
        var incorrectId = Int32.MaxValue;

        var request = new RequestMessageBuilder().WithAdminGetSectionByIdQuery(incorrectId).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор удаляет категорию")]
    public async Task WhenАдминистраторУдаляетКатегорию()
    {
        var request = new RequestMessageBuilder().WithDeleteSectionByIdCommand(Section!.Id).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор удаляет категорию с некорректным идентификатором")]
    public async Task WhenАдминистраторУдаляетКатегориюСНекорректнымИдентификатором()
    {
        var incorrectId = int.MaxValue;

        var request = new RequestMessageBuilder().WithDeleteSectionByIdCommand(incorrectId).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор удаляет корневую категорию")]
    public async Task WhenАдминистраторУдаляетКорневуюКатегорию()
    {
        var rootSection = SectionsWithSubs.FirstOrDefault(x => x.ParentId is null);
        
        var request = new RequestMessageBuilder().WithDeleteSectionByIdCommand(rootSection!.Id).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор добавляет статью с корректными данными")]
    public async Task WhenАдминистраторДобавляетСтатьюСКорректнымиДанными()
    {
        var question = "valid question";
        var answer = "valid answer";
        var parentId = Section!.Id;
        
        Article = new ArticleModel { Question = question, Answer = answer, ParentId = parentId };

        var request = new RequestMessageBuilder().WithCreateArticleCommand(question, answer, parentId).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор добавляет статью с некорректным идентификатором родителя")]
    public async Task WhenАдминистраторДобавляетСтатьюСНекорректнымИдентификаторомРодителя()
    {
        var question = "valid question";
        var answer = "valid answer";
        var parentId = Int32.MaxValue;

        var request = new RequestMessageBuilder().WithCreateArticleCommand(question, answer, parentId).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор запрашивает все статьи")]
    public async Task WhenАдминистраторЗапрашиваетВсеСтатьи()
    {
        var request = new RequestMessageBuilder().WithAdminGetAllArticlesQuery().Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор запрашивает статью по идентификатору")]
    public async Task WhenАдминистраторЗапрашиваетСтатьюПоИдентификатору()
    {
        var request = new RequestMessageBuilder().WithAdminGetArticleByIdQuery(Article.Id).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор запрашивает статью с некорректным идентификатором")]
    public async Task WhenАдминистраторЗапрашиваетСтатьюСНекорректнымИдентификатором()
    {
        var incorrectId = int.MaxValue;
        
        var request = new RequestMessageBuilder().WithAdminGetArticleByIdQuery(incorrectId).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор удаляет статью по идентификатору")]
    public async Task WhenАдминистраторУдаляетСтатьюПоИдентификатору()
    {
        var request = new RequestMessageBuilder().WithDeleteArticleByIdCommand(Article.Id).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор удаляет статью с некорректным идентификатором")]
    public async Task WhenАдминистраторУдаляетСтатьюСНекорректнымИдентификатором()
    {
        var incorrectId = int.MaxValue;
        
        var request = new RequestMessageBuilder().WithDeleteArticleByIdCommand(incorrectId).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор обновляет статью с корректными данными")]
    public async Task WhenАдминистраторОбновляетСтатьюСКорректнымиДанными()
    {
        var id = Article.Id;
        var question = "valid question";
        var answer = "valid answer";
        var newParent = SectionsWithSubs.Where(section => section.ParentId != null)
            .FirstOrDefault(section => section.Id != Article.ParentId);
        
        Article = new ArticleModel
            { Id = id, Question = question, Answer = answer, ParentId = newParent!.Id, OrderPosition = null };

        var request =
            new RequestMessageBuilder().WithUpdateArticleCommand(id, question, answer, newParent.Id, null).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор обновляет статью с некорректным идентификатором")]
    public async Task WhenАдминистраторОбновляетСтатьюСНекорректнымИдентификатором()
    {
        var id = Int32.MaxValue;
        var question = "valid question";
        var answer = "valid answer";
        var newParent = SectionsWithSubs.Where(section => section.ParentId != null)
            .FirstOrDefault(section => section.Id != Article.ParentId);

        var request =
            new RequestMessageBuilder().WithUpdateArticleCommand(id, question, answer, newParent!.Id, null).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор обновляет статью устанавливая идентификатор несуществующего родителя")]
    public async Task WhenАдминистраторОбновляетСтатьюУстанавливаяИдентификаторНесуществующегоРодителя()
    {
        var id = Article.Id;
        var question = "valid question";
        var answer = "valid answer";
        var newParentId = int.MaxValue;

        var request = 
            new RequestMessageBuilder().WithUpdateArticleCommand(id, question, answer, newParentId, null).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор обновляет статью устанавливая родителем корневую категорию")]
    public async Task WhenАдминистраторОбновляетСтатьюУстанавливаяРодителемКорневуюКатегорию()
    {
        var id = Article.Id;
        var question = "valid question";
        var answer = "valid answer";
        var newParent = SectionsWithSubs.FirstOrDefault(section => section.ParentId == null);

        var request = 
            new RequestMessageBuilder().WithUpdateArticleCommand(id, question, answer, newParent!.Id, null).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор запрашивает статьи по слову (.*) на странице (.*) с размером страницы (.*)")]
    public async Task WhenАдминистраторЗапрашиваетСтатьиПоСловуНаСтраницеСРазмеромСтраницы(string searchQuery, int page,
        int pageSize)
    {
        var request = new RequestMessageBuilder().WithAdminSearchArticleQuery(searchQuery, page, pageSize).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор запрашивает статьи по запросу ""(.*)"" на странице (.*) с размером страницы (.*)")]
    public async Task WhenАдминистраторЗапрашиваетСтатьиПоЗапросуНаСтраницеСРазмеромСтраницы(string searchQuery,
        int page, int pageSize)
    {
        var request = new RequestMessageBuilder().WithAdminSearchArticleQuery(searchQuery, page, pageSize).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }
    
    [When(@"Администратор добавляет новую статью с вопросом ""(.*)"" ответом ""(.*)"" в категорию с именем ""(.*)"" и позицией ""(.*)""")]
    public async Task WhenАдминистраторДобавляетНовуюСтатьюСВопросомОтветомВКатегориюСИменемИПозицией(string question, 
        string answer, string parentSectionName, int? orderPosition)
    {
        var parent = SectionsWithSubs.FirstOrDefault(section => section.Name == parentSectionName);
        
        var request = new RequestMessageBuilder().WithCreateArticleCommand(question, answer, parent!.Id, orderPosition)
            .Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);

        Article = HttpResponseMessage.Content.ReadAs<ArticleModel>()!;
        Articles.Add(Article);
    }

    [When(@"Администратор запрашивает список отсортированных статей для категории с именем ""(.*)""")]
    public async Task WhenАдминистраторЗапрашиваетСписокОтсортированныхСтатейДляКатегорииСИменем(string parentSectionName)
    {
        var parent = SectionsWithSubs.FirstOrDefault(section => section.Name == parentSectionName);

        Section = new SectionModel {Id = parent!.Id, Name = parent.Name, ParentId = parent.ParentId};
        
        var request = new RequestMessageBuilder().WithAdminSortedArticlesQuery(parent!.Id).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }
    
    [When(@"Администратор удаляет статью с вопросом ""(.*)""")]
    public async Task WhenАдминистраторУдаляетСтатьюСВопросом(string question)
    {
        var article = Articles.FirstOrDefault(article => article!.Question == question);
        
        var request = new RequestMessageBuilder().WithDeleteArticleByIdCommand(article!.Id).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);

        Articles.Remove(article);
    }
    
    [When(@"Администратор обновляет статью с вопросом ""(.*)"" устанавливая вопрос ""(.*)"" ответ ""(.*)"" родителя с именем ""(.*)"" и позицию ""(.*)""")]
    public async Task WhenАдминистраторОбновляетСтатьюСВопросомУстанавливаяВопросОтветРодителяСИменемИПозицию(string oldQuestion, 
        string newQuestion, string newAnswer, string newParentName, int? orderPosition)
    {
        var article = Articles.FirstOrDefault(article => article!.Question == oldQuestion);
        var newParent = SectionsWithSubs.FirstOrDefault(section => section.Name == newParentName);

        var request =
            new RequestMessageBuilder().WithUpdateArticleCommand(article!.Id, newQuestion, newAnswer, newParent!.Id,
                orderPosition).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);

        Article = HttpResponseMessage.Content.ReadAs<ArticleModel>()!;
    }

    [When(@"Администратор добавляет новый тэг с названием ""(.*)""")]
    public async Task WhenАдминистраторДобавляетНовыйТэгСНазванием(string name)
    {
        Tag = new TagModel { Name = name };

        var request = new RequestMessageBuilder().WithCreateTagCommand(name).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор запрашивает все тэги")]
    public async Task WhenАдминистраторЗапрашиваетВсеТэги()
    {
        var request = new RequestMessageBuilder().WithAdminGetAllTagsQuery().Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор запрашивает тэг по идентификатору")]
    public async Task WhenАдминистраторЗапрашиваетТэгПоИдентификатору()
    {
        var request = new RequestMessageBuilder().WithAdminGetTagByIdQuery(Tag.Id).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор запрашивает тэг с некорректным идентификатором")]
    public async Task WhenАдминистраторЗапрашиваетТэгСНекорректнымИдентификатором()
    {
        var incorrectId = int.MaxValue;
        
        var request = new RequestMessageBuilder().WithAdminGetTagByIdQuery(incorrectId).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор обновляет тэг устанавливая имя ""(.*)""")]
    public async Task WhenАдминистраторОбновляетТэгУстанавливаяИмя(string name)
    {
        Tag = new TagModel { Id = Tag.Id, Name = name };

        var request = new RequestMessageBuilder().WithUpdateTagCommand(Tag.Id, name).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор удаляет тэг с корректным идентификатором")]
    public async Task WhenАдминистраторУдаляетТэгСКорректнымИдентификатором()
    {
        var request = new RequestMessageBuilder().WithDeleteTagByIdCommand(Tag.Id).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор удаляет тэг с некорректным идентификатором")]
    public async Task WhenАдминистраторУдаляетТэгСНекорректнымИдентификатором()
    {
        var incorrectId = int.MaxValue;
        
        var request = new RequestMessageBuilder().WithDeleteTagByIdCommand(incorrectId).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор устанавливает тэги с некорректными идентификаторами")]
    public async Task WhenАдминистраторУстанавливаетТэгиСНекорректнымиИдентификаторами()
    {
        var tags = new List<int> { int.MaxValue };

        var request = new RequestMessageBuilder().WithSetTagsCommand(Article.Id, tags).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Администратор устанавливает тэги с названиями ""(.*)""")]
    public async Task WhenАдминистраторУстанавливаетТэгиСНазваниями(string tagNamesAsString)
    {
        var tags = tagNamesAsString.Split(',', StringSplitOptions.TrimEntries)
            .Select(name => Tags.FirstOrDefault(tag => tag!.Name == name))
            .Select(tag => tag!.Id)
            .ToList();
        
        var request = new RequestMessageBuilder().WithSetTagsCommand(Article.Id, tags).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }
}