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
    public class FuncionarioServices : IFuncionarioServices
    {
        FuncionarioRepository _repositorio;
        public FuncionarioServices(FuncionarioRepository funcionarioRepository)
        {
            _repositorio = funcionarioRepository;
        }

        public void inserir(Funcionario func)
        {
            _repositorio.inserir(func);
        }

        public void alterar(Funcionario func)
        {
            _repositorio.alterar(func);
        }

        public void excluir(int id)
        {
            _repositorio.excluir(id);
        }

        public IList<Funcionario> Listar()
        {
            return _repositorio.Listar();
        }

        public IList<Funcionario> Listar(Expression<Func<Funcionario, bool>> predicate)
        {
            return _repositorio.Listar(predicate);
        }

        public Funcionario GetById(Expression<Func<Funcionario, bool>> predicate)
        {
            return _repositorio.PegarFuncionario(predicate);
        }
    }
}