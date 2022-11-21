using AutoFixture.Xunit2;
using FaqDomain;
using FaqDomain.Aggregates;
using FluentAssertions;
using Xunit;

namespace FaqService.UnitTests;

public class SectionTests
{
    /// <summary>
    /// При корректных данных создает категорию
    /// </summary>
    [Fact]
    public void BeCreatedCorrectly()
    {
        //assert
        var name = "valid name";
        var parentId = 1;
          
        //act
        var section = new Section(name, parentId);
          
        //assert
        section.Name.Should().Be(name);
        section.ParentId.Should().Be(parentId);
    }

    /// <summary>
    /// При некорректных данных бросает ошибку
    /// </summary>
    [Theory]
    [InlineAutoData("")]
    [InlineAutoData("Too long name. Too long name. Too long name. ")]
    public void ShouldNotBeCreatedWithInvalidName(string name, int id)
    {
        //act
        Func<Section> createSectionFunc = () => new Section(name, id);
          
        //assert
        Assert.Throws<DomainValidateException>(createSectionFunc);
    }
}