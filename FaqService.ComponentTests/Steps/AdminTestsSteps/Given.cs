using FaqService.Application.Dtos;
using FaqService.ComponentTests.ExternalEnvironment;
using FaqService.ComponentTests.Helpers;
using FaqService.ComponentTests.Hooks.Common;
using TechTalk.SpecFlow;

namespace FaqService.ComponentTests.Steps.AdminTestsSteps;

[Binding]
public class Given : Common
{
    [Given(@"В базу данных добавлена корневая категория с именем ""(.*)""")]
    public async Task GivenВБазуДанныхДобавленаКорневаяКатегорияСИменем(string name)
    {
        var request = new RequestMessageBuilder().WithCreateSectionCommand(name).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);

        Section = HttpResponseMessage.Content.ReadAs<Section>();
        
        SectionsWithSubs.Add(new SectionWithSubs(Section!));
    }

    [Given(@"В базу данных добавлена дочерняя категория с именем ""(.*)""")]
    public async Task GivenВБазуДанныхДобавленаДочерняяКатегорияСИменем(string name)
    {
        var parent = SectionsWithSubs.FirstOrDefault(x => x.ParentId == null);
        
        var request = new RequestMessageBuilder().WithCreateSectionCommand(name, parent!.Id).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);

        Section = HttpResponseMessage.Content.ReadAs<Section>();
        
        SectionsWithSubs.Add(new SectionWithSubs(Section!));
        
        parent.Subsections.Add(new SectionWithSubs(Section!));
    }

    [Given(@"В базу данных добавлена статья с вопросом ""(.*)"", ответом ""(.*)"", позицией ""(.*)""")]
    public async Task GivenВБазуДанныхДобавленаСтатьяСВопросомОтветомПозицией(string question, string answer,
        int? position)
    {
        var parent = SectionsWithSubs.FirstOrDefault(x => x.ParentId != null);

        var request = new RequestMessageBuilder().WithCreateArticleCommand(question, answer, parent!.Id, position)
            .Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);

        Article = HttpResponseMessage.Content.ReadAs<Article>()!;

        Articles.Add(Article);
    }

    [Given(@"В базу данных добавлены тэг с названием ""(.*)""")]
    public async Task GivenВБазуДанныхДобавленыТэгСНазванием(string name)
    {
        var request = new RequestMessageBuilder().WithCreateTagCommand(name).Build();

        HttpResponseMessage = await ExtEnvironment.TestServer!.CreateClient().SendAsync(request);

        Tag = HttpResponseMessage.Content.ReadAs<Tag>()!;
            
        Tags.Add(Tag);
    }
}