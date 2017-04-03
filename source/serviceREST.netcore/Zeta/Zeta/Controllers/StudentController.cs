using Microsoft.AspNetCore.Mvc;
using NG.Interfaces;
using NG.Models;
using NG.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zeta.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private IStudent _student;
        private Message _message;
        public StudentController(IStudent ist, Message mess)
        {
            _student = ist;
            _message = mess;
        }


        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody]StudentVM objstudent)
        {
            if (ModelState.IsValid)
            {
                // Todo is ok
                _message = await Task.Run(() => _student.Create(objstudent));

                if (_message.status_item)
                    return Ok(_message.details);
                else
                    return StatusCode(401, _message.Res());
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


        [HttpPut]
        [Produces("application/json")]
        public async Task<IActionResult> Put([FromBody]StudentVM objstudent)
        {
            if (ModelState.IsValid)
            {
                // Todo is ok
                _message = await Task.Run(() => _student.Update(objstudent));

                if (_message.status_item)
                    return Ok(_message.details);
                else
                    return StatusCode(401, _message.Res());
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


        //[HttpDelete("{id}")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                // Todo is ok
                _message = await Task.Run(() => _student.Delete(id));

                if (_message.status_item)
                    return Ok(_message.details);
                else
                    return StatusCode(401, _message.Res());
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


        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Get(int id)
        {
            if (id > 0)
            {
                _message = await Task.Run(() => _student.GetbyID(id));

                if (_message.status_item)
                    return Ok(_message.details);
                else
                    return StatusCode(404, _message.Res());
            }
            else
            {
                _message.details = "*Id No valida";
                return BadRequest(_message.Res());
            }
        }


        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            _message = await Task.Run(() => _student.GetbyID(-10));

            if (_message.status_item)
                return Ok(_message.details);
            else
                return BadRequest(_message.Res());
        }
    }
}
