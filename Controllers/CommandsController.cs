using AutoMapper;
using Azure;
using CommandsApi.Attributes;
using CommandsApi.Data;
using CommandsApi.Dtos.Read;
using CommandsApi.Dtos.Write;
using CommandsApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CommandsApi.Controllers
{
    [ApiKeyAuth]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repo;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<CommandReadDto>> GetAllCommands()
        {
            var result = _repo.GetAppCommands();
            var mappedResult = _mapper.Map<List<CommandReadDto>>(result);
            return Ok(mappedResult);
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            try
            {
                var result = _repo.GetCommandById(id);
               
                return Ok(_mapper.Map<CommandReadDto>(result));
               
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand([FromBody] CommandWriteDto commandWriteDto)
        {
            var command = _mapper.Map<Command>(commandWriteDto);
            var created = _repo.CreateCommand(command);
            if (created)
            {
                var commandReadDto = _mapper.Map<CommandReadDto>(command);
                return CreatedAtRoute("GetCommandById",  new { Id = commandReadDto }, commandReadDto);
            }
            return StatusCode(500);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandWriteDto commandWriteDto)
        {
            var repoCommand = _repo.GetCommandById(id);

            if(repoCommand == null)
            {
                return NotFound();
            }

            //mapping makes commandWriteDto reflect in the db context
            _mapper.Map(commandWriteDto, repoCommand); 

            //call the update method by convention
            _repo.UpdateCommand(commandWriteDto);

            _repo.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandWriteDto> jsonPatch)
        {
            var repoCommand = _repo.GetCommandById(id);

            if (repoCommand == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandWriteDto>(repoCommand);

            jsonPatch.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, repoCommand);

            _repo.UpdateCommand(commandToPatch);

            _repo.SaveChanges();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var repoCommand = _repo.GetCommandById(id);
            if( repoCommand == null)
            {
                return NotFound();
            }
            var deleted = _repo.DeleteCommand(repoCommand);
            if (deleted)
            {
                return NoContent();
            }
            return StatusCode(500);
        }
        //TODO: implement search with auto complete feature.
    }
}
