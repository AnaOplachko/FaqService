using FaqService.Application.Commands.Admin.Article.Create;
using FaqService.Application.Commands.Admin.Article.Delete;
using FaqService.Application.Commands.Admin.Article.ReadAll;
using FaqService.Application.Commands.Admin.Article.ReadById;
using FaqService.Application.Commands.Admin.Article.Search;
using FaqService.Application.Commands.Admin.Article.SetTags;
using FaqService.Application.Commands.Admin.Article.Update;
using FaqService.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FaqService.Controllers.Admin;

[ApiController]
[Route("admin/articles", Name = "FAQ: articles for admin")]
public class ArticleController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// ctor
    /// </summary>
    public ArticleController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Возвращает лист всех статей
    /// </summary>
    [HttpGet("all")]
    public async Task<List<Article>> GetAllArticles() => await _mediator.Send(new GetAllArticlesQuery());

    /// <summary>
    /// Возвращает статью по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<Article> GetArticleById([FromRoute] int id)
        => await _mediator.Send(new GetArticleByIdQuery { Id = id });

    /// <summary>
    /// Обновление тегов вопроса
    /// </summary>
    [HttpPut("{id}/tags")]
    public async Task<Article> SetTags([FromRoute] int id, [FromBody] SetTagsCommand setTagsCommand)
    {
        setTagsCommand.Id = id;
        return await _mediator.Send(setTagsCommand);
    }

    /// <summary>
    /// Поиск запроса по статьям
    /// </summary>
    [HttpGet("search")]
    public async Task<PaginatedSearchArticlesResult> SearchArticles([FromQuery] SearchArticlesQuery query)
        => await _mediator.Send(query);

    /// <summary>
    /// Создание статьи
    /// </summary>
    [HttpPost("")]
    public async Task<Article> CreateArticle([FromBody] CreateArticleCommand createArticleCommand)
        => await _mediator.Send(createArticleCommand);

    /// <summary>
    /// Изменение статьи
    /// </summary>
    [HttpPut("{id}")]
    public async Task<Article> UpdateArticle([FromRoute] int id, [FromBody] UpdateArticleCommand updateArticleCommand)
    {
        updateArticleCommand.Id = id;
        return await _mediator.Send(updateArticleCommand);
    }

    /// <summary>
    /// Удаление статьи
    /// </summary>
    [HttpDelete("{id}")]
    public async Task DeleteArticle([FromRoute] int id) 
        => await _mediator.Send(new DeleteArticleCommand { Id = id });
}