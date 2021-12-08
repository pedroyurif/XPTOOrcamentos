using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XPTOOrcamentos.Models;

namespace XPTOOrcamentos
{
    public class DB : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<PrestadorServico> PrestadoresServico { get; set; }
        public DbSet<OrdemServico> OrdensServico { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);

            var connection = @"Server=botiq.cz6gnjb0yrml.us-east-1.rds.amazonaws.com,1433;Database=XPTOOrcamentos;user id=botiqadmin;password=f546PMw7y4DkE7T;Trusted_Connection=False;MultipleActiveResultSets=true;";

            optionsBuilder.UseSqlServer(connection);
        }
    }
}
