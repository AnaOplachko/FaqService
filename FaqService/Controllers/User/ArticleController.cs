using FaqService.Application.Commands.User.Article.ReadAll;
using FaqService.Application.Commands.User.Article.ReadById;
using FaqService.Application.Commands.User.Article.Search;
using FaqService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FaqService.Controllers.User;

[ApiController]
[Route("user/articles", Name = "FAQ: articles for user")]
public class ArticleController : ControllerBase
{
    private readonly IMediator _mediator;
    
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="mediator"></param>
    public ArticleController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Возвращает лист всех статей
    /// </summary>
    [HttpGet("all")]
    public async Task<List<ArticleModel>> GetAllArticles() => await _mediator.Send(new GetAllArticlesQuery());

    /// <summary>
    /// Метод обработчик запроса на получение статьи по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ArticleModel> GetArticleById([FromRoute] int id) 
        => await _mediator.Send(new GetArticleByIdQuery { Id = id });

    /// <summary>
    /// Метод обработчик поискового запроса по статьям
    /// </summary>
    [HttpGet("search")]
    public async Task<PaginatedSearchArticlesResult> SearchArticles([FromQuery] SearchArticlesQuery query)
        => await _mediator.Send(query);
}