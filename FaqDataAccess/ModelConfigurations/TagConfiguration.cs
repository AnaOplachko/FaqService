using FaqDomain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaqDataAccess.ModelConfigurations;

/// <summary>
/// Конфигурации сущности тэг в контесте
/// </summary>
public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(tag => tag.Id);

        builder.Property(tag => tag.Name).IsRequired();

        builder.HasMany(tag => tag.Articles)
            .WithMany(article => article.Tags);
        
        builder.Ignore(tag => tag.DomainEvents);
    }
}