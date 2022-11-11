using System;
using System.Collections.Generic;

namespace DBFirstDAL.Modelo
{
    public partial class Empleado
    {
        public int Id { get; set; }
        public string NombreEmpleado { get; set; } = null!;
        public int? NivelAccId { get; set; }

        public virtual NivelAcceso? NivelAcc { get; set; }
    }
}
