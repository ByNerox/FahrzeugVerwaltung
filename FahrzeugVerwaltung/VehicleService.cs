using Aufgabe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FahrzeugVerwaltung
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _repository;

        public VehicleService()
        {
            _repository = new VehicleRepository();
        }
        public void Delete(Vehicle entity)
        {
            _repository.Delete(entity);
        }

        public Vehicle Get(int ident)
        {
            return _repository.Get(ident);
        }

        public IEnumerable<Vehicle> GetAll()
        {
            return _repository.GetAll();
        }

        public void Save(Vehicle entity)
        {
            _repository.Save(entity);
        }

        public void Update(Vehicle entity)
        {
            _repository.Update(entity);
        }
    }
}
