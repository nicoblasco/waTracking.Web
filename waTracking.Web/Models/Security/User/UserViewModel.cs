using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace waTracking.Web.Models.Security.User
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public int SecurityRolId { get; set; }

        public string Rol { get; set; }
        public string Nombre { get; set; }
        public string Tipo_documento { get; set; }
        public string Num_documento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public byte[] Password_hash { get; set; }

        public bool Condicion { get; set; }
    }
}
