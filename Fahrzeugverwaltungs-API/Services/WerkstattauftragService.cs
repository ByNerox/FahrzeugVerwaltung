using Fahrzeugverwaltungs_API.Models;
using Fahrzeugverwaltungs_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fahrzeugverwaltungs_API.Services
{
    public class WerkstattauftragService
    {
        public List<Werkstattauftrag> GetAll()
        {
            return InMemoryRepository.Auftraege;
        }

        public List<Werkstattauftrag> GetByFahrzeug(int fahrzeugId)
        {
            return InMemoryRepository.Auftraege.Where(a => a.FahrzeugID == fahrzeugId).ToList();
        }

        public Werkstattauftrag GetById(int id)
        {
            return InMemoryRepository.Auftraege.FirstOrDefault(a => a.Id == id);
        }

        public Werkstattauftrag Create(int fahrzeugID, Werkstattauftrag auftrag)
        {
            if (InMemoryRepository.Auftraege.Count > 0)
            {
                auftrag.Id = InMemoryRepository.Auftraege.Max(a => a.Id) + 1;
            }
            else
            {
                auftrag.Id = 1;
            }
            auftrag.FahrzeugID = fahrzeugID;
            auftrag.Datum = DateTime.Now;
            auftrag.Status = Auftragsstatus.Offen;

            InMemoryRepository.Auftraege.Add(auftrag);
            return auftrag;
        }

            public bool Update(int id, Werkstattauftrag updated)
        {
            var auftrag = GetById(id);
            if (auftrag == null) return false;

            auftrag.Beschreibung = updated.Beschreibung;
            auftrag.Status = updated.Status;
            auftrag.Datum = updated.Datum;
            return true;
        }

        public bool Delete(int id)
        {
            var auftrag = GetById(id);
            if (auftrag == null) return false;

            return InMemoryRepository.Auftraege.Remove(auftrag);
        }
    }
    
}
