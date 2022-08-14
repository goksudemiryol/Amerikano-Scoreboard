using System;
using Xamarin.Forms;

namespace Amerikano
{
    public partial class GamePage : ContentPage
    {
        internal static Entry[,] entries;
        internal static Label[] labels;
        internal static Button[] buttonsOfTypes;

        static bool locker = false;

        public GamePage()
        {
            Title = Languages.Game[Languages.language];

            Grid gridFirstLine = new Grid()
            {
                Margin = new Thickness(5, 5, 5, 5),
                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(65) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = new GridLength(2.5, GridUnitType.Star) }
                }
            };

            for (int i = 0; i < GameCalculations.Players.Count; i++)
                gridFirstLine.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });


            Button button = new Button()
            {
                Text = Languages.Lock[Languages.language],
                Background = new SolidColorBrush(Color.FromRgb(0x73, 0xB2, 0xF5)),
                BorderColor = Color.FromRgb(0x73, 0xB2, 0xF5),
                FontSize = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            Grid.SetRow(button, 0);
            Grid.SetColumn(button, 0);
            gridFirstLine.Children.Add(button);

            button.Clicked += Button_Clicked;


            //----- Players Are Populated -----------------------------------------------------------------//
            for (int i = 1; i <= GameCalculations.Players.Count; i++)
            {
                Random rnd = new Random();
                int nick = rnd.Next(GameCalculations.Players[i - 1].Nicknames.Length);

                Picker picker = new Picker()
                {
                    ItemsSource = GameCalculations.Players[i - 1].Nicknames,
                    SelectedItem = GameCalculations.Players[i - 1].Nicknames[nick],
                    TitleColor = Color.Black,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    Rotation = -45,
                    Margin = new Thickness(-20, 0, -20, 0)
                };

                Grid.SetRow(picker, 0);
                Grid.SetColumn(picker, i);
                gridFirstLine.Children.Add(picker);
            }


            Grid gridRest = new Grid()
            {
                Margin = new Thickness(5, 5),
                ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = new GridLength(2.5, GridUnitType.Star) }
                }
            };

            for (int i = 0; i < GameCalculations.gameTypeCount; i++)
                gridRest.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(85)
                });

            for (int i = 0; i < GameCalculations.Players.Count; i++)
                gridRest.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });

            gridRest.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(90) });


            //----- Game Types Are Populated ------------------------------------------------------------//
            buttonsOfTypes = new Button[GameCalculations.gameTypeCount];
            for (int i = 1; i <= GameCalculations.gameTypeCount; i++)
            {
                Button buttonOfTypes = new Button()
                {
                    Text = Languages.GameTypes[Languages.language, i - 1].ToString(),
                    TextColor = Color.Black,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    ClassId = i.ToString()
                };
                buttonsOfTypes[i - 1] = buttonOfTypes;

                Grid.SetRow(buttonOfTypes, i - 1);
                Grid.SetColumn(buttonOfTypes, 0);
                gridRest.Children.Add(buttonOfTypes);

                buttonOfTypes.Clicked += ButtonOfTypes_Clicked;
            }


            //----- Game Scores ------------------------------------------------------------------------//
            entries = new Entry[GameCalculations.Players.Count, GameCalculations.gameTypeCount];
            for (short i = 1; i <= GameCalculations.gameTypeCount; i++)
            {
                for (short j = 1; j <= GameCalculations.Players.Count; j++)
                {
                    Entry entry = new Entry()
                    {
                        ClassId = $"{i}-{j}",
                        Text = String.Empty,
                        Background = new SolidColorBrush(Color.FromRgb(0xF0, 0xEE, 0x73)),
                        Keyboard = Keyboard.Numeric,
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center
                    };
                    entries[j - 1, i - 1] = entry;

                    Grid.SetRow(entry, i - 1);
                    Grid.SetColumn(entry, j);
                    gridRest.Children.Add(entry);

                    entry.TextChanged += Entry_TextChanged;
                }
            }


            //----- Total Scores Line --------------------------------------------------------------------//
            Label totalScores = new Label()
            {
                Text = Languages.Total[Languages.language],
                FontSize = 21,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 15, 0, 15)
            };

            Grid.SetRow(totalScores, GameCalculations.gameTypeCount);
            Grid.SetColumn(totalScores, 0);
            gridRest.Children.Add(totalScores);


            //----- Total Scores ------------------------------------------------------------------------//
            labels = new Label[GameCalculations.Players.Count];
            for (int i = 1; i <= GameCalculations.Players.Count; i++)
            {
                Label label = new Label()
                {
                    Text = String.Empty,
                    FontSize = 23,
                    TextColor = Color.Black,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Margin = new Thickness(-30, -15, -30, -15)
                };
                labels[i - 1] = label;

                Grid.SetRow(label, GameCalculations.gameTypeCount);
                Grid.SetColumn(label, i);
                gridRest.Children.Add(label);
            }


            ScrollView scrollView = new ScrollView()
            {
                Content = gridRest,
            };


            StackLayout stackLayout = new StackLayout();
            stackLayout.Children.Add(gridFirstLine);
            stackLayout.Children.Add(scrollView);
            this.Content = stackLayout;
        }


        //------------ EVENT HANDLERS ------------//

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!locker)
            {
                for (int i = 0; i < GameCalculations.gameTypeCount; i++)
                {
                    buttonsOfTypes[i].IsEnabled = false;
                }

                for (short i = 0; i < GameCalculations.gameTypeCount; i++)
                {
                    for (short j = 0; j < GameCalculations.Players.Count; j++)
                    {
                        entries[j, i].IsEnabled = false;
                    }
                }

                locker = true;
            }

            else
            {
                for (int i = 0; i < GameCalculations.gameTypeCount; i++)
                {
                    buttonsOfTypes[i].IsEnabled = true;
                }

                for (short i = 0; i < GameCalculations.gameTypeCount; i++)
                {
                    for (short j = 0; j < GameCalculations.Players.Count; j++)
                    {
                        entries[j, i].IsEnabled = true;
                    }
                }

                locker = false;
            }
        }


        internal static void ButtonOfTypes_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (button.TextColor == Color.Black)
            {
                button.TextColor = Color.Crimson;
                button.FontSize = 13.5;
                GameCalculations.gameTracker++;
                GameCalculations.gameTypeTracker.Add(button.ClassId);

                int language = Languages.language;
                switch (language)
                {
                    case (int)LanguagesEnum.English:
                        button.Text += $" /\nHAND {GameCalculations.gameTracker}, {GameCalculations.Players[(GameCalculations.gameTracker - 1) % GameCalculations.Players.Count].Name} IS THE PICKER";
                        break;

                    case (int)LanguagesEnum.Turkish:
                        button.Text += $" /\n{GameCalculations.gameTracker}. EL, {GameCalculations.Players[(GameCalculations.gameTracker - 1) % GameCalculations.Players.Count].Name} SEÇTİ";
                        break;
                }
            }
            else
            {
                button.TextColor = Color.Black;
                button.FontSize = 18;
                GameCalculations.gameTracker--;
                GameCalculations.gameTypeTracker.RemoveAt(GameCalculations.gameTypeTracker.Count - 1);
                button.Text = Languages.GameTypes[Languages.language, Int32.Parse(button.ClassId) - 1];
            }
            MainPage.newGame.SetScores();
        }


        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            string oldTextValue = e.OldTextValue;
            string newTextValue = e.NewTextValue;
            MainPage.newGame.Update(entry, oldTextValue, newTextValue);
            MainPage.newGame.SetScores();
        }
    }
}
