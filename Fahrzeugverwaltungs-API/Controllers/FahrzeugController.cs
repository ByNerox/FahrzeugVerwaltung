using Fahrzeugverwaltungs_API.Models;
using Fahrzeugverwaltungs_API.Repositories;
using Fahrzeugverwaltungs_API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fahrzeugverwaltungs_API.Controllers
{
    [ApiController]
    [Route("fahrzeuge")]
    public class FahrzeugController : ControllerBase
    {
        private readonly FahrzeugService _service;
        public FahrzeugController(FahrzeugService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult<List<Fahrzeug>> Getall()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Fahrzeug> GetById(int id)
        {
            Fahrzeug fahrzeug = _service.GetById(id);
            if (fahrzeug == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(fahrzeug);
            }
        }

        [HttpPost]
        public ActionResult<Fahrzeug> newVehicle(Fahrzeug f)
        {
            Fahrzeug createdF = _service.Create(f);
            return CreatedAtAction(nameof(GetById), new { id = f.ID }, createdF);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Fahrzeug updatedF)
        {
            if (!_service.Update(id, updatedF)) {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_service.Delete(id))
            {
                return NotFound();
            }
            return NoContent();
        }


    }


}
