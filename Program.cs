// See https://aka.ms/new-console-template for more information
using TunUI;

ChoseUI ui = new ChoseUI( ConsoleColor.Gray);
ui.AddUI(new MenuUI("Main Menu", new List<UIData>() {
    new UIData("option1", () => { }),
    new UIData("option2", () => { }),
    new UIData("Exit", ui.StopUI) }));
ui.StartUI();