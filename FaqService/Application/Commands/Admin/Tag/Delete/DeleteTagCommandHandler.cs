using FaqDataAccess.Repositories.TagRepository;
using FaqService.Application.Exceptions;
using MediatR;

namespace FaqService.Application.Commands.Admin.Tag.Delete;

/// <summary>
/// Обработчик команлда на удаление тэга
/// </summary>
public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
{
    private readonly ITagRepository _tagRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public DeleteTagCommandHandler(ITagRepository tagRepository) => _tagRepository = tagRepository;
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _tagRepository.GetTagByIdAsync(request.Id);

        if (tag == null!)
            throw new NoEntityException("No tag found");
        
        _tagRepository.DeleteTag(tag);
        await _tagRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}