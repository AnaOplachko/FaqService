using AutoFixture.Xunit2;
using FaqDomain;
using FaqDomain.Aggregates;
using FluentAssertions;
using Xunit;

namespace FaqService.UnitTests;

public class TagTests
{
    /// <summary>
    /// При валидных данных успешно создает тэг
    /// </summary>
    [Fact]
    public void BeCreatedCorrectly()
    {
        //assert
        var name = "valid name";
        var existingTag = new List<Tag>();
        
        //act
        var tag = new Tag(name, existingTag);
        
        //assert
        tag.Name.Should().Be(name);
    }

    /// <summary>
    /// При некоректных данных бросает ошибку
    /// </summary>
    [Theory]
    [InlineAutoData("")]
    [InlineAutoData("Too long name. Too long name. Too long name. ")]
    public void ShouldNotBeCreatedWithInvalidName(string name)
    {
        //assert
        var existingTags = new List<Tag>();
        
        //act
        Func<Tag> createTagFunc = () => new Tag(name, existingTags);
        
        //assert
        Assert.Throws<DomainValidateException>(createTagFunc);
    }

    /// <summary>
    /// При повторном использовании имени бросает ошибку
    /// </summary>
    [Fact]
    public void ShouldNotBeCreatedWhenNameNonUnique()
    {
        //assert
        var name = "valid name";
        var tags = new List<Tag>();
        var tag = new Tag(name, tags);
        tags.Add(tag);
        
        //act
        Func<Tag> createTagFunc = () => new Tag(name, tags);
        
        //assert
        Assert.Throws<DomainValidateException>(createTagFunc);
    }
}