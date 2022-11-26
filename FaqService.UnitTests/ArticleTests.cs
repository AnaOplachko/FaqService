using AutoFixture.Xunit2;
using FaqDomain;
using FaqDomain.Aggregates;
using FluentAssertions;
using Xunit;

namespace FaqService.UnitTests;

public class ArticleShould
{
    /// <summary>
    /// При корректных данных создает статью
    /// </summary>
    [Fact]
    public void BeCreatedCorrectly()
    {
        //arrange
        var answer = "no one";
        var question = "who is john golt?";
        var parent = new Section { Name = "section", Id = 1, ParentId = 3};

        //act
        var article = new Article(question, answer, parent);

        //assert
        article.Question.Should().Be(question);
        article.Answer.Should().Be(answer);
        article.ParentId.Should().Be(parent.Id);
    }

    /// <summary>
    /// При некорректном ответе бросает ошибку
    /// </summary>
    [Theory]
    [InlineAutoData("")]
    [InlineAutoData(
        "Too long answer. Too long answer. Too long answer. Too long answer. Too long answer. Too long answer. ")]
    public void NotBeCreatedWhenAnswerIsNotValid(string answer)
    {
        //arrange
        var question = "valid question";
        var parent = new Section { Name = "section", Id = 1, ParentId = 3};
        
        //act
        Func<Article> createArticleFunc = () => new Article(question, answer, parent);
        
        //assert
        Assert.Throws<DomainValidateException>(createArticleFunc);
    }

    /// <summary>
    /// При некорректном вопросе бросает ошибку
    /// </summary>
    /// <param name="question"></param>
    [Theory]
    [InlineAutoData("")]
    [InlineAutoData("Too long question. Too long question. ")]
    public void NotBeCreatedWhenQuestionIsNotValid(string question)
    {
        //arrange
        var answer = "valid answer";
        var parent = new Section { Name = "section", Id = 1, ParentId = 3};

        //act
        Func<Article> createArticleFunc = () => new Article(question, answer, parent);

        //assert
        Assert.Throws<DomainValidateException>(createArticleFunc);
    }

    /// <summary>
    /// Бросает ошибку при некорректном родителе
    /// </summary>
    [Fact]
    public void NotBeCreatedWhenParentIsRoot()
    {
        //arrange
        var answer = "no one";
        var question = "who is john golt?";
        var parent = new Section { Name = "section", Id = 1, ParentId = null};
        
        //act
        Func<Article> createArticleFunc = () => new Article(question, answer, parent);

        //assert
        Assert.Throws<DomainValidateException>(createArticleFunc);
    }
}