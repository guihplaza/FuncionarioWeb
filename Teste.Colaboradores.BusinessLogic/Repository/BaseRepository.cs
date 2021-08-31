using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Colaboradores.BusinessLogic.Repository
{
    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        ConexaoBanco _context;
        public BaseRepository(ConexaoBanco context)
        {
            _context = context;
        }
    }
}
