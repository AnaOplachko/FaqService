using System.Net;
using FaqService.Application.Dtos;
using FaqService.ComponentTests.Helpers;
using FaqService.ComponentTests.Hooks.Common;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace FaqService.ComponentTests.Steps.UserTestsSteps;

[Binding]
public class Then : Common
{
    [Then(@"Пользователь получает все категории")]
    public void ThenПользовательПолучаетВсеКатегории()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var sections = HttpResponseMessage.Content.ReadAs<List<SectionWithSubs>>();
        sections.Should().BeEquivalentTo(SectionsWithSubs);
    }

    [Then(@"Пользователь получает сообщение об ошибке NotFound")]
    public void ThenПользовательПолучаетСообщениеОбОшибкеNotFound()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then(@"Пользователь получает категорию по идентификатору")]
    public void ThenПользовательПолучаетКатегориюПоИдентификатору()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
    
        var sectionFromServer = HttpResponseMessage.Content.ReadAs<SectionWithSubs>();
        sectionFromServer.Should().BeEquivalentTo(Section);
    }

    [Then(@"Пользователь получает все статьи")]
    public void ThenПользовательПолучаетВсеСтатьи()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var articles = HttpResponseMessage.Content.ReadAs<List<Article>>();
        articles.Should().BeEquivalentTo(Articles);
    }

    [Then(@"Пользователь получает статьи в количестве (.*)")]
    public void ThenПользовательПолучаетСтатьиВКоличестве(int count)
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var articles = HttpResponseMessage.Content.ReadAs<PaginatedSearchArticlesResult>();
        articles!.Articles!.Count.Should().Be(count);
    }

    [Then(@"Пользователь успешно получает статью с идентификатором")]
    public void ThenПользовательУспешноПолучаетСтатьюСИдентификатором()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        var articleFromServer = HttpResponseMessage.Content.ReadAs<Article>();
        articleFromServer.Should().BeEquivalentTo(Article);
    }

    [Then(@"Пользователь получает все тэги")]
    public void ThenПользовательПолучаетВсеТэги()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var tags = HttpResponseMessage.Content.ReadAs<List<Tag>>();
        tags.Should().BeEquivalentTo(Tags);
    }

    [Then(@"Пользователь получает тэг с идентификатором")]
    public void ThenПользовательПолучаетТэгСИдентификатором()
    {
        HttpResponseMessage.Should().NotBeNull();
        HttpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        var tagFromServer = HttpResponseMessage.Content.ReadAs<TagWithArticles>();
        var tagAsModel = new Tag {Id = tagFromServer!.Id, Name = tagFromServer.Name};
        tagAsModel.Should().BeEquivalentTo(Tag);
    }
}