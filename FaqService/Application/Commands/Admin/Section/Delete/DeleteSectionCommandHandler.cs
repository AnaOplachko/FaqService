using FaqDataAccess.Repositories.SectionRepository;
using FaqService.Application.Exceptions;
using MediatR;

namespace FaqService.Application.Commands.Admin.Section.Delete;

/// <summary>
/// Обработчик команды на удалении категории
/// </summary>
public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand>
{
    private readonly ISectionRepository _sectionRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public DeleteSectionCommandHandler(ISectionRepository sectionRepository)
        => _sectionRepository = sectionRepository;

    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<Unit> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
    {
        var section = await _sectionRepository.GetSectionByIdAsync(request.Id);

        if (section == null!)
            throw new NoEntityException("No section found");

        _sectionRepository.DeleteSection(section);
        await _sectionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}