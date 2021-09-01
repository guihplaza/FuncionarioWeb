using FUNCIONARIOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Colaboradores.BusinessLogic.Interface
{
    public interface IFuncionarioRepository
    {
        void inserir(Funcionario func);

        void alterar(Funcionario func);

        void excluir(int id);

        IList<Funcionario> Listar();

        IList<Funcionario> Listar(Expression<Func<Funcionario, bool>> predicate);
    }
}