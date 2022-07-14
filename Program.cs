using Sharprompt;

namespace Lets_Speak
{
    public class Program
    {
        static void Main()
        {
            Database.Load();
            var dictionary = new Dictionary();
            Dictionary.dicionario = Database.Load();

            ConfiguraPrompt();
            Console.Title = "Let's Speak";

            var menu = new CreateMenu("Let's Speak - English School");
            //var teste = new Dictionary();

            var ListTerm = new CreateMenu("List terms", Dictionary.SearchTerm);
            var RegisterTerm = new CreateMenu("Save new terms", Dictionary.RegisterTerm);
            
            
            menu.Add(ListTerm);
            menu.Add(RegisterTerm);
            menu.Add(new CreateMenu("Exit", () => Environment.Exit(0)));

            menu.Execute();
        }

        private static void ConfiguraPrompt()
        {
            Prompt.ColorSchema.Answer = ConsoleColor.White;
            Prompt.ColorSchema.Select = ConsoleColor.White;

            Prompt.Symbols.Prompt = new Symbol("", "");
            Prompt.Symbols.Done = new Symbol("", "");
            Prompt.Symbols.Error = new Symbol("", "");
        }

       
    }
}