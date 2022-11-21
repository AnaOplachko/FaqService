namespace FaqService.Application.Exceptions;

/// <summary>
/// Ошибка некоректного поискового запроса
/// </summary>
public class InvalidSearchRequestException : Exception
{
    /// <summary>
    /// ctor
    /// </summary>
    public InvalidSearchRequestException(string message) : base(message) { }
}