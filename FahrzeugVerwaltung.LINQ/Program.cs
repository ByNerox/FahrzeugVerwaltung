using System;
using System.Linq;
using System.Collections.Generic;
using Aufgabe;

namespace FahrzeugVerwaltung.LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Vehicle> vehicles = new List<Vehicle>
{
    new PKW("BMW", "3er"),
    new LKW("Mercedes", "Actros", 12.5),
    new PKW("Audi", "A4"),
    new PKW("VW", "Golf"),
    new LKW("Scania", "R-Series", 16),
    new LKW("DAF", "XF", 14),
    new LKW("Iveco", "Stralis", 13.5),
    new LKW("Renault", "T", 12),
    new LKW("Freightliner", "Cascadia", 19),
    new LKW("Peterbilt", "579", 20),
    new LKW("Kenworth", "T680", 18.5),
    new LKW("Hino", "700", 14.5),
    new LKW("Mack", "Anthem", 17),
    new LKW("Isuzu", "Giga", 16.5),
    new PKW("BMW", "3er"),
    new PKW("BMW", "3er"),
    new LKW("Mercedes", "Actros", 12.5),
    new LKW("Mercedes", "Actros", 12.5),
    new PKW("Audi", "A4"),
    new PKW("Audi", "A4"),
    new PKW("VW", "Golf"),
    new PKW("VW", "Golf"),
    new LKW("Volvo", "FH", 15),
    new LKW("Volvo", "FH", 15),
    new PKW("Ford", "Focus"),
    new PKW("Ford", "Focus"),
    new PKW("Opel", "Astra"),
    new PKW("Opel", "Astra"),
    new LKW("MAN", "TGX", 18),
    new LKW("MAN", "TGX", 18),
    new PKW("Toyota", "Corolla"),
    new PKW("Toyota", "Corolla"),
    new PKW("Honda", "Civic"),
    new PKW("Honda", "Civic"),
    new PKW("Chevrolet", "Cruze"),
    new PKW("Tesla", "Model 3"),
    new LKW("Freightliner", "Cascadia", 19),
    new PKW("Jaguar", "XE"),
    new PKW("Alfa Romeo", "Giulia"),
    new LKW("Peterbilt", "579", 20),
    new PKW("Lexus", "IS"),
    new PKW("Skoda", "Octavia"),
    new PKW("Mazda", "3"),
    new PKW("Hyundai", "i30"),
    new PKW("Kia", "Ceed"),
    new PKW("Seat", "Leon"),
    new PKW("Subaru", "Impreza"),
    new PKW("Mitsubishi", "Lancer"),
    new PKW("Nissan", "Altima"),
    new PKW("Chevrolet", "Cruze"),
    new PKW("Tesla", "Model 3"),
    new PKW("Tesla", "Model S"),
    new PKW("Tesla", "Model S"),
    new PKW("Audi", "Q5"),
    new PKW("Audi", "Q5"),
    new PKW("BMW", "X3"),
    new PKW("BMW", "X3"),
    new LKW("Volvo", "FMX", 18),
    new LKW("Volvo", "FMX", 18),
};

            List<Vehicle> vehicles2 = new List<Vehicle>
{
    new PKW("BMW", "3er"),
    new LKW("Mercedes", "Actros", 12.5),
    new PKW("VW", "Golf"),
    new LKW("Scania", "R-Series", 16),
    new LKW("DAF", "XF", 14),
    new LKW("Iveco", "Stralis", 13.5),
    new LKW("Renault", "T", 12),
    new LKW("Freightliner", "Cascadia", 19) };

            bool repeat = true;
            while (repeat)
            {

                Console.WriteLine("\n" + @"Welche Aufgabe soll angezeigt werden?
1) Aufgabe 1 anzeigen
2) Aufgabe 2 anzeigen
3) Aufgabe 3 anzeigen
4) Aufgabe 4 anzeigen
5) Aufgabe 5 anzeigen
10) Programm Beenden");
                try
                {
                    int option = Int32.Parse(Console.ReadLine());

                    switch (option)
                    {
                        // Aufgabe 1
                        case 1:

                            Console.Clear();
                            Console.WriteLine("Aufgabe 1:\n");
                            IEnumerable<Vehicle> audiQuery =
                                from vehicle in vehicles
                                where vehicle.Brand == "Audi"
                                select vehicle;

                            foreach (Vehicle vehicle in audiQuery)
                            {
                                if (vehicle is LKW lkw)
                                {
                                    Console.WriteLine($" ID: {lkw.Ident} Marke: {lkw.Brand} Modell: {lkw.Model} Kapazität: {lkw.Capacity}");
                                }
                                else
                                {
                                    Console.WriteLine($" ID: {vehicle.Ident} Marke: {vehicle.Brand} Modell: {vehicle.Model}");
                                }


                            }

                            break;

                        // Aufgabe 2
                        case 2:
                            Console.Clear();
                            Console.WriteLine("\nAufgabe 2:\n");
                            IEnumerable<Vehicle> vehiclesById = vehicles.OrderBy(v => v.Ident).ToList();

                            foreach (Vehicle vehicle in vehiclesById)
                            {
                                if (vehicle is LKW lkw)
                                {
                                    Console.WriteLine($" ID: {lkw.Ident} Marke: {lkw.Brand} Modell: {lkw.Model} Kapazität: {lkw.Capacity}");
                                }
                                else
                                {
                                    Console.WriteLine($"ID: {vehicle.Ident}, Marke: {vehicle.Brand}, Modell: {vehicle.Model}");
                                }
                            }

                            break;

                        // Aufgabe 3
                        case 3:
                            Console.Clear();
                            Console.WriteLine("\nAufgabe 3:\n");
                            IEnumerable<Vehicle> vehiclesByIdAndModel = vehicles
                                .OrderBy(v => v.Ident)
                                .ThenBy(v => v.Brand)
                                .ToList();

                            foreach (Vehicle vehicle in vehiclesByIdAndModel)
                            {
                                if (vehicle is LKW lkw)
                                {
                                    Console.WriteLine($" ID: {lkw.Ident} Marke: {lkw.Brand} Modell: {lkw.Model} Kapazität: {lkw.Capacity}");
                                }
                                else
                                {
                                    Console.WriteLine($"ID: {vehicle.Ident}, Marke: {vehicle.Brand}, Modell: {vehicle.Model}");
                                }
                            }
                            break;

                        case 4:
                            Console.Clear();
                            Console.WriteLine("Aufgabe 4");
                            Vehicle firstAudi = vehicles.FirstOrDefault(v => v.Brand == "Audi");
                            Vehicle firstAudi2 = vehicles2.FirstOrDefault(v => v.Brand == "Audi");

                            Console.WriteLine("Für erste Liste");
                            if (firstAudi != null)
                            {
                                Console.WriteLine($"Gefundener Audi: ID: {firstAudi.Ident}, Modell: {firstAudi.Model}, Typ: {firstAudi.Type}");
                            }
                            else
                            {
                                Console.WriteLine("Kein Audi gefunden.");
                            }

                            Console.WriteLine("Für zweite Liste");
                            if (firstAudi2 != null)
                            {

                                Console.WriteLine($"Gefundener Audi: ID: {firstAudi.Ident}, Modell: {firstAudi.Model}, Typ: {firstAudi.Type}");
                            }
                            else
                            {
                                Console.WriteLine("Kein Audi gefunden.");
                            }

                            break;

                        case 5:
                            try
                            {
                                Vehicle audiError = vehicles2
                                    .First(v => v.Brand == "Audi");
                            } 
                            catch (System.InvalidOperationException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        // Programm beenden
                        case 10:
                            repeat = false;
                            break;

                        default:
                            Console.WriteLine("Ungültige Eingabe");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("ungültige Eingabe");
                }


            }
        }
    }
    
}


