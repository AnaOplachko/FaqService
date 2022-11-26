using FaqDataAccess.Repositories.ArticleRepository;
using FaqDataAccess.Repositories.TagRepository;
using FaqService.Application.Exceptions;
using MediatR;

namespace FaqService.Application.Commands.Admin.Article.SetTags;

/// <summary>
/// Обработчик команды на установку администратором тэгов статьи
/// </summary>
public class SetTagsCommandHandler : IRequestHandler<SetTagsCommand, Dtos.Article>
{
    private readonly IArticleRepository _articleRepository;
    private readonly ITagRepository _tagRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public SetTagsCommandHandler(IArticleRepository articleRepository, ITagRepository tagRepository)
    {
        _articleRepository = articleRepository;
        _tagRepository = tagRepository;
    }

    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<Dtos.Article> Handle(SetTagsCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetArticleByIdAsync(request.Id);
        if (article == null!)
            throw new NoEntityException("No article found");
            
        var existingTags = await _tagRepository.GetAllTagsAsync();
        
        article.SetTags(request.Tags, existingTags);
        
        _articleRepository.UpdateArticle(article);
        await _articleRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return new Dtos.Article(article);
    }
}