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
using Newtonsoft.Json;

namespace RoleplayApp
{
    /// <summary>
    /// Interaction logic for Character.xaml
    /// </summary>
    public partial class Character : Window
    {
        string filePath;
        string jsonPath;
        private List<CharacterProp> loadedCharacters;

        public Character()
        {
            InitializeComponent();

            filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RoleplayApp";
            jsonPath = System.IO.Path.Combine(filePath, "characters.json");

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
            CharacterCreationWindow characterCreationWindow = new CharacterCreationWindow(this);
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
                            border.BorderBrush = Brushes.Black;
                            border.BorderThickness = new Thickness(1);
                            border.Margin = new Thickness(5);

                            Image image = null;

                            if (characters.ImagePath != null)
                            {
                            image = new Image();
                            image.Height = 100;
                            image.Width = 100;
                            image.Stretch = Stretch.Fill;
                            image.Source = new BitmapImage(new Uri(characters.ImagePath));
                            }

                            StackPanel stackPanel = new StackPanel();
                            stackPanel.Orientation = Orientation.Vertical;

                            double logScale = Math.Log(characters.ModifiersCombined + 1);

                            if (logScale < Math.Log(4 + 1))
                            {
                                border.Background = new SolidColorBrush(Colors.Pink);
                            }
                            else if (logScale < Math.Log(8 + 1))
                            {
                                border.Background = new SolidColorBrush(Colors.SkyBlue);
                            }
                            else if (logScale < Math.Log(10 + 1))
                            {
                                border.Background = new SolidColorBrush(Colors.Yellow);
                            }
                            else if (logScale < Math.Log(15 + 1))
                            {
                                border.Background = new SolidColorBrush(Colors.DarkRed);
                            }
                            else
                            {
                                border.Background = new SolidColorBrush(Colors.BlueViolet);
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
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1);
                border.Margin = new Thickness(5);

                Image image = null;
                
                if (characters.ImagePath != null)
                {
                    image = new Image();
                    image.Height = 100;
                    image.Width = 100;
                    image.Stretch = Stretch.Fill;
                    image.Source = new BitmapImage(new Uri(characters.ImagePath));
                }

                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Vertical;

                double logScale = Math.Log(characters.ModifiersCombined + 1);

                if (logScale < Math.Log(4 + 1))
                {
                    border.Background = new SolidColorBrush(Colors.Pink);
                }
                else if (logScale < Math.Log(8 + 1))
                {
                    border.Background = new SolidColorBrush(Colors.SkyBlue);
                }
                else if (logScale < Math.Log(10 + 1))
                {
                    border.Background = new SolidColorBrush(Colors.Yellow);
                }
                else if (logScale < Math.Log(15 + 1))
                {
                    border.Background = new SolidColorBrush(Colors.DarkRed);
                }
                else
                {
                    border.Background = new SolidColorBrush(Colors.BlueViolet);
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
            ShowCharacterStats showCharacterStats = new ShowCharacterStats(character, this);
            showCharacterStats.ShowDialog();
        }

        private void EditCharacterInfo(CharacterProp character)
        {
            EditCharacterWindow showCharacterStats = new EditCharacterWindow(character, this);
            showCharacterStats.ShowDialog();
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
