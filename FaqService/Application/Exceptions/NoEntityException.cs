namespace FaqService.Application.Exceptions;

/// <summary>
/// Не найдена запрашиваемая сущность
/// </summary>
public class NoEntityException : Exception
{
    /// <summary>
    /// ctor
    /// </summary>
    public NoEntityException(string message) : base (message) { }
}