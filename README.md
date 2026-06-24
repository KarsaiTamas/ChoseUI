<br>I had a fun idea for console app in c#.</br>
<br>So this is just a simple select menu for console app.</br>
<br>It's similar to the console what fallout has in the PCs in functionality.</br>
<br>So you can add menus to the UI like this: </br>
<br>Just setup ChoseUI with a selection color:</br>
<br>ChoseUI ui = new ChoseUI( ConsoleColor.Gray);</br>

<br>Than you can add menus, with Select options, which has a text and functionality:</br>
<br>ui.AddUI(new MenuUI("Main Menu", new List<UIData>() {</br>
<br>    new UIData("option1", () => { }),</br>
<br>    new UIData("option2", () => { }),</br>
<br>    new UIData("Exit", ui.StopUI) }));</br>
<br>Than you just start it with:</br>
<br>ui.StartUI();</br>

<br>I don't know if there's a better way of doing this, but this gonna work perfectly fine for what I want to use this for.</br>
