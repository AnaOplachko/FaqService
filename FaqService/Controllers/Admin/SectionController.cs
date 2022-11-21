using FaqService.Application.Commands.Admin.Section.Create;
using FaqService.Application.Commands.Admin.Section.Delete;
using FaqService.Application.Commands.Admin.Section.ReadAll;
using FaqService.Application.Commands.Admin.Section.ReadById;
using FaqService.Application.Commands.Admin.Section.Update;
using FaqService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FaqService.Controllers.Admin;

[ApiController]
[Route("admin/sections", Name = "FAQ: sections for admin")]
public class SectionController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// ctor
    /// </summary>
    public SectionController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Возвращает лист всех категорий
    /// </summary>
    [HttpGet("all")]
    public async Task<List<SectionWithSubs>> GetAllSections() => await _mediator.Send(new GetAllSectionsQuery());

    /// <summary>
    /// Возвращает категорию по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<SectionWithSubs> GetSectionByUId([FromRoute] int id)
        => await _mediator.Send(new GetSectionByIdQuery { Id = id });

    /// <summary>
    /// Создание категории
    /// </summary>
    [HttpPost("")]
    public async Task<SectionModel> CreateSection([FromBody] CreateSectionCommand createSectionCommand)
        => await _mediator.Send(createSectionCommand);

    /// <summary>
    /// Изменение категории
    /// </summary>
    [HttpPut("{id}")]
    public async Task<SectionModel> UpdateSection([FromRoute] int id, [FromBody] UpdateSectionCommand updateSectionCommand)
    {
        updateSectionCommand.Id = id;
        return await _mediator.Send(updateSectionCommand);
    }

    /// <summary>
    /// Удаление категории
    /// </summary>
    [HttpDelete("{id}")]
    public async Task DeleteSection([FromRoute] int id) => await _mediator.Send(new DeleteSectionCommand { Id = id });
}