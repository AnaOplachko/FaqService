namespace FaqService.Configurations.Models;

/// <summary>
/// Настройки кэша
/// </summary>
public class MemoryCacheSettings
{
    /// <summary>
    /// Время актуальности кэша
    /// </summary>
    public int Expiration { get; set; }
}