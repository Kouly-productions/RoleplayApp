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

        private void UpdateCharacterPanel(List<CharacterProp> charactersToShow)
        {
            CharacterPanel.Children.Clear();

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

                CharacterPanel.Children.Add(border);
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
            CharacterPanel.Children.Clear();
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
                case "Navn":
                    SearchBox.Text = default;
                    sortedCharacters = sortedCharacters.OrderBy(c => c.Name).ToList();
                    break;
                case "Level":
                    SearchBox.Text = default;
                    sortedCharacters = sortedCharacters.OrderByDescending(c => c.Level).ToList();
                    break;
                case "Styrke":
                    SearchBox.Text = default;
                    sortedCharacters = sortedCharacters.OrderByDescending(c => c.ModifiersCombined).ToList();
                    break;
            }
            UpdateCharacterPanel(sortedCharacters);
        }
    }
}
