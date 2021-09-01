using FUNCIONARIOS.Data;
using FUNCIONARIOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Teste.Colaboradores.BusinessLogic.Interface;

namespace Teste.Colaboradores.BusinessLogic.Repository
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly EmpresaContexto _context;

        public FuncionarioRepository(EmpresaContexto context)
        {
            _context = context;
        }

        public void inserir(Funcionario func)
        {
            _context.Add(func);
            _context.SaveChanges();
        }

        public void alterar(Funcionario func)
        {
            _context.Update(func);
            _context.SaveChanges();
        }

        public void excluir(int id)
        {
            var funcionario = _context.Funcionarios                 
                                      .FirstOrDefault(m => m.IdFuncionario == id);
            
            
            _context.Funcionarios.Remove(funcionario);
            _context.SaveChanges();
        }

        public Funcionario PegarFuncionario(Expression<Func<Funcionario, bool>> predicate)
        {
            return _context.Funcionarios.FirstOrDefault(predicate);
        }

        public IList<Funcionario> Listar()
        {
            var lista = from func in _context.Funcionarios
                        select func;

            return lista.ToList();
        }

        public IList<Funcionario> Listar(Expression<Func<Funcionario, bool>> predicate)
        {
            return _context.Funcionarios.Where(predicate).ToList();
        }

    }
}