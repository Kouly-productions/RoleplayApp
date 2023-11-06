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

namespace RoleplayApp.Fantasy
{
    public partial class FantasyAbilityWindow : Window
    {
        string filePath;
        string jsonPathAbility;
        string jsonPathCharacter;
        private List<Forces> loadedAbilities;
        public CharacterProp CurrentCharacter { get; set; }

        public FantasyAbilityWindow()
        {
            InitializeComponent();

            filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RoleplayApp";
            jsonPathAbility = System.IO.Path.Combine(filePath, "fantasyAbility.json");
            jsonPathCharacter = System.IO.Path.Combine(filePath, "fantasyCharacters.json");

            GetAbilities();
            SortByComboBox_SelectionChanged(SortByComboBox, null);
        }

        private void CreateAbility_Click(object sender, RoutedEventArgs e)
        {
            double x = this.Left;
            double y = this.Top;

            FantasyAddPower fantasyAddPower = new FantasyAddPower(this);
            fantasyAddPower.Left = x;
            fantasyAddPower.Top = y;
            fantasyAddPower.Show();
        }

        public void GetAbilities()
        {
            if (File.Exists(jsonPathAbility))
            {
                try
                {
                    string json = File.ReadAllText(jsonPathAbility);

                    this.loadedAbilities = JsonConvert.DeserializeObject<List<Forces>>(json);

                    if (loadedAbilities != null)
                    {
                        foreach (var abilities in loadedAbilities)
                        {

                            Border border = new Border();
                            border.BorderThickness = new Thickness(5);
                            border.Margin = new Thickness(5);
                            border.BorderBrush = Brushes.Black;

                            Image image = null;

                            if (abilities.imagePath != null)
                            {
                                string fullImagePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RoleplayApp\\Images", abilities.imagePath);
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

                            if (abilities.abilityType == AbilityType.Ild)
                            {
                                textBlockRank.Text = "Ild";
                                border.Background = new SolidColorBrush(Colors.Red);
                                stackPanel.Children.Add(textBlockRank);
                            }
                            else if (abilities.abilityType == AbilityType.Vand)
                            {
                                textBlockRank.Text = "Vand";
                                border.Background = new SolidColorBrush(Colors.Blue);
                                stackPanel.Children.Add(textBlockRank);
                            }
                            else if (abilities.abilityType == AbilityType.Is)
                            {
                                textBlockRank.Text = "Is";
                                border.Background = new SolidColorBrush(Colors.SkyBlue);
                                stackPanel.Children.Add(textBlockRank);
                            }
                            else if (abilities.abilityType == AbilityType.Elektricitet)
                            {
                                textBlockRank.Text = "Strøm";
                                border.Background = new SolidColorBrush(Colors.Beige);
                                stackPanel.Children.Add(textBlockRank);
                            }
                            else if (abilities.abilityType == AbilityType.Natur)
                            {
                                textBlockRank.Text = "Natur";
                                border.Background = new SolidColorBrush(Colors.LightGreen);
                                stackPanel.Children.Add(textBlockRank);
                            }
                            else if (abilities.abilityType == AbilityType.Healing)
                            {
                                textBlockRank.Text = "Healing";
                                border.Background = new SolidColorBrush(Colors.Yellow);
                                stackPanel.Children.Add(textBlockRank);
                            }

                            TextBlock textBlock = new TextBlock();
                            textBlock.FontSize = 16;
                            textBlock.Width = 170;
                            textBlock.Margin = new Thickness(5);

                            Run nameRun = new Run();
                            nameRun.Text = abilities.Name;

                            Run levelRun = new Run();
                            levelRun.Text = "Level " + abilities.AbilityLevelRequirement.ToString();
                            levelRun.Foreground = new SolidColorBrush(Colors.BlueViolet);

                            textBlock.Inlines.Add(nameRun);
                            textBlock.Inlines.Add(new Run { Text = "  " });
                            textBlock.Inlines.Add(levelRun);

                            textBlock.TextAlignment = TextAlignment.Center;

                            border.MouseRightButtonDown += (sender, e) => { ShowAbilityInfo(abilities); };
                            border.MouseLeftButtonDown += (sender, e) => { AddAbilityToThisCharacter(abilities); };

                            if (image != null)
                            {
                                stackPanel.Children.Add(image);
                            }

                            stackPanel.Children.Add(textBlock);

                            border.Child = stackPanel;

                            FantasyAbilityPanel.Children.Add(border);
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
                System.IO.File.WriteAllText(jsonPathAbility, "[]");
            }
        }

        private void UpdateAbilityPanel(List<Forces> abilityToShow)
        {
            FantasyAbilityPanel.Children.Clear();

            foreach (var abilities in abilityToShow)
            {
                Border border = new Border();
                border.BorderThickness = new Thickness(5);
                border.Margin = new Thickness(5);
                border.BorderBrush = Brushes.Black;

                Image image = null;

                if (abilities.imagePath != null)
                {
                    string fullImagePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RoleplayApp\\Images", abilities.imagePath);
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

                if (abilities.abilityType == AbilityType.Ild)
                {
                    textBlockRank.Text = "Ild";
                    border.Background = new SolidColorBrush(Colors.Red);
                    stackPanel.Children.Add(textBlockRank);
                }
                else if (abilities.abilityType == AbilityType.Vand)
                {
                    textBlockRank.Text = "Vand";
                    border.Background = new SolidColorBrush(Colors.Blue);
                    stackPanel.Children.Add(textBlockRank);
                }
                else if (abilities.abilityType == AbilityType.Is)
                {
                    textBlockRank.Text = "Is";
                    border.Background = new SolidColorBrush(Colors.SkyBlue);
                    stackPanel.Children.Add(textBlockRank);
                }
                else if (abilities.abilityType == AbilityType.Elektricitet)
                {
                    textBlockRank.Text = "Strøm";
                    border.Background = new SolidColorBrush(Colors.Beige);
                    stackPanel.Children.Add(textBlockRank);
                }
                else if (abilities.abilityType == AbilityType.Natur)
                {
                    textBlockRank.Text = "Natur";
                    border.Background = new SolidColorBrush(Colors.LightGreen);
                    stackPanel.Children.Add(textBlockRank);
                }
                else if (abilities.abilityType == AbilityType.Healing)
                {
                    textBlockRank.Text = "Healing";
                    border.Background = new SolidColorBrush(Colors.Yellow);
                    stackPanel.Children.Add(textBlockRank);
                }

                TextBlock textBlock = new TextBlock();
                textBlock.FontSize = 16;
                textBlock.Width = 170;
                textBlock.Margin = new Thickness(5);

                Run nameRun = new Run();
                nameRun.Text = abilities.Name;

                Run levelRun = new Run();
                levelRun.Text = "Level " + abilities.AbilityLevelRequirement.ToString();
                levelRun.Foreground = new SolidColorBrush(Colors.BlueViolet);

                textBlock.Inlines.Add(nameRun);
                textBlock.Inlines.Add(new Run { Text = "  " });
                textBlock.Inlines.Add(levelRun);

                textBlock.TextAlignment = TextAlignment.Center;

                border.MouseRightButtonDown += (sender, e) => { ShowAbilityInfo(abilities); };
                border.MouseLeftButtonDown += (sender, e) => { AddAbilityToThisCharacter(abilities); };

                if (image != null)
                {
                    stackPanel.Children.Add(image);
                }
                stackPanel.Children.Add(textBlock);

                border.Child = stackPanel;

                FantasyAbilityPanel.Children.Add(border);
            }
        }

        private void ShowAbilityInfo(Forces ability)
        {
            FantasyThisAbility showAbilityStats = new FantasyThisAbility(ability, this);
            showAbilityStats.ShowDialog();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Empty for now
        }

        private void SortByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Empty for now
        }

        private void AddAbilityToThisCharacter(Forces ability)
        {
            if (CurrentCharacter != null)
            {
                if (CurrentCharacter.Abilities == null)
                    CurrentCharacter.Abilities = new List<Forces>();

                // Tjek om ability allerede findes
                if (!CurrentCharacter.Abilities.Any(a => a.Name == ability.Name))
                {
                    CurrentCharacter.Abilities.Add(ability);
                    // Debug udskrift eller breakpoint her for at inspicere CurrentCharacter
                    SaveCharacter();
                }
                else
                {
                    MessageBox.Show("Denne ability er allerede tilføjet til karakteren.");
                }
            }
            else
            {
                MessageBox.Show("Ingen nuværende karakter valgt.");
            }
             this.Close();
        }


        private List<CharacterProp> LoadCharactersFromJson(string characterJsonPath)
        {
            if (File.Exists(characterJsonPath))
            {
                string json = File.ReadAllText(characterJsonPath);
                return JsonConvert.DeserializeObject<List<CharacterProp>>(json) ?? new List<CharacterProp>();
            }
            else
            {
                return new List<CharacterProp>();
            }
        }


        private void SaveCharacter()
        {
            try
            {
                // Load the existing characters from the JSON file
                var allCharacters = LoadCharactersFromJson(jsonPathCharacter); // characterJsonPath skal være stien til din karakterdatafil

                // Find the character to update in the loaded list (assuming Name is a unique identifier)
                var characterToUpdate = allCharacters.FirstOrDefault(c => c.Name == CurrentCharacter.Name);

                // Update the character's abilities
                if (characterToUpdate != null)
                {
                    characterToUpdate.Abilities = CurrentCharacter.Abilities;
                }
                else
                {
                    // If the character is not found, it's a new character, so add it to the list
                    allCharacters.Add(CurrentCharacter);
                }

                // Save the updated list of characters back to the JSON file
                string json = JsonConvert.SerializeObject(allCharacters);
                File.WriteAllText(jsonPathCharacter, json); // characterJsonPath skal være stien til din karakterdatafil
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kunne ikke gemme karakter: " + ex.Message);
            }
        }


    }
}
