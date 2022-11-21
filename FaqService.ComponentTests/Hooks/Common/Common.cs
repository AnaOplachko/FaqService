using FaqService.Application.Models;
using TechTalk.SpecFlow;

namespace FaqService.ComponentTests.Hooks.Common;

[Binding]
public class Common
{
    /// <summary>
    /// Последняя созданная категория
    /// </summary>
    protected static SectionModel? Section = null!;

    /// <summary>
    /// Созданные категории
    /// </summary>
    protected static readonly List<SectionWithSubs> SectionsWithSubs = new();

    /// <summary>
    /// Последняя созданная статья
    /// </summary>
    protected static ArticleModel Article = null!;

    /// <summary>
    /// Созданные статьи
    /// </summary>
    protected static readonly List<ArticleModel?> Articles = new();

    /// <summary>
    /// Последний тэг
    /// </summary>
    protected static TagModel Tag = null!;

    /// <summary>
    /// Созданные тэги
    /// </summary>
    protected static readonly List<TagModel?> Tags = new();

    /// <summary>
    /// http ответ
    /// </summary>
    protected static HttpResponseMessage HttpResponseMessage { get; set; } = null!;

    /// <summary>
    /// Очистка состояния
    /// </summary>
    public static void ClearState()
    {
        SectionsWithSubs.Clear();
        Articles.Clear();
        Tags.Clear();
    }
}