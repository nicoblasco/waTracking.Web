using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using waTracking.Entities.Configuration;
using waTracking.Entities.System;

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
        public int SystemRoleId { get; set; }
        public virtual Company Company { get; set; }
        public virtual SystemRole SystemRole { get; set; }

        public ICollection<SecurityUser> Usuarios { get; set; }
        public ICollection<SecurityRoleAction> SecurityRoleActions { get; set; }
        public ICollection<SecurityRoleScreen> SecurityRoleScreens { get; set; }
    }
}
