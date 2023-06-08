using CommandsApi.Dtos.Write;
using CommandsApi.Models;

namespace CommandsApi.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public bool CreateCommand(Command commandWriteDto)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Command> GetAppCommands()
        {
            return new List<Command>()
            {
                new Command()
                {
                    Id = 4,
                    HowTo = "cook an egg",
                    Platform = "kitchen",
                    Line = " Place egg on fire",
                },
                new Command()
                {
                    Id = 5,
                    HowTo = "boil water",
                    Platform = "Kitchen",
                    Line = "Place Water on fire"
                }
        };
        }

        public Command GetCommandById(int Id)
        {
            return new Command()
            {
                Id = Id,
                HowTo = "cook an egg",
                Platform = "kitchen",
                Line = " Place egg on fire",
            };
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(CommandWriteDto commandWriteDto)
        {
            throw new NotImplementedException();
        }
    }
}
