using Fahrzeugverwaltungs_API.Models;
using Fahrzeugverwaltungs_API.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Fahrzeugverwaltungs_API.Services
{
    public class FahrzeugService
    {
       public List<Fahrzeug> GetAll()
        {
            return InMemoryRepository.Fahrzeuge;
        }

        public Fahrzeug GetById(int id)
        {
            return InMemoryRepository.Fahrzeuge.FirstOrDefault(f => f.ID == id);
        }

        public Fahrzeug Create(Fahrzeug f)
        {
            if (InMemoryRepository.Fahrzeuge.Count > 0)
            {
                f.ID = InMemoryRepository.Fahrzeuge.Max(f => f.ID) + 1;
            } else
            {
                f.ID = 1;
            }

            InMemoryRepository.Fahrzeuge.Add(f);
            return f;
        }

        public bool Update(int id, Fahrzeug updatedF)
        {
            Fahrzeug f = GetById(id);
            if (f == null)
            {
                return false;
            }
            f.Hersteller = updatedF.Hersteller;
            f.Modell =  updatedF.Modell;
            f.Baujahr = updatedF.Baujahr;
            f.Kennzeichen = updatedF.Kennzeichen;
            return true;
        }

        public bool Delete(int id)
        {
            Fahrzeug f = GetById(id);
            if (f == null)
            {
                return false;
            }

            return InMemoryRepository.Fahrzeuge.Remove(f);
        }
    }
}
