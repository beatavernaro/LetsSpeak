using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterConsoleTables;
using Sharprompt;
using System.ComponentModel.DataAnnotations;

namespace Lets_Speak
{
    public class Dictionary
    {
        
        public static string term { get; set; }

        public static string definition { get; set; }

        public static Dictionary<string, string> dicionario = new Dictionary<string, string>();


        public static void RegisterTerm()
        {
            Console.WriteLine("Write the term to add: ");
            term = Console.ReadLine();

            Console.WriteLine("Write the definition: ");
            definition = Console.ReadLine();

            var save = Prompt.Confirm("Deseja Salvar?");
            if (!save)
                return;
            Console.WriteLine("Saving");
            Thread.Sleep(2000);

            dicionario.Add(term, definition);
            Database.Save(dicionario);

            Console.WriteLine("Saved");
        }
        
        

        public static void SearchTerm()
        {

            Console.WriteLine("Type in the term: ");

            var searchedTerm = Console.ReadLine();
            bool found = false;
            foreach (var word in dicionario)
            {
                if(word.Key.Contains(searchedTerm, StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine($"{word.Key} : {word.Value}");
                    found = true;
                }
                
            }
            if (!found)
                Console.WriteLine("No matches found");


        }


        
        public override string ToString()
        {
            return term;
        }

       

        
    }
}
