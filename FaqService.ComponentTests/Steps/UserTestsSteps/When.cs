using FaqService.ComponentTests.ExternalEnvironment;
using FaqService.ComponentTests.Helpers;
using FaqService.ComponentTests.Hooks.Common;
using TechTalk.SpecFlow;

namespace FaqService.ComponentTests.Steps.UserTestsSteps;

[Binding]
public class When : Common
{
    [When(@"Пользователь запрашивает все категории")]
    public async Task WhenПользовательЗапрашиваетВсеКатегории()
    {
        var request = new RequestMessageBuilder().WithUserGetAllSectionsQuery().Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Пользователь запрашивает категорию по идентификатору")]
    public async Task WhenПользовательЗапрашиваетКатегориюПоИдентификатору()
    {
        var request = new RequestMessageBuilder().WithUserGetSectionByIdQuery(Section!.Id).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Пользователь запрашивает категорию с некорректным идентификатором")]
    public async Task WhenПользовательЗапрашиваетКатегориюСНекорректнымИдентификатором()
    {
        var incorrectId = int.MaxValue;
        
        var request = new RequestMessageBuilder().WithUserGetSectionByIdQuery(incorrectId).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Пользователь запрашивает все статьи")]
    public async Task WhenПользовательЗапрашиваетВсеСтатьи()
    {
        var request = new RequestMessageBuilder().WithUserGetAllArticlesQuery().Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Пользователь запрашивает статьи по слову (.*) на странице (.*) с размером страницы (.*)")]
    public async Task WhenПользовательЗапрашиваетСтатьиПоСловуНаСтраницеСРазмеромСтраницы(string searchQuery, int page, int pageSize)
    {
        var request = new RequestMessageBuilder().WithUserSearchArticleQuery(searchQuery, page, pageSize).Build();
            
        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Пользователь запрашивает статьи по запросу ""(.*)"" на странице ""(.*)"" с размером страницы ""(.*)""")]
    public async Task WhenПользовательЗапрашиваетСтатьиПоЗапросуНаСтраницеСРазмеромСтраницы(string searchQuery, int page, int pageSize)
    {
        var request = new RequestMessageBuilder().WithUserSearchArticleQuery(searchQuery, page, pageSize).Build();
            
        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Пользователь запрашивает статью по идентификатору")]
    public async Task WhenПользовательЗапрашиваетСтатьюПоИдентификатору()
    {
        var request = new RequestMessageBuilder().WithUserGetArticleByIdQuery(Article.Id).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Пользователь запрашивает статью с некорректным идентификатором")]
    public async Task WhenПользовательЗапрашиваетСтатьюСНекорректнымИдентификатором()
    {
        var incorrectId = int.MaxValue;
        
        var request = new RequestMessageBuilder().WithUserGetArticleByIdQuery(incorrectId).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Пользователь запрашивает все тэги")]
    public async Task WhenПользовательЗапрашиваетВсеТэги()
    {
        var request = new RequestMessageBuilder().WithUserGetAllTagsQuery().Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Пользователь запрашивает тэг по идентификатору")]
    public async Task WhenПользовательЗапрашиваетТэгПоИдентификатору()
    {
        var request = new RequestMessageBuilder().WithUserGetTagByIdQuery(Tag.Id).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }

    [When(@"Пользователь запрашивает тэг с некорректным идентификатором")]
    public async Task WhenПользовательЗапрашиваетТэгСНекорректнымИдентификатором()
    {
        var incorrectId = int.MaxValue;
        
        var request = new RequestMessageBuilder().WithUserGetTagByIdQuery(incorrectId).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);
    }
}