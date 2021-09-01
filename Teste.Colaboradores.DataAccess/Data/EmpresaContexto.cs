using FUNCIONARIOS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FUNCIONARIOS.Data
{
    public class EmpresaContexto : DbContext
    {
        public EmpresaContexto(DbContextOptions<EmpresaContexto> options) : base(options)
        {

        }        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Funcionario>().ToTable("Funcionario");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                var connectionString = AppConfiguration.ConnectionString;// AppConfiguration.GetConnectionString();

                dbContextOptionsBuilder.UseSqlServer(connectionString);//;
                                       //Configuration.GetConnectionString("ConexaoMySql:MySqlConnectionString"));
            }
        }

        public DbSet<Funcionario> Funcionarios { get; set; }

    }
}