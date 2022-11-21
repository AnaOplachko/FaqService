using FaqDataAccess.Repositories.SectionRepository;
using FaqService.Application.Exceptions;
using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Section.ReadById;

/// <summary>
/// Обработчик запроса на получение категории по идентификатору
/// </summary>
public class GetSectionByIdQueryHandler : IRequestHandler<GetSectionByIdQuery, SectionWithSubs>
{
    private readonly ISectionRepository _sectionRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public GetSectionByIdQueryHandler(ISectionRepository sectionRepository)
        => _sectionRepository = sectionRepository;
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<SectionWithSubs> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
    {
        var section = await _sectionRepository.GetSectionByIdAsync(request.Id);

        if (section == null!)
            throw new NoEntityException("No section found");

        return new SectionWithSubs(section);
    }
}