using FaqDataAccess.Repositories.ArticleRepository;
using FaqDataAccess.Repositories.SectionRepository;
using FaqService.Application.Exceptions;
using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Article.Create;

/// <summary>
/// Обработчик команды на создание новой статьи
/// </summary>
public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ArticleModel>
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IArticleRepository _articleRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public CreateArticleCommandHandler(ISectionRepository sectionRepository, IArticleRepository articleRepository)
    {
        _sectionRepository = sectionRepository;
        _articleRepository = articleRepository;
    }

    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<ArticleModel> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var parent = await _sectionRepository.GetSectionByIdAsync(request.ParentId);

        if (parent == null!)
            throw new NoEntityException("По идентификатору родительской категории ничего не найдено");

        var isParentRootSection = parent.ParentId == null;
        if (isParentRootSection)
            throw new InvalidEntityException("Родительская категория не может быть корневой");
            
        var newArticle =
            new FaqDomain.Aggregates.Article(request.Question, request.Answer, request.ParentId, request.OrderPosition);
        
        _articleRepository.CreateArticle(newArticle);
        await _articleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return new ArticleModel(newArticle);
    }
}