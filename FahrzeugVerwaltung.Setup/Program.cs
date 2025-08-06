using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aufgabe;
using Unity;

namespace FahrzeugVerwaltung.Setup
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Init.Initialize();

            IVehicleService vehicleService = Init.container.Resolve<IVehicleService>();

            IEnumerable<Vehicle> allVehicles = vehicleService.GetAll();

            foreach (Vehicle vehicle in allVehicles)
            {
                Console.WriteLine($"ID: {vehicle.Ident} | {vehicle.Type} | {vehicle.Brand} {vehicle.Model}");
                if (vehicle is LKW lkw)
                {
                    Console.WriteLine($"-> Kapazität: {lkw.Capacity} Tonnen");
                }
            }
        }
    }
}
