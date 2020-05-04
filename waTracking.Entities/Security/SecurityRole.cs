using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using waTracking.Entities.Configuration;

namespace waTracking.Entities.Security
{
   public class SecurityRole
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 30 caracteres, ni menos de 3 caracteres.")]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }
        public bool Enabled { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public ICollection<SecurityUser> Usuarios { get; set; }
    }
}
