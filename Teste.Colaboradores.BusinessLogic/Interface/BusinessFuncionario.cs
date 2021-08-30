using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Colaboradores.BusinessLogic.Interface
{
    public class BusinessFuncionario
    {
        public async Task<IActionResult> DespesasSalColaborador()
        {
            var funcionario = new Funcionario();
            funcionario.TotalSalarios = await _context.Funcionarios.OrderByDescending(o => o.Salario).ToListAsync();
            ViewBag.TotalSalarios = string.Format("{0:c}", funcionario.TotalSalarios.Sum(w => w.Salario));

        }
    }
}
