using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace waTracking.Entities.Security
{
    public class SecurityUser
    {
        public int Id { get; set; }
        [Required]
        public int SecurityRolId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string Nombre { get; set; }
        public string Tipo_documento { get; set; }
        public string Num_documento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        [Required]
        public byte[] Password_hash { get; set; }
        [Required]
        public byte[] Password_salt { get; set; }

        public bool Condicion { get; set; }

        public SecurityRole Rol { get; set; }
    }
}
