using FaqDataAccess.Repositories.ArticleRepository;
using FaqDataAccess.Repositories.SectionRepository;
using FaqService.Application.Exceptions;
using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Article.Update;

/// <summary>
/// Обработчик команды на обновление статьи
/// </summary>
public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, ArticleModel>
{
    private readonly IArticleRepository _articleRepository;
    private readonly ISectionRepository _sectionRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public UpdateArticleCommandHandler(IArticleRepository articleRepository, ISectionRepository sectionRepository)
    {
        _articleRepository = articleRepository;
        _sectionRepository = sectionRepository;
    }

    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<ArticleModel> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var articleToUpdate = await _articleRepository.GetArticleByIdAsync(request.Id);
        if (articleToUpdate == null!)
            throw new NoEntityException("No article found");

        var parent = await _sectionRepository.GetSectionByIdAsync(request.ParentId);

        //Проверка родителя на существование
        if (parent == null!)
            throw new NoEntityException("По идентификатору родительской категории ничего не найдено");

        //Проверка на положение родителя в корне
        if (!parent.ParentId.HasValue)
            throw new InvalidEntityException("Статья не может быть прикреплена к корневой категории");
        
        articleToUpdate.Update(request.Question, request.Answer, request.ParentId, parent, request.OrderPosition);

        _articleRepository.UpdateArticle(articleToUpdate);
        await _articleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return new ArticleModel(articleToUpdate);
    }
}