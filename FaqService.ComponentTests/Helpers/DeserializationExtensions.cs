using System.Text.Json;

namespace FaqService.ComponentTests.Helpers;

public static class DeserializationExtensions
{
    /// <summary>
    /// Возвращает десериализованный контент из ответа сервера
    /// </summary>
    public static T? ReadAs<T>(this HttpContent content)
    {
        var stringContent = content.ReadAsStringAsync().GetAwaiter().GetResult();
    
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        
        return JsonSerializer.Deserialize<T>(stringContent, options);
    }
}