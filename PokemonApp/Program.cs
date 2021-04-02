using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace PokemonApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please, enter a Pokemon's name: ");
            string input = Console.ReadLine();

            Console.WriteLine("...");
            GetPokemon(input);
            Console.ReadLine();
        }

        public static async void GetPokemon(string pokeName)
        {
            try
            {
                var content = (string)await GetData(pokeName);
                PokeItem pokeItem = JsonConvert.DeserializeObject<PokeItem>(content);

                content = (string)await GetData($"{pokeItem.Id}/encounters");
                JObject[] locationEncounters = JsonConvert.DeserializeObject<JObject[]>(content);
                string canEncounter;
                string[] locationNames = new string[locationEncounters.Length];

                if (locationEncounters.Length > 0)
                {
                    canEncounter = "This Pokémon could be encountered in the following areas...";
                    pokeItem.LocationEncounters = locationEncounters;

                    for (var i = 0; i < pokeItem.LocationEncounters.Length; i++)
                    {
                        locationNames[i] = (string)pokeItem.LocationEncounters[i]["location_area"]["name"];
                        locationNames[i] = locationNames[i].Replace("-", " ");
                    }
                }
                else
                {
                    canEncounter = "There's no known area where this Pokémon could be found...";
                }

                string totalLocations = "";

                foreach (var location in locationNames)
                {
                    totalLocations += $@"- {location}
";
                }

                string totalTypes = $"{pokeItem.Types[0]["type"]["name"]}";
                if (pokeItem.Types.Length > 1)
                    totalTypes += $" & {pokeItem.Types[1]["type"]["name"]}";

                Console.WriteLine($@"Id: {pokeItem.Id}
Height: {pokeItem.Height}
Weight: {pokeItem.Weight}
Types: {totalTypes}
Encounters: {canEncounter}
{totalLocations}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                System.Console.WriteLine("HELP: Maybe you misspelled the name...?");
            }
        }

        public static async Task<string> GetData(string url)
        {
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://pokeapi.co/api/v2/pokemon/") };

            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/json");

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}
