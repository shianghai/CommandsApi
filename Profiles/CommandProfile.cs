using AutoMapper;
using CommandsApi.Dtos.Read;
using CommandsApi.Dtos.Write;
using CommandsApi.Models;


namespace CommandsApi.Profiles
{
    public class CommandProfile: Profile
    {
        public CommandProfile()
        {
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandReadDto, Command>();

            CreateMap<CommandWriteDto, Command>();
            CreateMap<Command, CommandWriteDto>();
        }

    }
}
