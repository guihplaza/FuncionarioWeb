using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FUNCIONARIOS.Data
{
    public class DbInitializer
    {
        public static void Initialize(EmpresaContexto context)
        {
            context.Database.EnsureCreated();

            
            if (context.Funcionarios.Any())
            {
                return;  
            }
        }
    }
}
