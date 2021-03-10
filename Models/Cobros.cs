using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial2_AP2_AndyLanfranco.Models
{
    public class Cobros
    {
        [Key]
        public int CobroId { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public double TotalCobrado { get; set; }
        public string Observaciones { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Clientes Cliente { get; set; }

        [ForeignKey("CobroId")]
        public virtual List<CobrosDetalle> CobrosDetalle { get; set; }


    }

}
