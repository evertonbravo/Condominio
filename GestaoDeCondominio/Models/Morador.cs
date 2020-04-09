using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeCondominio.Models
{
    public class Morador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public int IdApartamento { get; set; }

        public ICollection<Apartamento> Apartamentos { get; set; }
    }
}
