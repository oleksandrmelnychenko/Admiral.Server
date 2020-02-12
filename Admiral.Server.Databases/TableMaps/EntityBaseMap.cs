using Admiral.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Admiral.Server.Databases.TableMaps {
    public abstract class EntityBaseMap<T> : EntityTypeConfiguration<T> where T : EntityBase {
        public override void Map(EntityTypeBuilder<T> entity) {
            entity.Property(e => e.Id).HasColumnName("Id");

            entity.Property(e => e.Created).HasDefaultValueSql("getutcdate()");
        }
    }
}
