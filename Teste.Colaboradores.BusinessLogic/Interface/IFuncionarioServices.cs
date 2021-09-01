using FUNCIONARIOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Teste.Colaboradores.BusinessLogic.Repository;

namespace Teste.Colaboradores.BusinessLogic.Services
{
    public interface IFuncionarioServices
    {
        void inserir(Funcionario func);

        void alterar(Funcionario func);

        void excluir(int id);

        IList<Funcionario> Listar();

        IList<Funcionario> Listar(Expression<Func<Funcionario, bool>> predicate);

        Funcionario GetById(Expression<Func<Funcionario, bool>> predicate);
    }
}