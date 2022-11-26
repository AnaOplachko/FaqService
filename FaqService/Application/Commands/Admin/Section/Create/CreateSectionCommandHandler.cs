using FaqDataAccess.Repositories.SectionRepository;
using FaqService.Application.Exceptions;
using MediatR;

namespace FaqService.Application.Commands.Admin.Section.Create;

/// <summary>
/// Обработчик команды на создание категории
/// </summary>
public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, Dtos.Section>
{
    private readonly ISectionRepository _sectionRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public CreateSectionCommandHandler(ISectionRepository sectionRepository) => _sectionRepository = sectionRepository;
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<Dtos.Section> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
    {
        if (request.ParentId != null)
        {
            var parent = await _sectionRepository.GetSectionByIdAsync(request.ParentId.Value);

            if (parent == null!)
                throw new NoEntityException("По идентификатору родительской категории ничего не найдено");
        }

        var newSection = new FaqDomain.Aggregates.Section(request.Name, request.ParentId);

        const string creationMessage = "New sections created successful";
        newSection.AddCreationMessage(creationMessage);
        
        _sectionRepository.CreateSection(newSection);
        await _sectionRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return new Dtos.Section(newSection);
    }
}