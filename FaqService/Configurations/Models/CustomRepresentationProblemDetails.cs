using Microsoft.AspNetCore.WebUtilities;

namespace FaqService.Configurations.Models;

/// <summary>
/// Обработчик ошибок у которого в type передается имя ошибки
/// </summary>
public class CustomRepresentationProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
{
    /// <summary>
    /// ctor
    /// </summary>
    public CustomRepresentationProblemDetails(int statusCode, string detail, IServiceProvider? serviceProvider = null,
        string? title = null)
    {
        Type = $"https://httpstatuses.com/{statusCode}";
        Title = title ?? ReasonPhrases.GetReasonPhrase(statusCode);
        Status = statusCode;
        Detail = detail;
        Instance = serviceProvider?.GetService<IHttpContextAccessor>()?.HttpContext?.Request.Path;
    }
}