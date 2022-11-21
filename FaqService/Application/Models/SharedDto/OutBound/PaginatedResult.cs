namespace FaqService.Application.Models.SharedDto.OutBound;

public abstract class PaginatedResult
{
    /// <summary>
    /// Расстраничивание
    /// </summary>
    public Pagination Pagination { get; set; }
}

public class Pagination
{
    /// <summary>
    /// Текущая страница
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Размер страницы
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Общее количество элементов
    /// </summary>
    public int TotalCount { get; set; }
}