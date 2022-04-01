using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GegaGamez.DAL.Data.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(d => d.Country)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_User_Country");
        }
    }
}