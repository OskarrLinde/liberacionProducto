
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberacionProducto.Bus.Services.Lotificacion
{
    public class LoteService
    {     
        public LoteService() 
        { 
        }

        public string GeneraNuevoLote(string idProducto, string descProducto, int idPlanta, string descPlanta, string idTanque)
        {
            string nuevoIdLote = string.Empty;
            string fechaFormateada = DateTime.Now.ToString("MMddyy");
            string horaFormateada = DateTime.Now.ToString("HHmm");
            

            nuevoIdLote = descProducto + '-' + descPlanta + "-" + fechaFormateada + '-' + descPlanta + '-' + idTanque + '-' + horaFormateada;

            return nuevoIdLote;
        }
    }
}
