using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XPTOOrcamentos.Models
{
    public class PrestadorServico
    {
        [Key]
        public int ID { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public bool Ativo { get; set; }
    }
}
