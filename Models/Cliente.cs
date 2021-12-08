﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XPTOOrcamentos.Models
{
    public class Cliente
    {
        [Key]
        public int ID { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public bool Ativo { get; set; }

    }
}
