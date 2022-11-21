using System.ComponentModel.DataAnnotations;

namespace FaqService.Configurations.Models;

/// <summary>
/// Настройки подключения к БД
/// </summary>
public class DbSettings
{
    /// <summary>
    /// Пользователь
    /// </summary>
    [Required] public string User { get; set; } = null!;

    /// <summary>
    /// Пароль
    /// </summary>
    [Required] public string Password { get; set; } = null!;

    /// <summary>
    /// Хост
    /// </summary>
    [Required] public string Host { get; set; } = null!;

    /// <summary>
    /// Порт
    /// </summary>
    [Required] public int Port { get; set; }

    /// <summary>
    /// Имя БД
    /// </summary>
    [Required] public string DbName { get; set; } = null!;
}