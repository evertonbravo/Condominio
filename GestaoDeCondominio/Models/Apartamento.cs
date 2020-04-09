using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeCondominio.Models
{
    public class Apartamento
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Bloco { get; set; }
        public int MoradorId { get; set; }        
        public Morador Morador { get; set; }

    }
}
