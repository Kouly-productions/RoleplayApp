using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace RoleplayApp.Fantasy
{
    /// <summary>
    /// Interaction logic for FantasyShowCharacters.xaml
    /// </summary>
    public partial class FantasyShowCharacters : Window
    {
        string filePath;
        string jsonPath;
        private List<CharacterProp> loadedCharacters;
        int numOfChar = default;

        public FantasyShowCharacters()
        {
            InitializeComponent();


            filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RoleplayApp";
            jsonPath = System.IO.Path.Combine(filePath, "fantasyCharacters.json");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            GetCharacters();

            SortByComboBox_SelectionChanged(SortByComboBox, null);
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            double x = this.Left;
            double y = this.Top;

            MainWindow mainWindow = new MainWindow();
            mainWindow.Left = x;
            mainWindow.Top = y;
            this.Close();
            mainWindow.Show();
        }

        private void showCountOfCharacters()
        {
            numOfChar = loadedCharacters.Count;

            NumberOfCharacters.Text = numOfChar.ToString();
        }

        private void CreateCharacter_Click(object sender, RoutedEventArgs e)
        {
            FantasyCreateCharacter characterCreationWindow = new FantasyCreateCharacter(this);
            characterCreationWindow.Show();
        }

        public void GetCharacters()
        {
            if (File.Exists(jsonPath))
            {
                try
                {
                    string json = File.ReadAllText(jsonPath);

                    this.loadedCharacters = JsonConvert.DeserializeObject<List<CharacterProp>>(json);

                    if (loadedCharacters != null)
                    {
                        foreach (var characters in loadedCharacters)
                        {

                            Border border = new Border();
                            border.BorderThickness = new Thickness(5);
                            border.Margin = new Thickness(5);
                            if (characters.Gender == Gender.Mand)
                            {
                                border.BorderBrush = Brushes.CornflowerBlue;
                            }
                            else if (characters.Gender == Gender.Kvinde)
                            {
                                border.BorderBrush = Brushes.HotPink;
                            }

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

                            border.MouseLeftButtonDown += (sender, e) => { ShowCharacterInfo(characters); };
                            border.MouseRightButtonDown += (sender, e) => { EditCharacterInfo(characters); };

                            if (image != null)
                            {
                                stackPanel.Children.Add(image);
                            }

                            stackPanel.Children.Add(textBlock);

                            border.Child = stackPanel;

                            FantasyCharacterPanel.Children.Add(border);

                            showCountOfCharacters();
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

        private void UpdateCharacterPanel(List<CharacterProp> charactersToShow)
        {
            FantasyCharacterPanel.Children.Clear();

            foreach (var characters in charactersToShow)
            {
                Border border = new Border();
                border.BorderThickness = new Thickness(5);
                border.Margin = new Thickness(5);
                if (characters.Gender == Gender.Mand)
                {
                    border.BorderBrush = Brushes.CornflowerBlue;
                }
                else if (characters.Gender == Gender.Kvinde)
                {
                    border.BorderBrush = Brushes.HotPink;
                }

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

                border.MouseLeftButtonDown += (sender, e) => { ShowCharacterInfo(characters); };
                border.MouseRightButtonDown += (sender, e) => { EditCharacterInfo(characters); };

                if (image != null)
                {
                    stackPanel.Children.Add(image);
                }
                stackPanel.Children.Add(textBlock);

                border.Child = stackPanel;

                FantasyCharacterPanel.Children.Add(border);
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = (sender as TextBox).Text.ToLower();

            var filteredCharacters = loadedCharacters.Where(c => c.Name.ToLower().Contains(query)).ToList();

            UpdateCharacterPanel(filteredCharacters);
        }

        public void UpdateCharacterList()
        {
            FantasyCharacterPanel.Children.Clear();
            GetCharacters();
        }

        private void ShowCharacterInfo(CharacterProp character)
        {
            FantasyShowCharacterStats showCharacterStats = new FantasyShowCharacterStats(character, this);
            showCharacterStats.ShowDialog();
        }

        private void EditCharacterInfo(CharacterProp character)
        {
            FantasyEditCharacter fantasyEditCharacterStats = new FantasyEditCharacter(character, this);
            fantasyEditCharacterStats.ShowDialog();
        }

        private void SortByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loadedCharacters == null)
            {
                return;
            }

            string selectedOption = (SortByComboBox.SelectedItem as ComboBoxItem).Content.ToString();

            List<CharacterProp> sortedCharacters = new List<CharacterProp>(loadedCharacters);

            switch (selectedOption)
            {
                case "Level":
                    SearchBox.Text = default;
                    sortedCharacters = sortedCharacters.OrderByDescending(c => c.Level).ToList();
                    break;
                case "Styrke":
                    SearchBox.Text = default;
                    sortedCharacters = sortedCharacters.OrderByDescending(c => c.Power).ToList();
                    break;
            }
            UpdateCharacterPanel(sortedCharacters);
        }
    }
}
