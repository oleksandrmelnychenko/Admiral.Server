﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Admiral.Server.Databases.TableMaps {
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}
