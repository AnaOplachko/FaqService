using FaqService.Application.Commands.User.Tag.ReadAll;
using FaqService.Application.Commands.User.Tag.ReadById;
using FaqService.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FaqService.Controllers.User;

[ApiController]
[Route("user/tags", Name = "Faq: tags for user")]
public class TagController : ControllerBase
{
    private readonly IMediator _mediator;
    
    /// <summary>
    /// ctor
    /// </summary>
    public TagController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Возвращает лист всех тэгов
    /// </summary>
    [HttpGet("all")]
    public async Task<List<Tag>> GetAllTags() => await _mediator.Send(new GetAllTagsQuery());

    /// <summary>
    /// Возвращает тэг по  дентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<TagWithArticles> GetTagById([FromRoute] int id)
        => await _mediator.Send(new GetTagByIdQuery { Id = id });
}