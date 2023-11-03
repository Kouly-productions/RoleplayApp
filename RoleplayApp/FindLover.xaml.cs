using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using RoleplayApp.Fantasy;

namespace RoleplayApp
{
    /// <summary>
    /// Interaction logic for FindLover.xaml
    /// </summary>
    public partial class FindLover : Window
    {
        public CharacterCreationWindow ParentWindow { get; set; }

        private FantasyEditCharacter fantasyEditParentWindow;
        private FantasyCreateCharacter fantasyCreateCharacterWindow;
        private CharacterCreationWindow ParentCreationWindow;
        private EditCharacterWindow ParentEditWindow;

        string filePath;
        string jsonPath;

        public FindLover(CharacterCreationWindow parentWindow)
        {
            this.ParentCreationWindow = parentWindow;
            InitializeCommonCode();
        }

        public FindLover(FantasyCreateCharacter parentWindow)
        {
            this.fantasyCreateCharacterWindow = parentWindow;
            InitializeCommonCode();
        }

        public FindLover(FantasyEditCharacter parentWindow)
        {
            this.fantasyEditParentWindow = parentWindow;
            InitializeCommonCode();
        }

        public FindLover(EditCharacterWindow parentWindow)
        {
            this.ParentEditWindow = parentWindow;
            InitializeCommonCode();
        }

        public void InitializeCommonCode()
        {
            InitializeComponent();

            filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RoleplayApp";
            if (fantasyEditParentWindow != null || fantasyCreateCharacterWindow != null)
            {
                jsonPath = System.IO.Path.Combine(filePath, "fantasyCharacters.json");
            }
            else
            {
                jsonPath = System.IO.Path.Combine(filePath, "characters.json");
            }

            GetCharacters();
        }

        public void GetCharacters()
        {
            if (File.Exists(jsonPath))
            {
                try
                {
                    string json = File.ReadAllText(jsonPath);

                    List<CharacterProp> loadedCharacters = JsonConvert.DeserializeObject<List<CharacterProp>>(json);
                    if (loadedCharacters != null)
                    {
                        foreach (var characters in loadedCharacters)
                        {
                            Border border = new Border();
                            border.BorderBrush = Brushes.Black;
                            border.BorderThickness = new Thickness(1);
                            border.Margin = new Thickness(5);

                            Image image = null;

                            if (characters.ImagePath != null)
                            {
                                string fullImagePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RoleplayApp\\Images", characters.ImagePath);
                                if (System.IO.File.Exists(fullImagePath))
                                {
                                    image = new Image();
                                    image.Height = 100;
                                    image.Width = 100;
                                    image.Stretch = Stretch.Fill;
                                    image.Source = new BitmapImage(new Uri(fullImagePath, UriKind.Absolute));
                                }
                            }

                            StackPanel stackPanel = new StackPanel();
                            stackPanel.Orientation = Orientation.Vertical;

                            TextBlock textBlockRank = new TextBlock();
                            textBlockRank.FontSize = 16;
                            textBlockRank.Width = 170;
                            textBlockRank.Margin = new Thickness(5);
                            textBlockRank.TextAlignment = TextAlignment.Center;
                            textBlockRank.FontWeight = FontWeights.Bold;

                            if (fantasyEditParentWindow != null || fantasyCreateCharacterWindow != null)
                            {
                                if (characters.Power == Power.MegetSvag)
                                {
                                    textBlockRank.Text = "Meget Svag";
                                    border.Background = new SolidColorBrush(Colors.DarkGray);
                                    stackPanel.Children.Add(textBlockRank);
                                }
                                else if (characters.Power == Power.Svag)
                                {
                                    textBlockRank.Text = "Svag";
                                    border.Background = new SolidColorBrush(Colors.Gray);
                                    stackPanel.Children.Add(textBlockRank);
                                }
                                else if (characters.Power == Power.Menneske)
                                {
                                    textBlockRank.Text = "Menneske";
                                    border.Background = new SolidColorBrush(Colors.Beige);
                                    stackPanel.Children.Add(textBlockRank);
                                }
                                else if (characters.Power == Power.Trænet)
                                {
                                    textBlockRank.Text = "Trænet";
                                    border.Background = new SolidColorBrush(Colors.Yellow);
                                    stackPanel.Children.Add(textBlockRank);
                                }
                                else if (characters.Power == Power.Stærk)
                                {
                                    textBlockRank.Text = "Stærk";
                                    border.Background = new SolidColorBrush(Colors.OrangeRed);
                                    stackPanel.Children.Add(textBlockRank);
                                }
                                else if (characters.Power == Power.Elite)
                                {
                                    textBlockRank.Text = "Elite";
                                    border.Background = new SolidColorBrush(Colors.Gold);
                                    stackPanel.Children.Add(textBlockRank);
                                }
                                else if (characters.Power == Power.Mystisk)
                                {
                                    textBlockRank.Text = "Mystisk";
                                    border.Background = new SolidColorBrush(Colors.Green);
                                    stackPanel.Children.Add(textBlockRank);
                                }
                                else if (characters.Power == Power.OP)
                                {
                                    textBlockRank.Text = "OP";
                                    border.Background = new SolidColorBrush(Colors.DarkRed);
                                    stackPanel.Children.Add(textBlockRank);
                                }
                                else if (characters.Power == Power.BROKEN)
                                {
                                    textBlockRank.Text = "BROKEN";
                                    border.Background = new SolidColorBrush(Colors.Cyan);
                                    stackPanel.Children.Add(textBlockRank);
                                }
                            }
                            else
                            {
                                if (characters.ModifiersCombined <= 4)
                                {
                                    textBlockRank.Text = "Svag";
                                    border.Background = new SolidColorBrush(Colors.Pink);
                                    stackPanel.Children.Add(textBlockRank);
                                }
                                else if (characters.ModifiersCombined <= 10)
                                {
                                    textBlockRank.Text = "Okay";
                                    border.Background = new SolidColorBrush(Colors.SkyBlue);
                                    stackPanel.Children.Add(textBlockRank);
                                }
                                else if (characters.ModifiersCombined <= 20)
                                {
                                    textBlockRank.Text = "Stærk";
                                    border.Background = new SolidColorBrush(Colors.Yellow);
                                    stackPanel.Children.Add(textBlockRank);
                                }
                                else if (characters.ModifiersCombined <= 40)
                                {
                                    textBlockRank.Text = "OP";
                                    border.Background = new SolidColorBrush(Colors.OrangeRed);
                                    stackPanel.Children.Add(textBlockRank);
                                }
                                else if (characters.ModifiersCombined <= 50)
                                {
                                    textBlockRank.Text = "BROKEN";
                                    border.Background = new SolidColorBrush(Colors.Cyan);
                                    stackPanel.Children.Add(textBlockRank);
                                }
                            }



                            TextBlock textBlock = new TextBlock();
                            textBlock.FontSize = 16;
                            textBlock.Width = 170;
                            textBlock.Margin = new Thickness(5);

                            Run nameRun = new Run();
                            nameRun.Text = characters.Name;

                            Run levelRun = new Run();
                            levelRun.Text = "Level " + characters.Level.ToString();
                            levelRun.Foreground = new SolidColorBrush(Colors.BlueViolet);

                            textBlock.Inlines.Add(nameRun);
                            textBlock.Inlines.Add(new Run { Text = "  " });
                            textBlock.Inlines.Add(levelRun);

                            textBlock.TextAlignment = TextAlignment.Center;

                            border.MouseLeftButtonDown += (sender, e) => { AddAsLover(characters); };

                            if (image != null)
                            {
                                stackPanel.Children.Add(image);
                            }
                            stackPanel.Children.Add(textBlock);
                            border.Child = stackPanel;

                            CharacterPanel.Children.Add(border);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kunne ikke læse fil" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("filen findes ikke");
                System.IO.File.WriteAllText(jsonPath, "[]");
            }
        }

        private void AddAsLover(CharacterProp selectedCharacter)
        {
            if (ParentCreationWindow != null)
            {
                ParentCreationWindow.SelectedLover = selectedCharacter.Name;
            }
            else if (ParentEditWindow != null)
            {
                ParentEditWindow.SelectedLover = selectedCharacter.Name;
            }
            else if (fantasyEditParentWindow != null)
            {
                fantasyEditParentWindow.SelectedLover = selectedCharacter.Name;
            }
            else if (fantasyCreateCharacterWindow != null)
            {
                fantasyCreateCharacterWindow.SelectedLover = selectedCharacter.Name;
            }
            this.Close();
        }
    }
}
