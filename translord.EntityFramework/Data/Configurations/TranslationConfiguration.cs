using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using translord.Models;

namespace translord.EntityFramework.Data.Configurations;

internal class TranslationConfiguration : IEntityTypeConfiguration<Translation>
{
    public void Configure(EntityTypeBuilder<Translation> builder)
    {
        builder.HasKey(x => x.Key);
        builder.Property(x => x.Key).ValueGeneratedNever();
    }
}