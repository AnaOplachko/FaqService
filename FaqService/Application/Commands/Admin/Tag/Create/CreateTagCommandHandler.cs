using FaqDataAccess.Repositories.TagRepository;
using MediatR;

namespace FaqService.Application.Commands.Admin.Tag.Create;

/// <summary>
/// Обработчик команды на создание тэга
/// </summary>
public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Dtos.Tag>
{
    private readonly ITagRepository _tagRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public CreateTagCommandHandler(ITagRepository tagRepository) => _tagRepository = tagRepository;
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<Dtos.Tag> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var existingTags = await _tagRepository.GetAllTagsAsync();
        var newTag = new FaqDomain.Aggregates.Tag(request.Name, existingTags);
        
        _tagRepository.CreateTag(newTag);
        await _tagRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return new Dtos.Tag(newTag);
    }
}