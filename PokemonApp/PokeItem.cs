using Newtonsoft.Json.Linq;

namespace PokemonApp
{
    class PokeItem
    {
        public PokeItem(int id, int height, int weight, JObject[] types)
        {
            Id = id;
            Height = height;
            Weight = weight;
            Types = types;
        }

        public int Id { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public JObject[] Types { get; set; }
        public JObject[] LocationEncounters { get; set; }
    }
}
