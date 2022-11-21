using FaqDataAccess.Repositories.TagRepository;
using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Tag.Create;

/// <summary>
/// Обработчик команды на создание тэга
/// </summary>
public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, TagModel>
{
    private readonly ITagRepository _tagRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public CreateTagCommandHandler(ITagRepository tagRepository) => _tagRepository = tagRepository;
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<TagModel> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var existingTags = await _tagRepository.GetAllTagsAsync();
        var newTag = new FaqDomain.Aggregates.Tag(request.Name, existingTags);
        
        _tagRepository.CreateTag(newTag);
        await _tagRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return new TagModel(newTag);
    }
}