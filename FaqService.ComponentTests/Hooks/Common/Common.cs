using FaqService.Application.Dtos;
using TechTalk.SpecFlow;

namespace FaqService.ComponentTests.Hooks.Common;

[Binding]
public class Common
{
    /// <summary>
    /// Последняя созданная категория
    /// </summary>
    protected static Section? Section = null!;

    /// <summary>
    /// Созданные категории
    /// </summary>
    protected static readonly List<SectionWithSubs> SectionsWithSubs = new();

    /// <summary>
    /// Последняя созданная статья
    /// </summary>
    protected static Article Article = null!;

    /// <summary>
    /// Созданные статьи
    /// </summary>
    protected static readonly List<Article?> Articles = new();

    /// <summary>
    /// Последний тэг
    /// </summary>
    protected static Tag Tag = null!;

    /// <summary>
    /// Созданные тэги
    /// </summary>
    protected static readonly List<Tag?> Tags = new();

    /// <summary>
    /// http ответ
    /// </summary>
    protected static HttpResponseMessage HttpResponseMessage { get; set; } = null!;
    
    /// <summary>
    /// Некорректный идентификатор, по которому гарантировано не будет возвращен результат запроса
    /// </summary>
    protected const int IncorrectId = Int32.MaxValue;

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