using Aufgabe;
using FahrzeugVerwaltung.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FahrzeugVerwaltung
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly string _filePath = "C:/dev/test.json";
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new VehicleJsonConverter() }
        };

        private List<Vehicle> LoadFromFile()
        {
            if (!File.Exists(_filePath) || new FileInfo(_filePath).Length == 0)
            {
                // Leere Datei oder nicht vorhanden → leere Liste
                return new List<Vehicle>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Vehicle>>(json, _options) ?? new List<Vehicle>();
        }

        private void SaveToFile(List<Vehicle> vehicles)
        {
            var jsonstring = JsonSerializer.Serialize(vehicles, _options);
        File.WriteAllText(_filePath, jsonstring);
        }

        public void Delete(Vehicle entity)
        {
            var vehicles = LoadFromFile();
            vehicles = vehicles.Where(v => v.Ident != entity.Ident).ToList();
            SaveToFile(vehicles);
        }

        public Vehicle Get(int ident)
        {
            return LoadFromFile().FirstOrDefault(v => v.Ident == ident);
        }

        public IEnumerable<Vehicle> GetAll()
        {
            return LoadFromFile();
        }

        public void Save(Vehicle entity)
        {
            var vehicles = LoadFromFile();
            vehicles.Add(entity);
            SaveToFile(vehicles);
        }

        public void Update(Vehicle entity)
        {
            var vehicles = LoadFromFile();
            var index = vehicles.FindIndex(v => v.Ident == entity.Ident);

            if (index != -1)
            {
                vehicles[index] = entity;
                SaveToFile(vehicles);
            }

        }
    }
}
