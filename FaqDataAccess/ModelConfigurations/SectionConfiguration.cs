using FaqDomain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaqDataAccess.ModelConfigurations;

/// <summary>
/// Конфигурация сущности категория в контексте
/// </summary>
public class SectionConfiguration : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.HasKey(section => section.Id);

        builder.Property(section => section.Name).IsRequired();

        builder.HasOne(section => section.Parent)
            .WithMany(section => section.Subsections)
            .HasForeignKey(section => section.ParentId);

        builder.HasMany(section => section.Articles)
            .WithOne(article=>article.Parent)
            .HasForeignKey(article=>article.ParentId);
        
        builder.Ignore(section => section.DomainEvents);
    }
}