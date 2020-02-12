using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admiral.Server.Domain.Entities {
    public class EntityBase {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public bool IsDeleted { get; set; }

        /// <summary>
        /// Date and time that the record was created (automatically set on creation).
        /// </summary>
        public DateTime? Created { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Date and time that the record was last modified (automatically set on update).
        /// </summary>
        public DateTime? LastModified { get; set; }

        public virtual bool IsNew() => Id.Equals(0);
    }
}
