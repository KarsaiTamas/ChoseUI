using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
namespace TunUI
{
    public class ChoseUI
    {
        public Dictionary<int,MenuUI> menu;
        ConsoleColor textColor;
        ConsoleColor backgroundColor;
        ConsoleColor selectedUIColor; 
        static int selectedMenu;
        static int selectedUI; 
        public bool isDirty = false; 
        public bool IsDirty {  get { return isDirty; } set { isDirty = value; if(isDirty) DrawSelectedUI(); } }
        bool _isRunning = false;
        int clearRegionX,clearRegionY;
        int regionBottomX,regionBottomY;
        int width=0, height=0;
        Thread UIThread;
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
            MenuUI currentMenu = menu[selectedMenu];
            int maxLenght = currentMenu.menuList.Select(x => x.text.Length).Max();
            ClearRegion();
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
            IsDirty =false;
            Console.CursorVisible = false;
            
        }
        public void StartUI()
        {
            _isRunning = true;

           var thread= new Thread(() =>
            {
                ChangeMenu(0);
                while (_isRunning)
                {
                    HandleSelectChange();
                }
            });  
            thread.Start();
        }
        public void StopUI()
        {
            _isRunning = false; 
        } 
        public void ChangeMenu(int newMenu)
        {
            selectedMenu = newMenu;
            MenuUI currentMenu = menu[selectedMenu];
            ClearRegion( );
            int maxLenght = currentMenu.menuList.Select(x => x.text.Length).Max();
            int width = $"{new string(' ', 5)}{currentMenu.title.ToUpper()}{new string(' ', 5 + maxLenght - currentMenu.title.Length)}".Length;
            this.width = width;
            height = currentMenu.menuList.Count+2;
            selectedUI = 0;

            //Console.WriteLine();
            (clearRegionX,clearRegionY)=Console.GetCursorPosition();
            IsDirty = true;

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
            IsDirty = true;
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
            DrawSelectedUI();
            if (keyPressed.Key == ConsoleKey.Enter)
            {
                menu[selectedMenu].menuList[selectedUI].actionToDo();
            }
        }
        void ClearRegion()
        {
            if (width==0) return;
            
            (regionBottomX,regionBottomY)=Console.GetCursorPosition();
            string blank = new string(' ', width);
            for (int row = clearRegionY; row < clearRegionY + height; row++)
            {
                Console.SetCursorPosition(clearRegionX, row);
                Console.Write(blank); 
            }
            // Restore cursor to the top-left of the cleared region
            (var x, var y)=Console.GetCursorPosition();
            if(regionBottomY==y)
            Console.SetCursorPosition(0, clearRegionY);
            else
            Console.SetCursorPosition(0, regionBottomY);

        }
    }
}
