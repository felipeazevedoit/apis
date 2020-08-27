using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class UsuarioPermissao : Base
    {
        public int idUsuario { get; set; }
        public int idAux { get; set; }
        public int idTipoPermissao { get; set; }
        public int idAcao { get; set; }
    }
}
