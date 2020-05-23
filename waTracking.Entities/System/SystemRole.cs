using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace waTracking.Entities.System
{
    public class SystemRole
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 30 caracteres, ni menos de 3 caracteres.")]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }
        public bool Enabled { get; set; }

    }
}
