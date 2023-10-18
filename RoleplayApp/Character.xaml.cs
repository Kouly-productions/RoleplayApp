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

        public Character()
        {
            InitializeComponent();

            //filePath = "C:\\Users\\EFDK\\AppData\\Roaming\\RoleplayApp";
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

                    List<CharacterProp> loadedCharacters = JsonConvert.DeserializeObject<List<CharacterProp>>(json);

                    if (loadedCharacters != null)
                    {
                        foreach (var characters in loadedCharacters)
                        {
                            Border border = new Border();
                            border.BorderBrush = Brushes.Black;
                            border.BorderThickness = new Thickness(1);
                            border.Margin = new Thickness(5);

                            TextBlock textBlock = new TextBlock();
                            textBlock.FontSize = 20;
                            textBlock.Height = 30;
                            textBlock.Width = 150;
                            textBlock.Margin = new Thickness(5);

                            Run nameRun = new Run();
                            nameRun.Text = characters.Name;

                            Run levelRun = new Run();
                            levelRun.Text = "Level " + characters.Level.ToString();
                            levelRun.Foreground = new SolidColorBrush(Colors.BlueViolet);

                            textBlock.Inlines.Add(nameRun);
                            textBlock.Inlines.Add(new Run { Text = "  " });
                            textBlock.Inlines.Add(levelRun);

                            textBlock.TextAlignment = TextAlignment.Right;

                            border.MouseLeftButtonDown += (sender, e) => { ShowCharacterInfo(characters); };

                            border.Child = textBlock;

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
    }
}
