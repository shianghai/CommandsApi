using System.ComponentModel.DataAnnotations;

namespace CommandsApi.Dtos.Write
{
    public class CommandWriteDto
    {  
        public string HowTo { get; set; }
      
        public string Line { get; set; }

        public string Platform { get; set; }
    }
}
