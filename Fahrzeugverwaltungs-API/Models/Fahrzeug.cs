namespace Fahrzeugverwaltungs_API.Models
{
    public class Fahrzeug
    {
        public int ID { get; set; }
        public string Hersteller { get; set; }
        public string Modell {  get; set; }
        public int Baujahr {  get; set; }
        public string Kennzeichen { get; set; }
    }
}
