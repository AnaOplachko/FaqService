using FaqService.Application.Commands.Admin.Tag.Create;
using FaqService.Application.Commands.Admin.Tag.Delete;
using FaqService.Application.Commands.Admin.Tag.ReadAll;
using FaqService.Application.Commands.Admin.Tag.ReadById;
using FaqService.Application.Commands.Admin.Tag.Update;
using FaqService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FaqService.Controllers.Admin;

[ApiController]
[Route("admin/tags", Name = "FAQ: tags for admin")]
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
    public async Task<List<TagModel>> GetAllTags() => await _mediator.Send(new GetAllTagsQuery());

    /// <summary>
    /// Возвращает тэг по идентификатору со связанными статьями
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<TagWithArticles> GetTagById([FromRoute] int id) 
        => await _mediator.Send(new GetTagByIdQuery { Id = id });

    /// <summary>
    /// Создание тэга
    /// </summary>
    [HttpPost("")]
    public async Task<TagModel> CreateTag([FromBody] CreateTagCommand createTagCommand) 
        => await _mediator.Send(createTagCommand);

    /// <summary>
    /// Изменение тэга
    /// </summary>
    [HttpPut("{id}")]
    public async Task<TagModel> UpdateTag([FromRoute] int id, [FromBody] UpdateTagCommand updateTagCommand)
    {
        updateTagCommand.Id = id;
        return await _mediator.Send(updateTagCommand);
    }

    /// <summary>
    /// Удаление тэга
    /// </summary>
    [HttpDelete("{id}")]
    public async Task DeleteTag([FromRoute] int id) => await _mediator.Send(new DeleteTagCommand { Id = id });
}