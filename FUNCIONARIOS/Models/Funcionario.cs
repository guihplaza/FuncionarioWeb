using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FUNCIONARIOS.Models
{
    public class Funcionario
	{
        [Key]
        public int IdFuncionario { get; set; }
        
        [Required(ErrorMessage = "O campo Valor deve ser preenchido")]
        public string Nome { get; set; }
        public string Sexo { get; set; }

        public int Pis { get; set; }
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo Valor deve ser preenchido")]
        [DataType(DataType.Currency)]
        public int Salario { get; set; }

        public string Email { get; set; }
        public DateTime Data_admissao { get; set; }

        [NotMapped]
        public IEnumerable<Funcionario> TotalSalarios { get; set; }


        public static List<Funcionario> GetSexos()
        {
            var listaSexo = new List<Funcionario>()
            {
                new Funcionario(){  Sexo="Masculino"},
                new Funcionario(){  Sexo="Feminino"},
                new Funcionario(){  Sexo="Outros"},
                
            };
            return listaSexo;
        }
    }
}
