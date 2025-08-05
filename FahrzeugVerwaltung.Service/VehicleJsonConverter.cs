using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Aufgabe;

namespace FahrzeugVerwaltung.Service
{
    public class VehicleJsonConverter : JsonConverter<Vehicle>
    {
        public override Vehicle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            var jsonObject = jsonDoc.RootElement;

            var type = jsonObject.GetProperty("Type").GetString();

            var brand = jsonObject.GetProperty("Brand").GetString();
            var model = jsonObject.GetProperty("Model").GetString();
            var ident = jsonObject.GetProperty("Ident").GetInt32();

            Vehicle vehicle;

            switch (type)
            {
                case "PKW":
                    vehicle = new PKW(brand, model);
                    break;
                case "LKW":
                    var capacity = jsonObject.GetProperty("Capacity").GetDouble();
                    vehicle = new LKW(brand, model, capacity);
                    break;
                default:
                    throw new NotSupportedException("Unbekanntse Fahrzeug");
            }
            vehicle.Ident = ident;

            // currentId ggf. aktualisieren
            if (Vehicle.currentId < ident)
            {
                Vehicle.currentId = ident;
            }

            return vehicle;
        }

        public override void Write(Utf8JsonWriter writer, Vehicle value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
        }
    }
}
