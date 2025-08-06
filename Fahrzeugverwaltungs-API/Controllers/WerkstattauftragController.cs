using Fahrzeugverwaltungs_API.Models;
using Fahrzeugverwaltungs_API.Repositories;
using Fahrzeugverwaltungs_API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fahrzeugverwaltungs_API.Controllers
{
    [ApiController]
    public class WerkstattauftragController : Controller
    {
        private readonly WerkstattauftragService _service;

        public WerkstattauftragController(WerkstattauftragService werkstattauftragService)
        {
            _service = werkstattauftragService;
        }
        [HttpGet("auftraege")]
        public ActionResult<List<Werkstattauftrag>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("fahrzeuge/{fahrzeugId}/auftraege")]
        public ActionResult<List<Werkstattauftrag>> GetByFahrzeug(int fahrzeugId)
        {
            return Ok(_service.GetByFahrzeug(fahrzeugId));
        }

        [HttpPost("fahrzeuge/{fahrzeugId}/auftraege")]
        public ActionResult<Werkstattauftrag> Create(int fahrzeugId, Werkstattauftrag auftrag)
        {
            Werkstattauftrag created = _service.Create(fahrzeugId, auftrag);

            if (created == null)
            {
                return NotFound("Fahrzeug wurde nicht gefunden!");
            }
            return CreatedAtAction(nameof(GetByFahrzeug), new { fahrzeugId = fahrzeugId }, created);
        }

        [HttpPut("/auftraege/{id}")]
        public IActionResult Update(int id, Werkstattauftrag updated)
        {

            if (!_service.Update(id, updated))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("/auftraege/{id}")]
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
