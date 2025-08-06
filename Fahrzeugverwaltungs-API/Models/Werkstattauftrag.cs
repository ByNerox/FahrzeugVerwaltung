using System;

namespace Fahrzeugverwaltungs_API.Models
{
    public class Werkstattauftrag
    {
        public int Id { get; set; }
        public int FahrzeugID { get; set; }
        public string Beschreibung { get; set; }
        public DateTime Datum { get; set; }
        public Auftragsstatus Status { get; set; }
    }

    public enum Auftragsstatus
    {
        Offen,
        InBearbeitung,
        Abgeschlossen
    }
}
