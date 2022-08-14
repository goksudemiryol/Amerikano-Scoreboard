using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Xamarin.Forms;

namespace Amerikano
{
    public class MainPage : ContentPage
    {
        internal static GameCalculations newGame;

        List<SwitchCell> cells;
        Label playingOrderLabel;
        List<PlayerInfo> players;
        Button newGameButton;

        public MainPage()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Language.txt");
            if (File.Exists(filePath))
                Languages.language = int.Parse(File.ReadAllText(filePath));

            Title = Languages.Main[Languages.language];

            players = new List<PlayerInfo>();

            StackLayout stackLayout = new StackLayout();
            this.Content = stackLayout;


            Grid grid = new Grid()
            {
                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) },
                    new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            stackLayout.Children.Add(grid);

            Button playerSettingsButton = new Button()
            {
                Text = Languages.PlayerSettingsButton[Languages.language]
            };
            grid.Children.Add(playerSettingsButton);
            Grid.SetRow(playerSettingsButton, 0);
            Grid.SetColumn(playerSettingsButton, 0);

            playerSettingsButton.Clicked += (object sender, EventArgs e) =>
            {
                if (App.tabbedPage1.Children.Count == 1)
                    App.tabbedPage1.Children.Add(new PlayersPage());

                else if (App.tabbedPage1.Children[1].GetType() != typeof(PlayersPage))
                        App.tabbedPage1.Children[1] = new PlayersPage();

                App.tabbedPage1.CurrentPage = App.tabbedPage1.Children[1];
            };

            Picker languagePicker = new Picker()
            {
                Title = Languages.ChooseLanguage[Languages.language],
                FontSize = 17,
                ItemsSource = Languages.Language,
                SelectedIndex = Languages.language,
                HorizontalTextAlignment = TextAlignment.Center
            };
            grid.Children.Add(languagePicker);
            Grid.SetRow(languagePicker, 0);
            Grid.SetColumn(languagePicker, 1);

            languagePicker.SelectedIndexChanged += (object sender, EventArgs e) =>
            {
                if (App.tabbedPage1.Children.Count == 2)
                    App.tabbedPage1.Children.RemoveAt(1);

                Languages.language = ((Picker)sender).SelectedIndex;
                File.WriteAllText(filePath, Languages.language.ToString());

                App.tabbedPage1.Children[0] = new MainPage();
            };


            var tableView = new TableView();
            stackLayout.Children.Add(tableView);

            var tableRoot = new TableRoot();
            tableView.Root = tableRoot;

            var tableSection = new TableSection(Languages.PlayerSelection[Languages.language]);
            tableRoot.Add(tableSection);

            cells = new List<SwitchCell>();
            for (int i = 0; i < GameCalculations.allPlayers.Count; i++)
            {
                cells.Add(new SwitchCell()
                {
                    Text = GameCalculations.allPlayers[i].Name,
                    ClassId = GameCalculations.allPlayers[i].Id.ToString()
                });
                tableSection.Add(cells[cells.Count - 1]);
            }

            foreach (SwitchCell cell in cells)
            {
                cell.OnChanged += Cell_OnChanged;
                cell.Tapped += Cell_Tapped;
            }


            playingOrderLabel = new Label()
            {
                IsVisible = false,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(10, 28, 10, 0)
            };
            stackLayout.Children.Add(playingOrderLabel);


            Label playerLimitLabel = new Label()
            {
                Text = Languages.PlayerLimit[Languages.language],
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.FromRgb(201,12,12)
            };
            stackLayout.Children.Add(playerLimitLabel);


            Grid grid1 = new Grid()
            {
                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition()
                }
            };
            stackLayout.Children.Add(grid1);


            Button resumeGameButton = new Button()
            {
                Margin = new Thickness(25, 25, 10, 40),
                HeightRequest = 75,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                Text = Languages.ResumeGame[Languages.language],
                BackgroundColor = Color.FromRgb(0x86, 0xF7, 0x72)
            };
            grid1.Children.Add(resumeGameButton);
            Grid.SetRow(resumeGameButton, 0);
            Grid.SetColumn(resumeGameButton, 0);

            resumeGameButton.Clicked += (object sender, EventArgs e) =>
            {
                LastGameSettings.GetScores();
            };


            newGameButton = new Button()
            {
                Margin = new Thickness(10, 25, 25, 40),
                HeightRequest = 75,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                Text = Languages.NewGame[Languages.language],
                BackgroundColor = Color.Red
            };
            grid1.Children.Add(newGameButton);
            Grid.SetRow(newGameButton, 0);
            Grid.SetColumn(newGameButton, 1);

            newGameButton.IsEnabled = false;
            newGameButton.Clicked += (object sender, EventArgs e) =>
            {
                newGame = new GameCalculations(players);

                if (App.tabbedPage1.Children.Count == 1)
                    App.tabbedPage1.Children.Add(new GamePage());
                else
                    App.tabbedPage1.Children[1] = new GamePage();

                App.tabbedPage1.CurrentPage = App.tabbedPage1.Children[1];
            };
        }


        //------------ EVENT HANDLERS ------------//

        private void Cell_OnChanged(object sender, ToggledEventArgs e)
        {            
            SwitchCell switchCell = (SwitchCell)sender;

            //Player data setup
            if (e.Value)
            {
                players.Add(GameCalculations.allPlayers.Where(player => player.Id.ToString() == switchCell.ClassId).ToList()[0]);
            }
            else
            {
                players.RemoveAll(player => player.Id.ToString() == switchCell.ClassId);
            }
            

            //The label that lists the players
            if (players.Count < 1)
            {
                playingOrderLabel.IsVisible = false;
            }
            else if (players.Count == 1)
            {
                playingOrderLabel.IsVisible = true;
                playingOrderLabel.Text = Languages.PlayingOrder[Languages.language] + players[0].Name + ".";
            }
            else
            {
                playingOrderLabel.IsVisible = true;
                playingOrderLabel.Text = Languages.PlayingOrder[Languages.language];
                for (int i = 0; i < players.Count; i++)
                {
                    playingOrderLabel.Text += players[i].Name + ", ";
                }
                playingOrderLabel.Text = playingOrderLabel.Text.Remove(playingOrderLabel.Text.Length - 2);
                playingOrderLabel.Text += ".";
            }


            //Availability of the New Game Button
            if (players.Count >= 3 && players.Count <= 6)
                newGameButton.IsEnabled = true;
            else
                newGameButton.IsEnabled = false;
        }


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