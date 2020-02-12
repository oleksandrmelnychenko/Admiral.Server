using Admiral.Server.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Admiral.Server.Databases.TableMaps.Identity {
    public class UserIdentityMap : EntityBaseMap<UserIdentity> {
        public override void Map(EntityTypeBuilder<UserIdentity> entity) {
            base.Map(entity);
            entity.ToTable("UserIdentities");
            entity
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
