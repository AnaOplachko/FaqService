using FaqDataAccess.Repositories.SectionRepository;
using FaqService.Application.Exceptions;
using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Section.ReadAll;

/// <summary>
/// Обработчик запроса на получение списка всех категорий
/// </summary>
public class GetAllSectionsQueryHandler : IRequestHandler<GetAllSectionsQuery, List<SectionWithSubs>>
{
    private readonly ISectionRepository _sectionRepository;
    
    /// <summary>
    /// ctor
    /// </summary>
    public GetAllSectionsQueryHandler(ISectionRepository sectionRepository)
        => _sectionRepository = sectionRepository;
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<List<SectionWithSubs>> Handle(GetAllSectionsQuery request, CancellationToken cancellationToken)
    {
        var sections = await _sectionRepository.GetAllSectionsAsync();
        
        if (sections.Count is 0)
            throw new NoEntityException("No sections found");
        
        return sections.Select(x => new SectionWithSubs(x)).ToList();
    }
}