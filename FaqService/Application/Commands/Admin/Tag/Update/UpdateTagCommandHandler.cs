using FaqDataAccess.Repositories.TagRepository;
using FaqService.Application.Exceptions;
using MediatR;

namespace FaqService.Application.Commands.Admin.Tag.Update;

/// <summary>
/// Обработчик команды на измененение тэга
/// </summary>
public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, Dtos.Tag>
{
    private readonly ITagRepository _tagRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public UpdateTagCommandHandler(ITagRepository tagRepository) => _tagRepository = tagRepository;
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<Dtos.Tag> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var existingTags = await _tagRepository.GetAllTagsAsync();

        var tag = existingTags.FirstOrDefault(x => x.Id == request.Id);

        if (tag! == null!)
            throw new NoEntityException("No tag found");
        
        tag.Update(request.Name, existingTags);
        
        _tagRepository.UpdateTag(tag);
        await _tagRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new Dtos.Tag(tag);
    }
}