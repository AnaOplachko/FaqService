using FaqDataAccess.Repositories.ArticleRepository;
using FaqDataAccess.Repositories.SectionRepository;
using FaqService.Application.Exceptions;
using MediatR;

namespace FaqService.Application.Commands.Admin.Article.Create;

/// <summary>
/// Обработчик команды на создание новой статьи
/// </summary>
public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Dtos.Article>
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
    public async Task<Dtos.Article> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var parent = await _sectionRepository.GetSectionByIdAsync(request.ParentId);

        if (parent == null!)
            throw new NoEntityException("По идентификатору родительской категории ничего не найдено");
        
        var newArticle =
            new FaqDomain.Aggregates.Article(request.Question, request.Answer, parent, request.OrderPosition);
        
        _articleRepository.CreateArticle(newArticle);
        await _articleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return new Dtos.Article(newArticle);
    }
}