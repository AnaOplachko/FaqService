using System.Net;
using System.Text;
using FaqService.Application.Models;
using FaqService.ComponentTests.Helpers;
using FaqService.ComponentTests.Hooks.Common;
using FluentAssertions;
using RabbitMQ.Client;
using TechTalk.SpecFlow;

namespace FaqService.ComponentTests.Steps.AdminTestsSteps;

[Binding]
public class Then : Common
{
    [Then(@"Категория успешно добавлена")]
    public void ThenКатегорияУспешноДобавлена()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        var sectionFromServer = HttpResponseMessage.Content.ReadAs<SectionModel>();
        sectionFromServer!.Name.Should().Be(Section!.Name);
        sectionFromServer.ParentId.Should().Be(Section.ParentId);
    }

    [Then(@"Администратор получает все категории")]
    public void ThenАдминистраторПолучаетВсеКатегории()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        var sections = HttpResponseMessage.Content.ReadAs<List<SectionWithSubs>>();
        sections.Should().BeEquivalentTo(SectionsWithSubs);
    }

    [Then(@"Категория успешно обновлена")]
    public void ThenКатегорияУспешноОбновлена()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        var sectionFromServer = HttpResponseMessage.Content.ReadAs<SectionModel>();
        sectionFromServer.Should().BeEquivalentTo(Section);
    }

    [Then(@"Получено сообщение об ошибке NotFound")]
    public void ThenПолученоСообщениеОбОшибкеNotFound()
    {
        var content = HttpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then(@"Администратор получает категорию с id")]
    public void ThenАдминистраторПолучаетКатегориюСId()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        var sectionFromServer = HttpResponseMessage.Content.ReadAs<SectionWithSubs>();
        sectionFromServer.Should().BeEquivalentTo(Section);
    }

    [Then(@"Категория успешно удалена")]
    public void ThenКатегорияУспешноУдалена()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Then(@"Статья успешно создана")]
    public void ThenСтатьяУспешноСоздана()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        var articleFromServer = HttpResponseMessage.Content.ReadAs<ArticleModel>();
        articleFromServer!.Question.Should().Be(Article.Question);
        articleFromServer.Answer.Should().Be(Article.Answer);
        articleFromServer.ParentId.Should().Be(Article.ParentId);
        articleFromServer.OrderPosition.Should().Be(Article.OrderPosition);
    }

    [Then(@"Администратор получает все статьи")]
    public void ThenАдминистраторПолучаетВсеСтатьи()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var articles = HttpResponseMessage.Content.ReadAs<List<ArticleModel>>();
        articles.Should().BeEquivalentTo(Articles);
    }

    [Then(@"Администратор получает статью с верным идентификатором")]
    public void ThenАдминистраторПолучаетСтатьюСВернымИдентификатором()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var articleFromServer = HttpResponseMessage.Content.ReadAs<ArticleModel>();
        articleFromServer.Should().BeEquivalentTo(Article);
    }

    [Then(@"Статья успешно удалена")]
    public void ThenСтатьяУспешноУдалена()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Then(@"Статья успешно обновлена")]
    public void ThenСтатьяУспешноОбновлена()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        var articleFromServer = HttpResponseMessage.Content.ReadAs<ArticleModel>();
        articleFromServer.Should().BeEquivalentTo(Article);
    }

    [Then(@"Получено сообщение об ошибке BadRequest")]
    public void ThenПолученоСообщениеОбОшибкеBadRequest()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Then(@"Администратор получает статьи в количестве (.*)")]
    public void ThenАдминистраторПолучаетСтатьиВКоличестве(int count)
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var result = HttpResponseMessage.Content.ReadAs<PaginatedSearchArticlesResult>();
        result!.Articles!.Count.Should().Be(count);
    }

    [Then(@"Получен список статей для категории с именем ""(.*)""")]
    public void ThenПолученСписокСтатейДляКатегорииСИменем(string name)
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = HttpResponseMessage.Content.ReadAs<List<ArticleModel>>();
        Articles.Clear();
        foreach (var article in result!)
        {
            article.ParentId.Should().Be(Section!.Id);
            Articles.Add(article);
        }
    }

    [Then(@"Статьи отсортированы по позиции")]
    public void ThenСтатьиОтсортированыПоПозиции()
    {
        Articles.Should().BeInAscendingOrder(article => article!.OrderPosition);
    }

    [Then(@"Сообщение отправлено в брокер")]
    public void ThenСообщениеОтправленоВБрокер()
    {
        const string expectedMessage = "New sections created successful";
        string message;
        
        var factory = new ConnectionFactory { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            const string queue = "LogMessageQueue";
            var response = channel.BasicGet(queue, true);
            message = Encoding.UTF8.GetString(response.Body.ToArray());
        }

        message.Should().Be(expectedMessage);
    }

    [Then(@"Сообщение не отправлено в брокер")]
    public void ThenСообщениеНеОтправленоВБрокер()
    {
        uint messageCount;
        
        var factory = new ConnectionFactory { HostName = "localhost" };
        using(var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            const string queue = "LogMessageQueue";
            
            channel.QueueDeclare(queue: queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            messageCount = channel.MessageCount(queue);
        }

        messageCount.Should().Be(0);
    }

    [Then(@"Тэг успешно добавлен")]
    public void ThenТэгУспешноДобавлен()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        var tagFromServer = HttpResponseMessage.Content.ReadAs<TagModel>();
        tagFromServer!.Name.Should().Be(Tag.Name);
    }

    [Then(@"Администратор получает все тэги")]
    public void ThenАдминистраторПолучаетВсеТэги()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var tags = HttpResponseMessage.Content.ReadAs<List<TagModel>>();
        tags.Should().BeEquivalentTo(Tags);
    }

    [Then(@"Администратор получает тэг с идентификатором")]
    public void ThenАдминистраторПолучаетТэгСИдентификатором()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        var tagFromServer = HttpResponseMessage.Content.ReadAs<TagWithArticles>();
        var tagAsModel = new TagModel {Id = tagFromServer!.Id, Name = tagFromServer.Name};
        tagAsModel.Should().BeEquivalentTo(Tag);
    }

    [Then(@"Тэг успешно обновлен")]
    public void ThenТэгУспешноОбновлен()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        var tagFromServer = HttpResponseMessage.Content.ReadAs<TagModel>();
        tagFromServer.Should().BeEquivalentTo(Tag);
    }

    [Then(@"Тэг успешно удален")]
    public void ThenТэгУспешноУдален()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Then(@"Администратор получает пагинированный ответ со статьей с Id ""(.*)""")]
    public void ThenАдминистраторПолучаетПагинированныйОтветСоСтатьейСId(int id)
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var result = HttpResponseMessage.Content.ReadAs<PaginatedSearchArticlesResult>();
        result!.Articles!.Should().Contain(x=>x.Id == id);
    }

    [Then(@"Тэги с названиями ""(.*)"" успешно добавлены вопросу")]
    public void ThenТэгиСНазваниямиУспешноДобавленыВопросу(string tagNamesAsString)
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        var articleFromServer = HttpResponseMessage.Content.ReadAs<ArticleModel>();
        var tags = tagNamesAsString.Split(',', StringSplitOptions.TrimEntries)
            .Select(name => Tags.FirstOrDefault(tag => tag!.Name == name))
            .Select(tag => tag!.Id)
            .ToList();
        articleFromServer!.Tags.Should().Contain(x => tags.Contains(x.Id));
    }

    [Then(@"Администратор получает пагинированный ответ со статьей в количестве (.*)")]
    public void ThenАдминистраторПолучаетПагинированныйОтветСоСтатьейВКоличестве(int count)
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var result = HttpResponseMessage.Content.ReadAs<PaginatedSearchArticlesResult>();
        result!.Articles!.Count.Should().Be(count);
    }
}