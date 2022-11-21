using FaqService.Application.Commands.Admin.TopArticles;
using FaqService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FaqService.Controllers.Admin;

[ApiController]
[Route("admin/articles/top/section", Name = "FAQ: top articles for admin")]
public class TopArticlesController : Controller
{
    private readonly IMediator _mediator;

    /// <summary>
    /// ctor
    /// </summary>
    public TopArticlesController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Возвращает отсортированный по заданной позиции список статей для заданной категории
    /// </summary>
    /// <param name="id"> Идентификатор родительской категории </param>
    [HttpGet("{id}")]
    public async Task<List<ArticleModel>> GetSortedArticlesBySectionId([FromRoute] int id)
        => await _mediator.Send(new GetSortedArticlesBySectionQuery { SectionId = id });
}