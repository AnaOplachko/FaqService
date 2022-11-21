using FaqDomain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaqDataAccess.ModelConfigurations;

/// <summary>
/// Конфигурация сущности статья в контексте
/// </summary>
public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(article => article.Id);

        builder.Property(article => article.Question).IsRequired();
        builder.Property(article => article.Answer).IsRequired();
        builder.Property(article => article.OrderPosition);

        builder.Property(article => article.ParentId).IsRequired();
        builder.HasOne(article => article.Parent)
            .WithMany(section => section.Articles)
            .HasForeignKey(article => article.ParentId);

        builder.HasMany(article => article.Tags)
            .WithMany(tag => tag.Articles);
        
        builder.HasIndex(article => article.Question);
        builder.HasIndex(article => article.Answer);
        
        builder.Ignore(article => article.DomainEvents);
    }
}