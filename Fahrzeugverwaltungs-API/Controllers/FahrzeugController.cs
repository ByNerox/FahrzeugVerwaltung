using Fahrzeugverwaltungs_API.Models;
using Fahrzeugverwaltungs_API.Repositories;
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
        [HttpGet]
        public ActionResult<List<Fahrzeug>> Getall()
        {
            return Ok(InMemoryRepository.Fahrzeuge);
        }

        [HttpGet("{id}")]
        public ActionResult<Fahrzeug> GetById(int id)
        {
            Fahrzeug fahrzeug = InMemoryRepository.Fahrzeuge.FirstOrDefault(f => f.ID == id);
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
            if (InMemoryRepository.Fahrzeuge.Any())
            {
                f.ID = InMemoryRepository.Fahrzeuge.Max(f => f.ID) + 1;
            }
            else
            {
                f.ID = 1;
            }

            InMemoryRepository.Fahrzeuge.Add(f);
            return CreatedAtAction(nameof(GetById), new { id = f.ID }, f);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Fahrzeug updatedF)
        {
            Fahrzeug f = InMemoryRepository.Fahrzeuge.FirstOrDefault(f => f.ID == id);
            if (f == null)
            {
                return NotFound();
            }
            
            f.Hersteller = updatedF.Hersteller;
            f.Modell = updatedF.Modell;
            f.Baujahr = updatedF.Baujahr;
            f.Kennzeichen = updatedF.Kennzeichen;
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Fahrzeug f = InMemoryRepository.Fahrzeuge.FirstOrDefault(f => f.ID == id);

            if (f == null) { 
                return NotFound(); 
            }
            else
            {
                InMemoryRepository.Fahrzeuge.Remove(f);
                return NoContent();
            }
        }

         
    }


}
