using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
namespace TunUI
{
    public class ChoseUI
    {
        public Dictionary<int,MenuUI> menu;
        ConsoleColor textColor;
        ConsoleColor backgroundColor;
        ConsoleColor selectedUIColor; 
        int selectedMenu;
        int selectedUI; 
        public bool isDirty = true; 
        bool _isRunning = false;

        public ChoseUI(List<MenuUI> uiElements, ConsoleColor selectedUIColor)
        {
            menu=new Dictionary<int,MenuUI>();
            for (int i = 0; i < uiElements.Count; i++) 
            {
            
                menu.Add(i, uiElements[i]);
            }
            this.textColor = Console.ForegroundColor;
            this.backgroundColor = Console.BackgroundColor;
            this.selectedUIColor = selectedUIColor;  
        }

        public ChoseUI( ConsoleColor selectedUIColor)
        {
            menu = new Dictionary<int, MenuUI>();
            this.textColor = Console.ForegroundColor;
            this.backgroundColor = Console.BackgroundColor;
            this.selectedUIColor = selectedUIColor;
        }

        public void AddUI(MenuUI ui)
        {
            menu.Add(menu.Count, ui);
        }
        public void DrawSelectedUI()
        {
           
            Console.Clear();
            MenuUI currentMenu = menu[selectedMenu];
            int maxLenght = currentMenu.menuList.Select(x => x.text.Length).Max();
             
            Console.WriteLine($"{new string(' ',5)}{currentMenu.title.ToUpper()}{new string(' ', 5+maxLenght - currentMenu.title.Length)}");
            // Draw menu items
            for (int i = 0; i < currentMenu.menuList.Count; i++)
            {
                //Console.SetCursorPosition(x + 1, y + 1 + i);
                var menuItem = currentMenu.menuList[i];
                if (i == selectedUI)
                {
                    Console.BackgroundColor = selectedUIColor;
                    Console.ForegroundColor = backgroundColor;
                }
                else
                {
                    Console.BackgroundColor = backgroundColor;
                    Console.ForegroundColor = textColor;
                }
                
                Console.WriteLine($"{new string(' ',5)}{menuItem.text.ToUpper()}{new string(' ', 5+maxLenght - menuItem.text.Length)}");
            }
            Console.ResetColor();

            Console.CursorVisible = false;
            
        }
        public void StartUI()
        {
            _isRunning = true;
            
            while (_isRunning)
            {
                if (isDirty)
                {
                    DrawSelectedUI();
                    isDirty = false;
                } 
                HandleSelectChange(); 
            }
        }
        public void StopUI()
        {
            _isRunning = false; 
        }
        public void ChangeMenu(int newMenu)
        {
            selectedMenu = newMenu;
            selectedUI = 0;
            isDirty = true;
            
        }
        //Only do +1 or -1
        void ChangeSelectedUI(int newSelectedUI)
        {
            selectedUI += newSelectedUI;
            if (selectedUI < 0)
            {
                selectedUI = menu[selectedMenu].menuList.Count - 1;
            }
            if(selectedUI> menu[selectedMenu].menuList.Count - 1)
            {
                selectedUI = 0;
            }
            isDirty = true;
        }
        void HandleSelectChange()
        {
            var keyPressed= Console.ReadKey();
            if (keyPressed.Key==ConsoleKey.W|| keyPressed.Key==ConsoleKey.UpArrow)
            {
                ChangeSelectedUI(-1);
            }
            else if(keyPressed.Key == ConsoleKey.S || keyPressed.Key == ConsoleKey.DownArrow)
            {
                ChangeSelectedUI(+1);
            }
            if (keyPressed.Key == ConsoleKey.Enter)
            {
                menu[selectedMenu].menuList[selectedUI].actionToDo();
            }
        }
    }
}
