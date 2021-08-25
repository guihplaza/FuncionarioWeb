using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FUNCIONARIOS.Models
{
    public class Funcionario
	{
        [Key]
        public int IdFuncionario { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public int Pis { get; set; }
        public string CPF { get; set; }
        public double Salario { get; set; }
        public string Email { get; set; }
        public DateTime Data_admissao { get; set; }

    }
}
