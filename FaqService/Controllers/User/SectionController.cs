using FaqService.Application.Commands.User.Section.ReadAll;
using FaqService.Application.Commands.User.Section.ReadById;
using FaqService.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FaqService.Controllers.User;

[ApiController]
[Route("user/sections", Name = "FAQ: sections for user")]
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
    public async Task<SectionWithSubs> GetSectionById([FromRoute] int id)
        => await _mediator.Send(new GetSectionByIdQuery { Id = id });
}