using Aufgabe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;


namespace FahrzeugVerwaltung.Service
{
    public class VehicleConsoleService
    {
        private List<Vehicle> vehicleList = new List<Vehicle>();
        private Dictionary<string, List<Vehicle>> cleanupResult;

        // Vehicle anlegen
        public void Save()
        {
            Console.WriteLine(@"Welchen Typ wollen Sie anlegen?
1)PKW
2)LKW");
            string option = Console.ReadLine();

            Console.WriteLine("Bitte geben Sie nun die Marke an:");
            string brand = Console.ReadLine();
            Console.WriteLine("Bitte geben Sie nun das Modell ein:");
            string model = Console.ReadLine();

            // Bei LKW
            if (option == "2")

            {
                try
                {
                    Console.WriteLine("Geben Sie die Kapazität an");
                    double capacity = double.Parse(Console.ReadLine());
                    LKW vehicle = new LKW(brand, model, capacity);
                    vehicleList.Add(vehicle);
                }
                catch (FormatException)
                {
                    Console.WriteLine("\u001b[1;31mBitte geben Sie eine gültige Zahl ein!\u001b[1;39m");
                    return;
                }
            }
            // Bei PKW
            else
            {
                PKW vehicle = new PKW(brand, model);
                vehicleList.Add(vehicle);
            }

            Console.WriteLine("\u001b[1;32mFahrzeug erfolgreich gespeichert!\u001b[1;39m");
            LoadingScreen();


        }

        // Vehicle löschen
        public void Delete()
        {
            try
            {
                Console.WriteLine("Bitte geben Sie die Id vom Fahrzeug ein, welches gelöscht werden soll");
                int id = Int32.Parse(Console.ReadLine());

                foreach (Vehicle vehicle in vehicleList.ToList())
                {
                    if (vehicle.Ident == id)
                    {
                        vehicleList.Remove(vehicle);
                    }
                }
                Console.WriteLine("\u001b[1;32mFahrzeug erfolgreich gelöscht!\u001b[1;39m");
            }
            catch (FormatException)
            {
                Console.WriteLine("\u001b[1;31mBitte geben Sie eine gültige Zahl ein!\u001b[1;39m");
            }
        }

        // alle Vehicle anzeigen
        public void GetAll()
        {
            foreach (Vehicle vehicle in vehicleList)
            {
                if (vehicle is LKW lkw)
                {
                    Console.WriteLine("Ident: " + lkw.Ident + ", Marke: " + lkw.Brand + ", Modell: " + lkw.Model + ", Typ: " + lkw.Type + ", capacity: " + lkw.Capacity);
                }
                else
                {
                    Console.WriteLine("Ident: " + vehicle.Ident + ", Marke: " + vehicle.Brand + ", Modell: " + vehicle.Model + ", Typ: " + vehicle.Type);
                }
            }
        }
        // Vehicle bearbeiten
        public void Update()
        {
            Console.WriteLine("Bitte geben Sie den Ident ein, von dem Auto, welches bearbeitet werden soll");
            try
            {
                int id = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Bitte geben Sie nun die Marke ein");
                string brand = Console.ReadLine();
                Console.WriteLine("Bitte geben Sie nun das Modell ein");
                string model = Console.ReadLine();


                foreach (Vehicle vehicle in vehicleList)
                {
                    if (vehicle.Ident == id)
                    {
                        vehicle.Brand = brand;
                        vehicle.Model = model;
                    }
                }
                Console.WriteLine("\u001b[1;32mFahrzeug erfolgreich bearbeitet!\u001b[1;39m");
            }
            catch (FormatException)
            {
                Console.WriteLine("\u001b[1;31mBitte geben Sie eine gültige Zahl ein!\u001b[1;39m");
            }
        }
        // Vehicle Liste in JSON umwandeln
        public void ConvertToJSON()
        {
            
            string jsonString = JsonSerializer.Serialize(vehicleList);
            Console.WriteLine("Unter welchem Namen soll die Liste gespeichert werden?");
            string fileName = Console.ReadLine();
            File.WriteAllText("C:/dev/" + fileName + ".json", jsonString);
            Console.WriteLine("\u001b[1;32mErfolgreich in eine JSON Datei geschrieben!\u001b[1;39m");
        }

        // JSON Datein laden und der Vehicle Liste hinzufügen
        public void LoadJSON()
        {
            string folderPath = "C:/dev";
            string[] jsonFiles = Directory.GetFiles(folderPath, "*.json");

            if (jsonFiles.Length == 0)
            {
                Console.WriteLine("Keine JSON-Dateien im Verzeichnis gefunden.");
                return;
            }

            Console.WriteLine("Verfügbare JSON-Dateien:");
            for (int i = 0; i < jsonFiles.Length; i++)
            {
                Console.WriteLine($"{i + 1}: {Path.GetFileName(jsonFiles[i])}");
            }

            Console.Write("Bitte die Nummer der Datei eingeben, die geladen werden soll: ");
            if (!int.TryParse(Console.ReadLine(), out int fileIndex) || fileIndex < 1 || fileIndex > jsonFiles.Length)
            {
                Console.WriteLine("\u001b[1;31mUngültige Eingabe.\u001b[1;39m");
                return;
            }

            string selectedFile = jsonFiles[fileIndex - 1];
            string jsonString = File.ReadAllText(selectedFile);

            if (!string.IsNullOrEmpty(jsonString))
            {
                List<Dictionary<string, JsonElement>> rawList = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonString);

                foreach (var item in rawList)
                {
                    string type = item["Type"].GetString();
                    string brand = item["Brand"].GetString();
                    string model = item["Model"].GetString();

                    if (type == "LKW")
                    {
                        double capacity = item.ContainsKey("Capacity") ? item["Capacity"].GetDouble() : 0;
                        LKW lkw = new LKW(brand, model, capacity);
                        lkw.Ident = item["Ident"].GetInt32();
                        vehicleList.Add(lkw);
                    }
                    else if (type == "PKW")
                    {
                        PKW pkw = new PKW(brand, model);
                        pkw.Ident = item["Ident"].GetInt32();
                        vehicleList.Add(pkw);
                    }
                }

                Console.WriteLine("\u001b[1;32mFahrzeuge erfolgreich geladen.\u001b[1;39m");
            }
            else
            {
                Console.WriteLine("Die ausgewählte Datei ist leer.");
            }
        }

        // Ladebildschirm + Duplikationenentferner
        public void LoadingScreen()
        {
            Console.WriteLine("Loading...");

            while (Console.KeyAvailable)
                Console.ReadKey(true);

            // Ladezeit
            TimeSpan duration = TimeSpan.FromSeconds(2);
            DateTime start = DateTime.Now;

            // Duplikate im Hintergrund entfernen
            var cleanupTask = Task.Run(() =>
            {
                var uniqueVehicles = new Dictionary<string, Vehicle>();
                var duplicates = new Dictionary<string, List<Vehicle>>();

                foreach (var vehicle in vehicleList)
                {
                    string key = $"{vehicle.Brand}|{vehicle.Model}|{vehicle.Type}";

                    if (!uniqueVehicles.ContainsKey(key))
                    {
                        uniqueVehicles[key] = vehicle;
                    }
                    else
                    {
                        var existing = uniqueVehicles[key];

                        if (vehicle.Ident < existing.Ident)
                        {
                            if (!duplicates.ContainsKey(key))
                                duplicates[key] = new List<Vehicle> { existing };
                            else
                                duplicates[key].Add(existing);

                            uniqueVehicles[key] = vehicle;
                        }
                        else
                        {
                            if (!duplicates.ContainsKey(key))
                                duplicates[key] = new List<Vehicle> { vehicle };
                            else
                                duplicates[key].Add(vehicle);
                        }
                    }
                }

                vehicleList = uniqueVehicles.Values.ToList();

                cleanupResult = duplicates;
            });

            while (DateTime.Now - start < duration)
            {
                if (Console.KeyAvailable)
                    Console.ReadKey(true);

                Thread.Sleep(50);
            }

            cleanupTask.Wait(); // Warten bis Task fertig

            // Ergebnis anzeigen
            if (cleanupResult != null && cleanupResult.Count > 0)
            {
                Console.WriteLine("\u001b[1;33mWarnung: Es wurden doppelte Fahrzeuge gefunden (neuere wurden entfernt):");
                foreach (var pair in cleanupResult)
                {
                    var parts = pair.Key.Split('|');
                    Console.WriteLine($"- {parts[0]} {parts[1]} ({parts[2]}): {pair.Value.Count} entfernt\u001b[1;39m");
                }
            }
            else
            {
                Console.WriteLine("Keine doppelten Fahrzeuge gefunden.");
            }
        }

    }
}
