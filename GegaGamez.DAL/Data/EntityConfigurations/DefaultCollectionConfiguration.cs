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
    internal class DefaultCollectionConfiguration : IEntityTypeConfiguration<DefaultCollection>
    {
        public void Configure(EntityTypeBuilder<DefaultCollection> builder)
        {
            builder.HasOne(d => d.DefaultCollectionType)
                    .WithMany(p => p.DefaultCollections)
                    .HasForeignKey(d => d.DefaultCollectionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DefaultCollection_DefaultCollectionType");

            builder.HasOne(d => d.User)
                .WithMany(p => p.DefaultCollections)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DefaultCollection_User");
        }
    }
}
