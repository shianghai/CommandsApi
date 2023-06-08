using CommandsApi.Dtos.Read;
using CommandsApi.Dtos.Write;
using CommandsApi.Models;

namespace CommandsApi.Data
{
    public interface ICommanderRepo
    {
        IEnumerable<Command> GetAppCommands();

        Command GetCommandById(int Id);

        bool SaveChanges();

        bool CreateCommand(Command commandWriteDto);

        void UpdateCommand(CommandWriteDto commandWriteDto);

        bool DeleteCommand(Command command);
    }
}
