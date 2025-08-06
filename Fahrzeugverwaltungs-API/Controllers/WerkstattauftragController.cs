using Fahrzeugverwaltungs_API.Models;
using Fahrzeugverwaltungs_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fahrzeugverwaltungs_API.Controllers
{
    [ApiController]
    public class WerkstattauftragController : Controller
    {
        [HttpGet("/auftraege")]
        public ActionResult<List<Werkstattauftrag>> GetAll()
        {
            return Ok(InMemoryRepository.Auftraege);
        }

        [HttpGet("fahrzeuge/{fahrzeugId}/auftraege")]
        public ActionResult<List<Werkstattauftrag>> GetByFahrzeug(int fahrzeugId)
        {
            List<Werkstattauftrag> auftraege = InMemoryRepository.Auftraege.Where(a => a.FahrzeugID == fahrzeugId).ToList();

            return Ok(auftraege);
        }

        [HttpPost("fahrzeuge/{fahrzeugId}/auftraege")]
        public ActionResult <Werkstattauftrag> newAuftrag(int fahrzeugId, Werkstattauftrag auftrag)
        {
            if (!InMemoryRepository.Fahrzeuge.Any(f => f.ID == fahrzeugId)) {
                return NotFound("Fahrzeug wurde nicht gefunden!");
            }

            if (InMemoryRepository.Auftraege.Count > 0)
            {
                auftrag.Id = InMemoryRepository.Auftraege.Max(a => a.Id) + 1;
            }
            else
            {
                auftrag.Id = 1;
            }

            auftrag.FahrzeugID = fahrzeugId;
            auftrag.Datum = DateTime.Now;
            auftrag.Status = Auftragsstatus.Offen;

            InMemoryRepository.Auftraege.Add(auftrag);
            return CreatedAtAction(nameof(GetByFahrzeug), new { fahrzeugId = fahrzeugId }, auftrag);
            
        }

        [HttpPut("/auftraege/{id}")]
        public IActionResult Update(int id, Werkstattauftrag updatedAuftrag)
        {
            Werkstattauftrag auftrag = InMemoryRepository.Auftraege.FirstOrDefault(a => a.Id == id);
            if (auftrag == null)
            { 
                return NotFound("Auftrag wurde nicht gefunden"); 
            }
            auftrag.FahrzeugID = updatedAuftrag.FahrzeugID;
            auftrag.Beschreibung = updatedAuftrag.Beschreibung;
            auftrag.Datum = updatedAuftrag.Datum;
            auftrag.Status = updatedAuftrag.Status;

            return NoContent();
        }

        [HttpDelete("/auftraege/{id}")]
        public IActionResult Delete(int id)
        {
            Werkstattauftrag auftrag = InMemoryRepository.Auftraege.FirstOrDefault(a => a.Id == id);
            if (auftrag == null)
            {
                return NotFound("Auftrag wurde nicht gefunden");
            }

            InMemoryRepository.Auftraege.Remove(auftrag);
            return NoContent();
        }
            
    }
}
