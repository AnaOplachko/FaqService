using FaqDataAccess.Repositories.TagRepository;
using FaqService.Application.Exceptions;
using MediatR;

namespace FaqService.Application.Commands.User.Tag.ReadAll;

/// <summary>
/// Обработчик запроса на получние пользователем всех тэгов
/// </summary>
public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, List<Dtos.Tag>>
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
    public async Task<List<Dtos.Tag>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _cachedTagRepository.GetAllTagsAsync();

        if (tags.Count == 0)
            throw new NoEntityException("No tags found");

        return tags.Select(x => new Dtos.Tag(x)).ToList();
    }
}