using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("/csv")]
    public class CsvController : ControllerBase
    {        
        private readonly ToDoService _service;

        public CsvController(ToDoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var items = await _service.GetToDosAsync();
            // TODO  - return a CSV of the items
            return  ""; 
        }
    }
}
