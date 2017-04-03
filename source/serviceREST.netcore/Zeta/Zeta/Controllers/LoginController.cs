using Microsoft.AspNetCore.Mvc;
using NG.Interfaces;
using NG.Models;
using NG.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Zeta.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IStudent _student;
        private Message _message;
        public LoginController(IStudent istu, Message message)
        {
            _student = istu;
            _message = message;
        }

        // POST api/values
        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody]LoginVM login)
        {
            if (ModelState.IsValid)
            {
                // Todo is ok
                _message = await Task.Run(() => _student.Login(login));

                if (_message.status_item)
                    return Ok(_message.details);
                else
                    return StatusCode(401, _message.details);
            }
            else
            {
                var mes = string.Join(" | ", ModelState.Values
                       .SelectMany(v => v.Errors)
                       .Select(e => e.ErrorMessage));

                _message.details = mes;
                return BadRequest(_message.Res());
            }
        }

         
    }
}
