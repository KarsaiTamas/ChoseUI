using System;
using System.Collections.Generic;

namespace TunUI
{
    public class MenuUI
    {
        public string title;
        public List<UIData> menuList;

        public MenuUI(string title, List<UIData> menuList)
        {
            this.title = title;
            this.menuList = menuList;
        }
    }
}
