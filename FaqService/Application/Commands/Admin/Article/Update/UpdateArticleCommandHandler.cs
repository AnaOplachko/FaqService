using FaqDataAccess.Repositories.ArticleRepository;
using FaqDataAccess.Repositories.SectionRepository;
using FaqService.Application.Exceptions;
using MediatR;

namespace FaqService.Application.Commands.Admin.Article.Update;

/// <summary>
/// Обработчик команды на обновление статьи
/// </summary>
public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Dtos.Article>
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
    public async Task<Dtos.Article> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var articleToUpdate = await _articleRepository.GetArticleByIdAsync(request.Id);
        if (articleToUpdate == null!)
            throw new NoEntityException("No article found");

        var parent = await _sectionRepository.GetSectionByIdAsync(request.ParentId);

        //Проверка родителя на существование
        if (parent == null!)
            throw new NoEntityException("По идентификатору родительской категории ничего не найдено");
        
        articleToUpdate.Update(request.Question, request.Answer, parent, request.OrderPosition);

        _articleRepository.UpdateArticle(articleToUpdate);
        await _articleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return new Dtos.Article(articleToUpdate);
    }
}