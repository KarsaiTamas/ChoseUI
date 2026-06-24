I had a fun idea for console app in c#.
So this is just a simple select menu for console app.
It's similar to the console what fallout has in the PCs in functionality.
So you can add menus to the UI like this: 
Just setup ChoseUI with a selection color:
ChoseUI ui = new ChoseUI( ConsoleColor.Gray);

Than you can add menus, with Select options, which has a text and functionality:
ui.AddUI(new MenuUI("Main Menu", new List<UIData>() {
    new UIData("option1", () => { }),
    new UIData("option2", () => { }),
    new UIData("Exit", ui.StopUI) }));

Than you just start it with:
ui.StartUI();

I don't know if there's a better way of doing this, but this gonna work perfectly fine for what I want to use this for.
