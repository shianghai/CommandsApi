using CommandsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsApi.Data
{
    public class CommanderDbContext: DbContext
    {
        public CommanderDbContext(DbContextOptions<CommanderDbContext> opt): base(opt)
        {
            
        }

        public DbSet<Command> Commands { get; set; }
    }
}
