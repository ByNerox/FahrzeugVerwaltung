using System;

namespace Aufgabe1
{
    public class Vehicle
    {
        public static int currentId = 0;
        public int Ident { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        public Vehicle(string Brand, string Model)
        {
            currentId++;
            this.Ident = currentId;
            this.Brand = Brand;
            this.Model = Model;
        }
        public void DeleteVehicle(int id)
        {
            Console.WriteLine("Vehicle Deleted");
        }
        public void ListVehicles()
        {
            Console.WriteLine("Alle Fahrzeuge angezeigt!");
        }

    }

    public class Program
    {
        static void Main(string[] args)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            bool nochmal = true;

            Console.WriteLine("Hallo und herzlich willkommen!");
            while (nochmal)
            {
                Console.WriteLine(@"Bitte Wählen Sie eine Aktion aus
1) Vehicle anlegen
2) Vehicle löschen
3) Alle Vehicle anzeigen
4) Vehicle bearbeiten
10) Programm beenden");
                string option = Console.ReadLine();
                // Vehicle anlegen
                if (option == "1")
                {
                    
                    Console.WriteLine("Bitte geben Sie nun die Marke an:");
                    string brand = Console.ReadLine();
                    Console.WriteLine("Bitte geben Sie nun das Modell ein:");
                    string model = Console.ReadLine();
                    Vehicle vehicle = new Vehicle(brand, model);
                    vehicles.Add(vehicle);
                    Console.WriteLine(vehicles);

                }
                // Vehicle löschen
                else if (option == "2")
                {
                    Console.WriteLine("Bitte geben Sie die Id vom Fahrzeug ein, welches gelöscht werden soll");
                    int id = Int32.Parse(Console.ReadLine());

                    foreach (Vehicle vehicle in vehicles.ToList())
                    { 
                        if (vehicle.Ident == id)
                        {
                            vehicles.Remove(vehicle);
                        }
                    }

                }
                // alle Vehicle anzeigen 
                else if (option == "3")
                {
                    foreach (Vehicle vehicle in vehicles)
                    {
                        Console.WriteLine("Ident: " + vehicle.Ident + ", Marke: " + vehicle.Brand + ", Modell: " + vehicle.Model);
                    }
                }
                // Vehicle bearbeiten
                else if (option == "4")
                {
                    Console.WriteLine("Bitte geben Sie den Ident ein, von dem Auto, welches bearbeitet werden soll");
                    try
                    {
                        int id = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Bitte geben Sie nun die Marke ein");
                        string brand = Console.ReadLine();
                        Console.WriteLine("Bitte geben Sie nun das Modell ein");
                        string model = Console.ReadLine();

                        foreach (Vehicle vehicle in vehicles)
                        {
                            if (vehicle.Ident == id)
                            {
                                vehicle.Brand = brand;
                                vehicle.Model = model;
                            }
                        }
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Bitte geben Sie eine gültige Zahl ein!");
                    }
                    
                }
                // Programm beenden
                else if (option == "10")
                {
                    nochmal = false;
                }
            }
        }
    }

}