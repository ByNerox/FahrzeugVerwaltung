using System;
using FahrzeugVerwaltung.Service;

namespace FahrzeugVerwaltung
{
    public class Program
    {
        static void Main(string[] args)
        {
            bool nochmal = true;
            VehicleConsoleService vcs = new VehicleConsoleService();
            vcs.LoadJSON();

            Console.WriteLine("Hallo und herzlich willkommen!");
            while (nochmal)
            {
                Console.WriteLine(@"Bitte Wählen Sie eine Aktion aus
1) Vehicle anlegen
2) Vehicle löschen
3) Alle Vehicle anzeigen
4) Vehicle bearbeiten
5) Liste Speichern
10) Programm beenden");
                string option = Console.ReadLine();
                if (option == "1")
                {

                    vcs.Save();

                }
                else if (option == "2")
                {
                    vcs.Delete();

                }
                else if (option == "3")
                {
                    vcs.GetAll();

                }
                else if (option == "4")
                {
                    vcs.Update();

                }
                else if (option == "5")
                {
                    vcs.ConvertToJSON();
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
