using FaqDataAccess.Repositories.TagRepository;
using FaqService.Application.Exceptions;
using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Tag.ReadAll;

/// <summary>
/// Обработчик запроса на получение всех тэгов
/// </summary>
public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, List<TagModel>>
{
    private readonly ITagRepository _tagRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public GetAllTagsQueryHandler(ITagRepository tagRepository) => _tagRepository = tagRepository;
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<List<TagModel>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _tagRepository.GetAllTagsAsync();

        if (tags.Count is 0)
            throw new NoEntityException("No tags found");

        return tags.Select(x => new TagModel(x)).ToList();
    }
}