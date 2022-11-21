using FaqDataAccess.Repositories.ArticleRepository;
using FaqDataAccess.Repositories.TagRepository;
using FaqService.Application.Exceptions;
using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Tag.ReadById;

/// <summary>
/// Обработчик запроса на получение тэга по идентификатору
/// </summary>
public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, TagWithArticles>
{
    private readonly IArticleRepository _articleRepository;
    private readonly ITagRepository _tagRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public GetTagByIdQueryHandler(IArticleRepository articleRepository, ITagRepository tagRepository)
    {
        _articleRepository = articleRepository;
        _tagRepository = tagRepository;
    }

    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<TagWithArticles> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var tag = await _tagRepository.GetTagByIdAsync(request.Id);

        if (tag == null!)
            throw new NoEntityException("No tag found");

        var articles = await _articleRepository.GetAllArticlesAsync();
        
        var tagArticles = articles.Where(article => article.Tags!.Any(x => x.Id == request.Id)).ToList();

        return new TagWithArticles(tag, tagArticles);
    }
}