using System.ComponentModel.DataAnnotations;

namespace FaqService.Application.Models.SharedDto;

public class Pagination
{
    /// <summary>
    /// Номер страницы
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Значение должно быть больше ноля")]
    public int Page { get; set; }
    
    /// <summary>
    /// Размер страницы
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Значение должно быть больше ноля")]
    public int PageSize { get; set; }
}