using Microsoft.AspNetCore.Mvc;
using NG.Interfaces;
using NG.Models;
using NG.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Zeta.Controllers
{
    [Route("api/[controller]")]
    public class SchoolAttendanceController : Controller
    {
        private ISchoolAttendance _schoolattendance;
        private Message _message;
        public SchoolAttendanceController(ISchoolAttendance iSchoolAttendance, Message mess)
        {
            _schoolattendance = iSchoolAttendance;
            _message = mess;
        }


        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody]SchoolAttendaceVM objSchoolAttendance)
        {
            if (ModelState.IsValid)
            {
                _message = await Task.Run(() => _schoolattendance.Create(objSchoolAttendance));

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
        public async Task<IActionResult> Put([FromBody]SchoolAttendaceVM objSchoolAttendance)
        {
            if (ModelState.IsValid)
            {
                _message = await Task.Run(() => _schoolattendance.Update(objSchoolAttendance));

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

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _message = await Task.Run(() => _schoolattendance.Delete(id));

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
                _message = await Task.Run(() => _schoolattendance.GetbyID(id));

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
            _message = await Task.Run(() => _schoolattendance.GetbyID(-10));

            if (_message.status_item)
                return Ok(_message.details);
            else
                return BadRequest(_message.Res());
        }
    }
}