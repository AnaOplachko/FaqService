using FaqDataAccess.Repositories.SectionRepository;
using FaqService.Application.Exceptions;
using MediatR;

namespace FaqService.Application.Commands.Admin.Section.Update;

/// <summary>
/// Обработчик команды на изменение категории
/// </summary>
public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, Dtos.Section>
{
    private readonly ISectionRepository _sectionRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public UpdateSectionCommandHandler(ISectionRepository sectionRepository)
        => _sectionRepository = sectionRepository;

    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<Dtos.Section> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
    {
        var sectionToUpdate = await _sectionRepository.GetSectionByIdAsync(request.Id);

        if (sectionToUpdate == null!)
            throw new NoEntityException("No section found");

        if (request.ParentId != null)
        {
            var parent = await _sectionRepository.GetSectionByIdAsync((int)request.ParentId);
            if (parent is null) 
                throw new NoEntityException("No parent section found");
        }

        sectionToUpdate.Update(request.Name, request.ParentId);

        _sectionRepository.UpdateSection(sectionToUpdate);
        await _sectionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new Dtos.Section(sectionToUpdate);
    }
}