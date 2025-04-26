using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FacturacionForm.entidades;

namespace FacturacionForm.Controladores
{
    public  class ProcesarDTECF
    {

        public Emisor Emisor { get; set; }
        public Receptor Receptor { get; set; }
        public List<DetalleVenta> DetallesVenta { get; set; }


        public ProcesarDTECF(List<DetalleVenta> detalles,Emisor emisor, Receptor receptor)
        {
            Emisor = emisor;
            Receptor = receptor;
            DetallesVenta = detalles;
            
        }

        public bool EnviarDTE() {


            //Convertir JSON

            // Convertir el objeto a JSON
            string jsonString = JsonSerializer.Serialize(Emisor);


            return true;
        }

    }
}
