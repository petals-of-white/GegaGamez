using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GegaGamez.DAL.Data.EntityConfigurations
{
    internal class DefaultCollectionTypeConfiguration : IEntityTypeConfiguration<DefaultCollectionType>
    {
        public void Configure(EntityTypeBuilder<DefaultCollectionType> builder)
        {

        }
    }
}
