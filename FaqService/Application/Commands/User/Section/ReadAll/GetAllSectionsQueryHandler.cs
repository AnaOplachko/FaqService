using FaqDataAccess.Repositories.SectionRepository;
using FaqService.Application.Exceptions;
using FaqService.Application.Dtos;
using MediatR;

namespace FaqService.Application.Commands.User.Section.ReadAll;

/// <summary>
/// Обработчик запроса на получение пользователем всех категорий
/// </summary>
public class GetAllSectionsQueryHandler : IRequestHandler<GetAllSectionsQuery, List<SectionWithSubs>>
{
    private readonly ICachedSectionRepository _cachedSectionRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public GetAllSectionsQueryHandler(ICachedSectionRepository cachedSectionRepository)
        => _cachedSectionRepository = cachedSectionRepository;
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<List<SectionWithSubs>> Handle(GetAllSectionsQuery request, CancellationToken cancellationToken)
    {
        var sections = await _cachedSectionRepository.GetAllSectionsAsync();

        if (sections.Count == 0)
            throw new NoEntityException("No sections found");
        
        return sections.Select(x => new SectionWithSubs(x)).ToList();
    }
}