using FaqDataAccess.Repositories.TagRepository;
using FaqService.Application.Exceptions;
using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.User.Tag.ReadAll;

/// <summary>
/// Обработчик запроса на получние пользователем всех тэгов
/// </summary>
public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, List<TagModel>>
{
    private readonly ICachedTagRepository _cachedTagRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public GetAllTagsQueryHandler(ICachedTagRepository cachedTagRepository)
        => _cachedTagRepository = cachedTagRepository;
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<List<TagModel>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _cachedTagRepository.GetAllTagsAsync();

        if (tags.Count == 0)
            throw new NoEntityException("No tags found");

        return tags.Select(x => new TagModel(x)).ToList();
    }
}