using System;

namespace TunUI
{
    public class UIData
    {
        public string text; 
        public Action actionToDo;

        public UIData(string text, Action uiOption)
        {
            this.text = text;
            this.actionToDo = uiOption;
        }
    }
}
