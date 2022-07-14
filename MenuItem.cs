using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lets_Speak
{
    public enum MenuType {Submenu,Command}
    public class CreateMenu
    {
        private const string UNSELECTED = "|     ";
        private const string SELECTED = "|   » ";
        private readonly int LINE_WIDTH = Console.WindowWidth / 3;//50;

        private string title;
        private Action action;
        public CreateMenu parent = null;
        private List<CreateMenu> menuList;
        public MenuType Type { get; }
        private static CreateMenu _root;
        private int selectedIndex = 0;

        public CreateMenu(string title)
        {
            this.title = title;
            menuList = new List<CreateMenu>();
            if (_root == null) _root = this;
            Type = MenuType.Submenu;
        }

        public CreateMenu(string title, Action action) : this(title)
        {
            this.action = action;
            Type = MenuType.Command;
        }

        public void Add(CreateMenu menuItem)
        {
            menuItem.parent = this;
            menuList.Add(menuItem);
        }

        public void Execute()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ResetColor();

            switch (Type)
            {
                case MenuType.Submenu:
                    if (menuList.Count == 0)
                        return;
                    RenderSubmenu();
                    break;

                case MenuType.Command:
                    action();
                    Console.ReadKey(true);
                    break;

                default:
                    break;
            }

            return;

            void RenderSubmenu()
            {
                var key = new ConsoleKeyInfo();
                do
                {
                    Console.ResetColor();
                    Console.Clear();

                    var menuTitle = $"{UNSELECTED}{title.ToUpperInvariant().PadRight(LINE_WIDTH)}|";
                    var lineSeparator = $"|{new String('-', menuTitle.Length - 2)}|";

                    Console.WriteLine(lineSeparator);
                    Console.WriteLine(menuTitle);
                    Console.WriteLine(lineSeparator);

                    for (int i = 0; i < menuList.Count; i++)
                    {
                        var isSelected = i == selectedIndex;
                        var margin = isSelected ? SELECTED : UNSELECTED;

                        if (isSelected)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        Console.WriteLine($"{margin}{menuList[i].ToString().PadRight(LINE_WIDTH)}|");
                        Console.ResetColor();
                    }
                    Console.WriteLine(lineSeparator);

                    key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.PageUp:
                        case ConsoleKey.UpArrow:
                            selectedIndex = Math.Max(selectedIndex - 1, 0);
                            break;
                        case ConsoleKey.PageDown:
                        case ConsoleKey.DownArrow:
                            selectedIndex = Math.Min(selectedIndex + 1, Math.Max(menuList.Count - 1, 0));
                            break;
                        case ConsoleKey.Enter:
                        case ConsoleKey.RightArrow:
                            menuList[selectedIndex].Execute();
                            break;
                        case ConsoleKey.Escape:
                            if (this != _root)
                                return;
                            break;
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.Backspace:
                            if (this != _root)
                                return;
                            break;
                        default:
                            break;
                    }
                } while (true);
            }
        }


        public override string ToString()
        {
            return title;
        }
    }

    
}


