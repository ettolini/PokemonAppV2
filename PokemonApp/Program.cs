using System;

namespace PokemonApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PokeProcessor pokeProcessor = new PokeProcessor();
            bool continueProgram = true;
            string input;

            Console.WriteLine("Welcome to my humble Pokémon App!");

            while (continueProgram)
            {
                Console.WriteLine($@"What would you like to do?
1. Enter a Pokémon's name to get their info.
2. Enter '{pokeProcessor.quitCommand}' to quit the program.
");

                input = Console.ReadLine();

                if (input != pokeProcessor.quitCommand)
                {
                    Console.WriteLine(@"...
");
                    continueProgram = pokeProcessor.GetPokemon(input).Result;

                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                }
                else
                {
                    continueProgram = false;
                }
            }

            Console.WriteLine(@"
I hope you liked it!");
        }
    }
}
