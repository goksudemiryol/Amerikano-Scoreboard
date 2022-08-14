using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Amerikano
{
    public class PlayersPage : ContentPage
    {
        public PlayersPage()
        {
            Title = Languages.PlayerSettings[Languages.language];

            StackLayout stackLayout = new StackLayout();
            this.Content = stackLayout;


            var tableView = new TableView();
            stackLayout.Children.Add(tableView);

            var tableRoot = new TableRoot();
            tableView.Root = tableRoot;

            var tableSection = new TableSection(Languages.PlayerInfo[Languages.language]);
            tableRoot.Add(tableSection);

            EntryCell entryCell_Name = new EntryCell() { Placeholder = Languages.PlayerName[Languages.language], Text = ""  };
            EntryCell entryCell_FirstNick = new EntryCell() { Placeholder = Languages.FirstNick[Languages.language], Text = "" };
            EntryCell entryCell_SecondNick = new EntryCell() { Placeholder = Languages.SecondNick[Languages.language], Text = "" };

            tableSection.Add(entryCell_Name);
            tableSection.Add(entryCell_FirstNick);
            tableSection.Add(entryCell_SecondNick);


            Button addPlayerButton = new Button()
            {
                Background = new SolidColorBrush(Color.FromRgb(0x73, 0xB2, 0xF5)),
                Text = Languages.AddPlayer[Languages.language],
                FontSize = 19
            };
            stackLayout.Children.Add(addPlayerButton);

            addPlayerButton.Clicked += (object sender, EventArgs e) =>
            {
                entryCell_Name.Text = entryCell_Name.Text.Trim();
                if (entryCell_Name.Text == String.Empty)
                    return;

                entryCell_FirstNick.Text = entryCell_FirstNick.Text.Trim();
                entryCell_SecondNick.Text = entryCell_SecondNick.Text.Trim();
                
                List<string> nickNamesList = new List<string>()
                {
                    entryCell_Name.Text,
                    entryCell_FirstNick.Text,
                    entryCell_SecondNick.Text
                };

                string[] nickNamesArray = nickNamesList.Where(nick => nick != String.Empty).ToArray();

                GameCalculations.allPlayers.Add(new PlayerInfo()
                {
                    Id = GameCalculations.allPlayers.Count + 1,
                    Name = entryCell_Name.Text,
                    Nicknames = nickNamesArray
                });

                PlayerSettings.SetPlayersToFile(GameCalculations.allPlayers);

                App.tabbedPage1.Children[0] = new MainPage();
                App.tabbedPage1.Children[1] = new PlayersPage();

                App.tabbedPage1.CurrentPage = App.tabbedPage1.Children[1];
            };


            Label label = new Label
            {
                FontSize = 40
            };
            stackLayout.Children.Add(label);


            var tableView1 = new TableView();
            stackLayout.Children.Add(tableView1);

            var tableRoot1 = new TableRoot();
            tableView1.Root = tableRoot1;

            var tableSection1 = new TableSection(Languages.Players[Languages.language]);
            tableRoot1.Add(tableSection1);

            var cells = new List<SwitchCell>();
            for (int i = 0; i < GameCalculations.allPlayers.Count; i++)
            {
                cells.Add(new SwitchCell()
                {
                    Text = GameCalculations.allPlayers[i].Name,
                    ClassId = GameCalculations.allPlayers[i].Id.ToString()
                });
                tableSection1.Add(cells[cells.Count - 1]);
            }

            foreach (SwitchCell cell in cells)
                cell.Tapped += Cell_Tapped;


            Button deletePlayerButton = new Button()
            {
                Background = new SolidColorBrush(Color.FromRgb(0xE5, 0x16, 0x16)),
                Text = Languages.DeletePlayer[Languages.language],
                FontSize = 19
            };
            stackLayout.Children.Add(deletePlayerButton);

            deletePlayerButton.Clicked += async (object sender, EventArgs e) =>
            {
                bool answer = await DisplayAlert(Languages.Question[Languages.language], Languages.Description[Languages.language], Languages.Yes[Languages.language], Languages.No[Languages.language]);

                if (answer)
                {
                    int count = GameCalculations.allPlayers.Count;

                    foreach (SwitchCell switchCell in cells)
                        if (switchCell.On)
                            GameCalculations.allPlayers.RemoveAll(player => player.Id.ToString() == switchCell.ClassId);

                    if (count == GameCalculations.allPlayers.Count)
                        return;
                    else
                    {
                        GameCalculations.IDSetter();
                        PlayerSettings.SetPlayersToFile(GameCalculations.allPlayers);

                        App.tabbedPage1.Children[0] = new MainPage();
                        App.tabbedPage1.Children[1] = new PlayersPage();

                        App.tabbedPage1.CurrentPage = App.tabbedPage1.Children[1];
                    }
                }
            };
        }


        //------------ EVENT HANDLERS ------------//

        private void Cell_Tapped(object sender, EventArgs e)
        {
            SwitchCell switchCell = (SwitchCell)sender;

            if (switchCell.On)
                switchCell.On = false;
            else
                switchCell.On = true;
        }
    }
}