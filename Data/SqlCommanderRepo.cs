using AutoMapper;
using CommandsApi.Dtos.Read;
using CommandsApi.Dtos.Write;
using CommandsApi.Models;

namespace CommandsApi.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        /// <summary>
        /// using the commanderdbcontext to directly access the database. An alternative is the repository pattern
        /// </summary>
        private readonly CommanderDbContext _context;


        public SqlCommanderRepo(CommanderDbContext context) 
        {
            _context = context;
        }

        public bool CreateCommand(Command command)
        {
            _context.Commands.Add(command);
            return SaveChanges();
        }

        public bool DeleteCommand(Command command)
        {
           if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
           _context.Commands.Remove(command);

            return SaveChanges();
        }

        public IEnumerable<Command> GetAppCommands()
        {
            return _context.Commands.ToList();  
        }

        public Command GetCommandById(int Id)
        {
            
           var result  =  _context.Commands.FirstOrDefault(c => c.Id == Id);
           if(result == null)
           {
                throw new KeyNotFoundException($"No Matching Records Found For Id: {Id}");
           }
           return result;
     
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(CommandWriteDto commandWriteDto)
        {
            //Not implemented
        }
    }
}
