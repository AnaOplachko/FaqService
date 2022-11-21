namespace FaqService.Application.Exceptions;

/// <summary>
/// Попытка взаимодействия с недопустимой сущностью
/// </summary>
public class InvalidEntityException : Exception
{
    /// <summary>
    /// ctor
    /// </summary>
    public InvalidEntityException(string message) : base(message) { }
}