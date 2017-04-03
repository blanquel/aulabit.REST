using Microsoft.AspNetCore.Mvc;
using NG.Interfaces;
using NG.Models;
using NG.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Zeta.Controllers
{
    [Produces("application/json")]
    [Route("api/School_Subject")]
    public class School_SubjectController : Controller
    {
        private ISchoolSubject _schoolsubject;
        private Message _message;
        public School_SubjectController(ISchoolSubject schoolsubject, Message mess)
        {
            _schoolsubject = schoolsubject;
            _message = mess;
        }


        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody]SchoolSubjectVM objSchoolSubject)
        {
            if (ModelState.IsValid)
            {
                _message = await Task.Run(() => _schoolsubject.Create(objSchoolSubject));

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
        public async Task<IActionResult> Put([FromBody]SchoolSubjectVM objSchoolSubject)
        {
            if (ModelState.IsValid)
            {
                _message = await Task.Run(() => _schoolsubject.Update(objSchoolSubject));

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
                // Todo is ok
                _message = await Task.Run(() => _schoolsubject.Delete(id));

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
                _message = await Task.Run(() => _schoolsubject.GetbyID(id));

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
            _message = await Task.Run(() => _schoolsubject.GetbyID(-10));

            if (_message.status_item)
                return Ok(_message.details);
            else
                return BadRequest(_message.Res());
        }
    }
}