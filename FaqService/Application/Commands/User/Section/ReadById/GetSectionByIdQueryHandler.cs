using FaqDataAccess.Repositories.SectionRepository;
using FaqService.Application.Exceptions;
using FaqService.Application.Dtos;
using MediatR;

namespace FaqService.Application.Commands.User.Section.ReadById;

/// <summary>
/// Обработчик запроса на получение пользователем категории по идентификатору
/// </summary>
public class GetSectionByIdQueryHandler : IRequestHandler<GetSectionByIdQuery, SectionWithSubs>
{
    private readonly ICachedSectionRepository _cachedSectionRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public GetSectionByIdQueryHandler(ICachedSectionRepository cachedSectionRepository)
        => _cachedSectionRepository = cachedSectionRepository;
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<SectionWithSubs> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
    {
        var section = await _cachedSectionRepository.GetSectionByIdAsync(request.Id);

        if (section == null!)
            throw new NoEntityException("No section found");

        return new SectionWithSubs(section);
    }
}