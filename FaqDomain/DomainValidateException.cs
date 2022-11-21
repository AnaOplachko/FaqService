namespace FaqDomain;

/// <summary>
/// Ошибка в доменной логике 
/// </summary>
public class DomainValidateException : Exception
{
    /// <summary>
    /// ctor
    /// </summary>
    public DomainValidateException(string message) : base(message) { }
}