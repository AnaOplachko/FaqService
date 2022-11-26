using FaqDataAccess.Repositories.ArticleRepository;
using FaqDataAccess.Repositories.TagRepository;
using FaqService.Application.Exceptions;
using FaqService.Application.Dtos;
using MediatR;

namespace FaqService.Application.Commands.User.Tag.ReadById;

/// <summary>
/// Обработчик запроса на получние пользователем тэга по идентификатору
/// </summary>
public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, TagWithArticles>
{
    private readonly ICachedTagRepository _cachedTagRepository;
    private readonly ICachedArticleRepository _cachedArticleRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public GetTagByIdQueryHandler(ICachedArticleRepository cachedArticleRepository, ICachedTagRepository cachedTagRepository)
    {
        _cachedArticleRepository = cachedArticleRepository;
        _cachedTagRepository = cachedTagRepository;
    }
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<TagWithArticles> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var tag = await _cachedTagRepository.GetTagByIdAsync(request.Id);

        if (tag == null!)
            throw new NoEntityException("No tag found");
        
        var articles = await _cachedArticleRepository.GetAllArticlesAsync();

        var tagArticles = articles.Where(x => x.Tags!.Contains(tag)).ToList();

        return new TagWithArticles(tag, tagArticles);
    }
}