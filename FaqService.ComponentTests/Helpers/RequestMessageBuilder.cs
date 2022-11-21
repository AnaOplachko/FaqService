using System.Text;
using System.Text.Json;
using FaqService.Application.Commands.Admin.Article.Create;
using FaqService.Application.Commands.Admin.Article.SetTags;
using FaqService.Application.Commands.Admin.Article.Update;
using FaqService.Application.Commands.Admin.Section.Create;
using FaqService.Application.Commands.Admin.Section.Update;
using FaqService.Application.Commands.Admin.Tag.Create;
using FaqService.Application.Commands.Admin.Tag.Update;

namespace FaqService.ComponentTests.Helpers;

/// <summary>
/// Строитель для конфигурации HttpRequestMessage
/// </summary>
public class RequestMessageBuilder
{
    private string? _body;
    private HttpMethod _httpMethod = null!;
    private string? _requestUri;
    
    /// <summary>
    /// Конфигурируем строителя командой на создание категории администратором
    /// </summary>
    public RequestMessageBuilder WithCreateSectionCommand(string name, int? parentId = null)
    {
        var createCommand = new CreateSectionCommand { Name = name, ParentId = parentId };
        _body = JsonSerializer.Serialize(createCommand);
        _httpMethod = HttpMethod.Post;
        _requestUri = "admin/sections";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя запросом на получение администратором списка всех категорий 
    /// </summary>
    public RequestMessageBuilder WithAdminGetAllSectionsQuery()
    {
        _httpMethod = HttpMethod.Get;
        _requestUri = "admin/sections/all";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя командой на обновление категории администратором
    /// </summary>
    public RequestMessageBuilder WithUpdateSectionCommand(int id, string name, int? parentId)
    {
        var updateCommand = new UpdateSectionCommand { Id = id, Name = name, ParentId = parentId };
        _body = JsonSerializer.Serialize(updateCommand);
        _httpMethod = HttpMethod.Put;
        _requestUri = $"admin/sections/{id}";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя запросом на получение администратором категории по идентификатору 
    /// </summary>
    public RequestMessageBuilder WithAdminGetSectionByIdQuery(int id)
    {
        _httpMethod = HttpMethod.Get;
        _requestUri = $"admin/sections/{id}";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя командой на удаление администратором категории по идентификатору 
    /// </summary>
    public RequestMessageBuilder WithDeleteSectionByIdCommand(int id)
    {
        _httpMethod = HttpMethod.Delete;
        _requestUri = $"admin/sections/{id}";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя командой на создание статьи администратором
    /// </summary>
    public RequestMessageBuilder WithCreateArticleCommand(string question, string answer, int parentId, int? orderPosition = null)
    {
        var createCommand = new CreateArticleCommand { Question = question, Answer = answer, ParentId = parentId, OrderPosition = orderPosition};
        _body = JsonSerializer.Serialize(createCommand);
        _httpMethod = HttpMethod.Post;
        _requestUri = "admin/articles";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя запросом на получение администратором списка всех статей 
    /// </summary>
    public RequestMessageBuilder WithAdminGetAllArticlesQuery()
    {
        _httpMethod = HttpMethod.Get;
        _requestUri = "admin/articles/all";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя запросом на получение администратором статьи по идентификатору 
    /// </summary>
    public RequestMessageBuilder WithAdminGetArticleByIdQuery(int id)
    {
        _httpMethod = HttpMethod.Get;
        _requestUri = $"admin/articles/{id}";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя командой на удаление администратором статьи по идентификатору 
    /// </summary>
    public RequestMessageBuilder WithDeleteArticleByIdCommand(int id)
    {
        _httpMethod = HttpMethod.Delete;
        _requestUri = $"admin/articles/{id}";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя командой на обновление статьи администратором
    /// </summary>
    public RequestMessageBuilder WithUpdateArticleCommand(int id, string question, string answer, int parentId, int? orderPosition)
    {
        var updateCommand = new UpdateArticleCommand { Id = id, Question = question, Answer = answer, ParentId = parentId, OrderPosition = orderPosition};
        _body = JsonSerializer.Serialize(updateCommand);
        _httpMethod = HttpMethod.Put;
        _requestUri = $"admin/articles/{id}";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя командой на обновление статьи администратором
    /// </summary>
    public RequestMessageBuilder WithSetTagsCommand(int id, ICollection<int> tags)
    {
        var setTagsCommand = new SetTagsCommand { Id = id, Tags = tags };
        _body = JsonSerializer.Serialize(setTagsCommand);
        _httpMethod = HttpMethod.Put;
        _requestUri = $"admin/articles/{id}/tags";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя запросом на поиск статьи администратором
    /// </summary>
    public RequestMessageBuilder WithAdminSearchArticleQuery(string searchQuery, int page, int pageSize)
    {
        _httpMethod = HttpMethod.Get;
        _requestUri = $"admin/articles/search?SearchQuery={searchQuery}&Pagination.Page={page}&Pagination.PageSize={pageSize}";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя командой на получение отсортированных статей администратором
    /// </summary>
    public RequestMessageBuilder WithAdminSortedArticlesQuery(int id)
    {
        _httpMethod = HttpMethod.Get;
        _requestUri = $"admin/articles/top/section/{id}";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя запросом на получение пользователем списка всех категорий 
    /// </summary>
    public RequestMessageBuilder WithUserGetAllSectionsQuery()
    {
        _httpMethod = HttpMethod.Get;
        _requestUri = "user/sections/all";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя запросом на получение пользователем категории по идентификатору 
    /// </summary>
    public RequestMessageBuilder WithUserGetSectionByIdQuery(int id)
    {
        _httpMethod = HttpMethod.Get;
        _requestUri = $"user/sections/{id}";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя запросом на получение пользователем списка всех статей 
    /// </summary>
    public RequestMessageBuilder WithUserGetAllArticlesQuery()
    {
        _httpMethod = HttpMethod.Get;
        _requestUri = "user/articles/all";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя запросом на получение пользователем статьи по идентификатору 
    /// </summary>
    public RequestMessageBuilder WithUserGetArticleByIdQuery(int id)
    {
        _httpMethod = HttpMethod.Get;
        _requestUri = $"user/articles/{id}";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя запросом на поиск статьи пользователем
    /// </summary>
    public RequestMessageBuilder WithUserSearchArticleQuery(string searchQuery, int page, int pageSize)
    {
        _httpMethod = HttpMethod.Get;
        _requestUri = $"user/articles/search?SearchQuery={searchQuery}&Pagination.Page={page}&Pagination.PageSize={pageSize}";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя командой на создание тэга администратором
    /// </summary>
    public RequestMessageBuilder WithCreateTagCommand(string name)
    {
        var createCommand = new CreateTagCommand { Name = name };
        _body = JsonSerializer.Serialize(createCommand);
        _httpMethod = HttpMethod.Post;
        _requestUri = "admin/tags";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя запросом на получение администратором списка всех тэгов 
    /// </summary>
    public RequestMessageBuilder WithAdminGetAllTagsQuery()
    {
        _httpMethod = HttpMethod.Get;
        _requestUri = "admin/tags/all";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя запросом на получение администратором тэга по идентификатору 
    /// </summary>
    public RequestMessageBuilder WithAdminGetTagByIdQuery(int id)
    {
        _httpMethod = HttpMethod.Get;
        _requestUri = $"admin/tags/{id}";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя командой на обновление тэга администратором
    /// </summary>
    public RequestMessageBuilder WithUpdateTagCommand(int id, string name)
    {
        var updateCommand = new UpdateTagCommand { Id = id, Name = name };
        _body = JsonSerializer.Serialize(updateCommand);
        _httpMethod = HttpMethod.Put;
        _requestUri = $"admin/tags/{id}";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя командой на удаление администратором тэга по идентификатору 
    /// </summary>
    public RequestMessageBuilder WithDeleteTagByIdCommand(int id)
    {
        _httpMethod = HttpMethod.Delete;
        _requestUri = $"admin/tags/{id}";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя запросом на получение пользователем списка всех тэгов 
    /// </summary>
    public RequestMessageBuilder WithUserGetAllTagsQuery()
    {
        _httpMethod = HttpMethod.Get;
        _requestUri = "user/tags/all";
        return this;
    }
    
    /// <summary>
    /// Конфигурируем строителя запросом на получение пользователем тэга по идентификатору 
    /// </summary>
    public RequestMessageBuilder WithUserGetTagByIdQuery(int id)
    {
        _httpMethod = HttpMethod.Get;
        _requestUri = $"user/tags/{id}";
        return this;
    }
    
    /// <summary>
    /// Строим HttpRequestMessage
    /// </summary>
    public HttpRequestMessage Build()
    {
        var request = new HttpRequestMessage(_httpMethod, _requestUri);
        
        if (!string.IsNullOrEmpty(_body))
            request.Content = new StringContent(_body, Encoding.UTF8, "application/json");

        return request;
    }
}