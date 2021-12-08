using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace XPTOOrcamentos.Models
{
    public class OrdemServico
    {
        [Key]
        public int ID { get; set; }
        public string TituloServico { get; set; }
        public int ClienteID { get; set; }
        [ForeignKey("ClienteID")]
        public Cliente Cliente { get; set; }
        public int PrestadorID { get; set; }
        [ForeignKey("PrestadorID")]

        public PrestadorServico Prestador { get; set; }
        public DateTime DataExecucao { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }
    }
}
