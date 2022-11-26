using FaqDomain.Events;

namespace FaqDomain.Aggregates;

/// <summary>
/// Статья
/// </summary>
public class Article : Entity
{
    private string _question;
    private string _answer;

    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Вопрос
    /// </summary>
    public string Question
    {
        get => _question;
        private set
        {
            if (value.Length > 30)
                throw new DomainValidateException("Вопрос не может быть длиннее 30 символов");
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainValidateException("Вопрос должен содержать символы");

            _question = value;
        }
    }

    /// <summary>
    /// Ответ
    /// </summary>
    public string Answer
    {
        get => _answer;
        private set
        {
            if (value.Length > 100)
                throw new DomainValidateException("Ответ не может быть длиннее 100 символов");
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainValidateException("Ответ должен содержать символы");

            _answer = value;
        }
    }

    /// <summary>
    /// Родительская категория
    /// </summary>
    public Section? Parent { get; set; }

    /// <summary>
    /// Идентификатор родительской категории
    /// </summary>
    public int ParentId { get; set; }

    /// <summary>
    /// Позиция при выводе списка статей
    /// </summary>
    public int? OrderPosition { get; private set; }

    /// <summary>
    /// Тэги вопроса
    /// </summary>
    public IReadOnlyCollection<Tag>? Tags { get; private set; }

    /// <summary>
    /// ctor
    /// </summary>
    public Article(string question, string answer, Section parent, int? orderPosition = null)
    {
        Question = question;
        Answer = answer;
        SetParent(parent);
        SetOrderPosition(orderPosition, parent.Id);
        Tags = new List<Tag>();
    }

    /// <summary>
    /// ctor
    /// </summary>
    protected Article() { }

    /// <summary>
    /// Устанавливает родительскую категорию
    /// </summary>
    private void SetParent(Section parent)
    {
        var isParentRootSection = parent.ParentId == null;
        if (isParentRootSection)
            throw new DomainValidateException("Родительская категория не может быть корневой");

        ParentId = parent.Id;
    }

    /// <summary>
    /// Обновление доменного объекта
    /// </summary>
    public void Update(string question, string answer, Section parent, int? orderPosition)
    {
        Question = question;
        Answer = answer;

        var isNewParent = ParentId != parent.Id;
            
        if (isNewParent)
        {
            SetParent(parent);
            UpdatePositionsAfterRemove();
            SetOrderPosition(orderPosition, parent.Id);
        }

        if (OrderPosition != orderPosition && !isNewParent)
            UpdateOrderPosition(parent.Id, orderPosition);
    }
    
    /// <summary>
    /// Оболочка для вызова метода выравнивания списка статей после удаления текущей
    /// </summary>
    public void MarkForDelete() => UpdatePositionsAfterRemove();

    /// <summary>
    /// Устанавливает позицию статьи при выводе списка статей
    /// Создает событие по обновлению позиций статей той же родительской категории
    /// </summary>
    private void SetOrderPosition(int? orderPosition, int parentId)
    {
        AddDomainEvent(new ArticleAddedEvent(parentId, orderPosition));
        OrderPosition = orderPosition;
    }
    
    /// <summary>
    /// Устанавливает позицию статьи при выводе списка статей
    /// </summary>
    public void SetOrderPosition(int orderPosition) => OrderPosition = orderPosition;
    
    /// <summary>
    /// Обновляет позицию статьи при выводе списка статей
    /// Создает событие по обновлению позиций статей той же родительской категории
    /// </summary>
    private void UpdateOrderPosition(int parentId, int? orderPosition)
    {
        AddDomainEvent(new ArticleChangedOrderPositionEvent(Id, parentId, orderPosition));
        OrderPosition = orderPosition;
    }
    
    /// <summary>
    /// Создает событие по обновлению позиций статей той же родительской категории
    /// </summary>
    private void UpdatePositionsAfterRemove()
    { 
        AddDomainEvent(new ArticleRemovedFromSectionEvent(Id,ParentId, OrderPosition));
    }
    
    /// <summary>
    /// Устанавливает статье тэги
    /// </summary>
    public void SetTags(ICollection<int> tags, ICollection<Tag> existingTags)
    {
        if (tags.Count > 3)
            throw new DomainValidateException("Статья не может содержать более 3 тэгов");

        var isAllTagsExist = existingTags.Select(x => x.Id).Intersect(tags).Count() == tags.Count;

        if (!isAllTagsExist)
            throw new DomainValidateException("Одного или нескольких тэгов не существует");

        Tags = existingTags.Where(x => tags.Contains(x.Id)).ToList();
    }
}