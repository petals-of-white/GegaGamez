using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GegaGamez.DAL.Data.EntityConfigurations
{
    internal class UserCollectionConfiguration : IEntityTypeConfiguration<UserCollection>
    {
        public void Configure(EntityTypeBuilder<UserCollection> builder)
        {
            builder.HasOne(d => d.User)
                .WithMany(p => p.UserCollections)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCollection_User");
        }
    }
}
