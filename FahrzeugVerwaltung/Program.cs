using Aufgabe;
using FahrzeugVerwaltung.Service;
using System;

namespace FahrzeugVerwaltung
{
    public class Program
    {
        static void Main(string[] args)
        {
            bool repeat = true;
            VehicleService vs = new VehicleService();
            

            Console.WriteLine("Hallo und herzlich willkommen!");
            while (repeat)
            {
                Console.WriteLine(@"Bitte Wählen Sie eine Aktion aus
1) Vehicle anlegen
2) Vehicle löschen
3) Alle Vehicle anzeigen
4) Vehicle bearbeiten

10) Programm beenden");
                string option = Console.ReadLine();
                switch (option)
                {
                    // Vehicle anlegen
                    case "1":
                        Console.Write("Marke: ");
                        string brand = Console.ReadLine();

                        Console.Write("Modell: ");
                        string model = Console.ReadLine();

                        Console.Write("Typ (PKW/LKW): ");
                        string type = Console.ReadLine();

                        Vehicle newVehicle;
                        if (type.ToUpper() == "PKW")
                        {
                            newVehicle = new PKW(brand, model);
                        }
                        else if (type.ToUpper() == "LKW")
                        {
                            Console.Write("Kapazität in Tonnen: ");
                            double capacity = double.Parse(Console.ReadLine());
                            try
                            {
                                newVehicle = new LKW(brand, model, capacity);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Ungültige Eingabe!");
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Unbekannter Typ!");
                            break;
                        }

                        vs.Save(newVehicle);
                        Console.WriteLine("Fahrzeug gespeichert!");
                        break;
                    // Vehicle löschen
                    case "2":
                        Console.Write("ID des Fahrzeugs zum Löschen: ");
                        int ident = Int32.Parse(Console.ReadLine());
                        try
                        {
                            var toDelete = vs.Get(ident);
                            if (toDelete != null)
                            {
                                vs.Delete(toDelete);
                                Console.WriteLine("Fahrzeug gelöscht.");
                            }
                            else
                            {
                                Console.WriteLine("Fahrzeug nicht gefunden.");
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Ungültige Eingabe!");
                        }

                        break;

                    // Alle Vehicle anzeigen
                    case "3":
                        var allVehicles = vs.GetAll();
                        foreach (var vehicle in allVehicles)
                        {
                            Console.WriteLine($"ID: {vehicle.Ident} | {vehicle.Type} | {vehicle.Brand} {vehicle.Model}");
                            if (vehicle is LKW lkw)
                            {
                                Console.WriteLine($"-> Kapazität: {lkw.Capacity} Tonnen");
                            }
                        }
                        break;
                    // Vehicle bearbeiten
                    case "4":
                        Console.Write("ID des Fahrzeugs zum Aktualisieren: ");
                        int id = Int32.Parse(Console.ReadLine());
                        try
                        {
                            var vehicle = vs.Get(id);
                            if (vehicle == null)
                            {
                                Console.WriteLine("Fahrzeug nicht gefunden.");
                                break;
                            }

                            Console.Write($"Neue Marke (aktuell: {vehicle.Brand}): ");
                            string brandInput = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(brandInput))
                                vehicle.Brand = brandInput;

                            Console.Write($"Neues Modell (aktuell: {vehicle.Model}): ");
                            string modelInput = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(modelInput))
                                vehicle.Model = modelInput;

                            if (vehicle is LKW lkwUpdate)
                            {
                                Console.Write($"Neue Kapazität (aktuell: {lkwUpdate.Capacity}): ");
                                string capacityInput = Console.ReadLine();
                                double newCapacity = double.Parse(Console.ReadLine());
                                try
                                {
                                    lkwUpdate.Capacity = newCapacity;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Ungültige Eingabe");
                                }
                            }

                            vs.Update(vehicle);
                            Console.WriteLine("Fahrzeug aktualisiert.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Ungültige Eingabe!");
                        }
                        break;

                    case "10":
                        Console.WriteLine("Programm wird beendet.");
                        repeat = false;
                        return;

                    default:
                        Console.WriteLine("Ungültige Eingabe.");
                        break;
                }
            }
        }
    
    }
}
