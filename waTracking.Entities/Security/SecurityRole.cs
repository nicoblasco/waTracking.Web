using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace waTracking.Entities.Security
{
   public class SecurityRole
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 30 caracteres, ni menos de 3 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(256)]
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }

        public ICollection<SecurityRole> usuarios { get; set; }
    }
}
