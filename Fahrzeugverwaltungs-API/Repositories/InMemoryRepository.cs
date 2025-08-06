using Fahrzeugverwaltungs_API.Models;
using System.Collections.Generic;

namespace Fahrzeugverwaltungs_API.Repositories
{
    public static class InMemoryRepository
    {
        public static List<Fahrzeug> Fahrzeuge { get; set; } = new();
        public static List<Werkstattauftrag> Auftraege { get; set; } = new();
        

        
    }
}
