using Sharprompt;

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

            bool exists = false;
            foreach (var word in dicionario)
            {
                if (word.Key.ToLower() == term.ToLower())
                {
                    exists = true;
                }
            }
            if (!exists)
            {
                Console.WriteLine("Saving");
                Thread.Sleep(2000);
                dicionario.Add(term, definition);
                Database.Save(dicionario);
                Console.WriteLine("Saved");
            }
            else
                Console.WriteLine("This term already exists in the dictionary");

        }

        public static void SearchTerm()
        {

            Console.WriteLine("Type in the term: ");

            var searchedTerm = Console.ReadLine();
            bool found = false;
            foreach (var word in from word in dicionario
                                 where word.Key.Contains(searchedTerm, StringComparison.InvariantCultureIgnoreCase)
                                 select word)
            {
                Console.WriteLine($"{word.Key}: {word.Value}");
                found = true;
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
