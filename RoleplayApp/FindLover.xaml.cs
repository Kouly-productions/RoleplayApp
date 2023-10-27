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

namespace RoleplayApp
{
    /// <summary>
    /// Interaction logic for FindLover.xaml
    /// </summary>
    public partial class FindLover : Window
    {
        public CharacterCreationWindow ParentWindow { get; set; }

        private CharacterCreationWindow ParentCreationWindow;
        private EditCharacterWindow ParentEditWindow;

        string filePath;
        string jsonPath;

        public FindLover(CharacterCreationWindow parentWindow)
        {
            InitializeCommonCode();
            this.ParentCreationWindow = parentWindow;
        }

        public FindLover(EditCharacterWindow parentWindow)
        {
            InitializeCommonCode();
            this.ParentEditWindow = parentWindow;
        }

        public void InitializeCommonCode()
        {
            InitializeComponent();

            filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RoleplayApp";
            jsonPath = System.IO.Path.Combine(filePath, "characters.json");

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

                            if (characters.ModifiersCombined <= 4 && characters.StatsCombined <= 50)
                            {
                                textBlockRank.Text = "Svag";
                                border.Background = new SolidColorBrush(Colors.Pink);
                                stackPanel.Children.Add(textBlockRank);
                            }
                            else if (characters.ModifiersCombined <= 10 && characters.StatsCombined <= 90)
                            {
                                textBlockRank.Text = "Okay";
                                border.Background = new SolidColorBrush(Colors.SkyBlue);
                                stackPanel.Children.Add(textBlockRank);
                            }
                            else if (characters.ModifiersCombined <= 20 && characters.StatsCombined <= 200)
                            {
                                textBlockRank.Text = "Stærk";
                                border.Background = new SolidColorBrush(Colors.Yellow);
                                stackPanel.Children.Add(textBlockRank);
                            }
                            else if (characters.ModifiersCombined <= 40 && characters.StatsCombined <= 250)
                            {
                                textBlockRank.Text = "OP";
                                border.Background = new SolidColorBrush(Colors.OrangeRed);
                                stackPanel.Children.Add(textBlockRank);
                            }
                            else if (characters.ModifiersCombined <= 50 && characters.StatsCombined <= 350)
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
            this.Close();
        }
    }
}
