using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using static RoleplayApp.CharacterProp;

namespace RoleplayApp.Fantasy
{
    /// <summary>
    /// Interaction logic for FantasyEditCharacter.xaml
    /// </summary>
    public partial class FantasyEditCharacter : Window
    {
        string filePath;
        string jsonPath;
        string destinationFilePath;
        string oldLover;
        public string SelectedLover { get; set; }
        string fileName;

        CharacterProp character = new CharacterProp();

        private FantasyShowCharacters parentCharacterWindow;

        private CharacterProp charInfo = new CharacterProp();

        List<CharacterProp> characters = new List<CharacterProp>();

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            UseShellExecute = true,
        };

        public FantasyEditCharacter(CharacterProp characterToEdit, FantasyShowCharacters parentCharacterWindow)
        {
            InitializeComponent();

            filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RoleplayApp";
            jsonPath = System.IO.Path.Combine(filePath, "fantasyCharacters.json");
            this.parentCharacterWindow = parentCharacterWindow;
            this.character = characterToEdit;
            this.DataContext = character;

            WriteName.Text = characterToEdit.Name;
            WriteLevel.Text = characterToEdit.Level.ToString();
            WriteAge.Text = characterToEdit.Age.ToString();
            WriteCountry.Text = characterToEdit.Country;
            WriteMoney.Text = characterToEdit.Money;
            WriteDescription.Text = characterToEdit.Description;
            WriteHistory.Text = characterToEdit.CharacterHistory;

            if (characterToEdit.ImagePath != "RoleplayApp")
            {
                ImageUploadText.Foreground = new SolidColorBrush(Colors.Green);
                ImageUploadText.Text = "Billede er uploaded";
            }

            if (characterToEdit.Gender == Gender.Mand)
            {
                genderFemale.Background = new SolidColorBrush(Colors.Gray);
                genderMale.Background = new SolidColorBrush(Colors.SkyBlue);
            }
            else
            {
                genderMale.Background = new SolidColorBrush(Colors.Gray);
                genderFemale.Background = new SolidColorBrush(Colors.Pink);
            }
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            var jsonData = System.IO.File.ReadAllText(jsonPath);
            List<CharacterProp> existingCharacters = JsonConvert.DeserializeObject<List<CharacterProp>>(jsonData);

            CharacterProp existingCharacter = existingCharacters.FirstOrDefault(c => c.Name == character.Name);

            if (existingCharacter != null)
            {
                existingCharacter.Name = WriteName.Text;
                existingCharacter.Level = int.Parse(WriteLevel.Text);
                existingCharacter.Age = int.Parse(WriteAge.Text);
                existingCharacter.ImagePath = string.IsNullOrEmpty(fileName) ? existingCharacter.ImagePath : fileName;
                existingCharacter.Gender = character.Gender;
                existingCharacter.Type = character.Type;
                existingCharacter.Power = character.Power;
                existingCharacter.Country = WriteCountry.Text;
                existingCharacter.Money = WriteMoney.Text;
                existingCharacter.Description = WriteDescription.Text;
                existingCharacter.CharacterHistory = WriteHistory.Text;

                existingCharacter.Skills = new ObservableCollection<SkillViewModel>(character.Skills.Select(s => new SkillViewModel { Skill = s.Skill }));
                existingCharacter.Friends = new ObservableCollection<FriendViewModel>(character.Friends.Select(s => new FriendViewModel { Friend = s.Friend }));
                existingCharacter.Enemies = new ObservableCollection<EnemyViewModel>(character.Enemies.Select(s => new EnemyViewModel { Enemy = s.Enemy }));

                oldLover = existingCharacter.LoverId;

                if (string.IsNullOrEmpty(this.SelectedLover))
                {
                    this.SelectedLover = oldLover;
                }

                existingCharacter.LoverId = this.SelectedLover;

                if (!string.IsNullOrEmpty(oldLover) && oldLover != this.SelectedLover)
                {
                    CharacterProp oldLoverCharacter = existingCharacters.FirstOrDefault(c => c.Name == oldLover);
                    if (oldLoverCharacter != null)
                    {
                        if (oldLoverCharacter.LoverId == existingCharacter.Name)
                        {
                            oldLoverCharacter.LoverId = null;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(this.SelectedLover))
                {
                    CharacterProp newLoverCharacter = existingCharacters.FirstOrDefault(c => c.Name == this.SelectedLover);
                    if (newLoverCharacter != null)
                    {
                        if (newLoverCharacter.LoverId != null && newLoverCharacter.LoverId != existingCharacter.Name)
                        {
                            CharacterProp previousLoverOfNewLover = existingCharacters.FirstOrDefault(c => c.Name == newLoverCharacter.LoverId);
                            if (previousLoverOfNewLover != null)
                            {
                                previousLoverOfNewLover.LoverId = null;
                            }
                        }
                        newLoverCharacter.LoverId = existingCharacter.Name;
                    }
                }
            }



            CharacterProp existingLover = existingCharacters.FirstOrDefault(c => c.Name == this.SelectedLover);

            if (existingLover != null)
            {
                existingLover.LoverId = existingCharacter.Name;
            }


            string json = JsonConvert.SerializeObject(existingCharacters, Formatting.Indented);

            try
            {
                File.WriteAllText(jsonPath, json);
                parentCharacterWindow.UpdateCharacterList();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kunne ikke gmme fil: " + ex.Message);
            }
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files (*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                string destinationDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RoleplayApp\\Images";
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }
                string uniqueFileName = $"{System.IO.Path.GetFileNameWithoutExtension(selectedFilePath)}-{DateTime.Now.Ticks}{System.IO.Path.GetExtension(selectedFilePath)}";
                string destinationFilePath = System.IO.Path.Combine(destinationDirectory, uniqueFileName);

                try
                {
                    File.Copy(selectedFilePath, destinationFilePath, true);
                    this.fileName = uniqueFileName; // Opdater fileName til det unikke filnavn
                    character.ImagePath = this.fileName; // Sæt character.ImagePath til det unikke filnavn
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fejl: {ex.Message}. Kunne ikke kopiere billedet. Vælg et andet billede.");
                }

                // Konstruér den fulde sti dynamisk
                string fullImagePath = System.IO.Path.Combine(destinationDirectory, character.ImagePath);

                // Vis billedet
                try
                {
                    BitmapImage image = new BitmapImage(new Uri(fullImagePath, UriKind.Absolute));
                    ImageUploadText.Foreground = new SolidColorBrush(Colors.Green);
                    ImageUploadText.Text = "Billede er uploaded";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fejl: {ex.ToString()}. Kunne ikke læse billede, find et andet");
                }
            }
        }



        private void AddFriend(object sender, RoutedEventArgs e)
        {
            character.Friends.Add(new FriendViewModel { Friend = "" });
        }

        private void AddEnemy(object sender, RoutedEventArgs e)
        {
            character.Enemies.Add(new EnemyViewModel { Enemy = "" });
        }

        private void AddSkillButton(object sender, RoutedEventArgs e)
        {
            character.Skills.Add(new SkillViewModel { Skill = "" });
        }

        private void genderMale_Click(object sender, RoutedEventArgs e)
        {
            character.Gender = Gender.Mand;
            genderFemale.Background = new SolidColorBrush(Colors.Gray);
            genderMale.Background = new SolidColorBrush(Colors.SkyBlue);
        }

        private void genderFemale_Click(object sender, RoutedEventArgs e)
        {
            character.Gender = Gender.Kvinde;
            genderFemale.Background = new SolidColorBrush(Colors.Pink);
            genderMale.Background = new SolidColorBrush(Colors.Gray);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = sender as TextBox;
            int index = SkillsList.Items.IndexOf(box.DataContext);
            if (index != -1)
            {
                character.Skills[index].Skill = box.Text;
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            TextBox box = sender as TextBox;
            int index = EnemiesList.Items.IndexOf(box.DataContext);
            if (index != -1)
            {
                character.Enemies[index].Enemy = box.Text;
            }
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            TextBox box = sender as TextBox;
            int index = FriendsList.Items.IndexOf(box.DataContext);
            if (index != -1)
            {
                character.Friends[index].Friend = box.Text;
            }
        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedContent = (string)selectedItem.Content;
                Type selectedType = (Type)Enum.Parse(typeof(Type), selectedContent);
                character.Type = selectedType;
            }
        }

        private void AddLover_Click(object sender, RoutedEventArgs e)
        {
            FindLover findLoverWindow = new FindLover(this); 
            findLoverWindow.Show();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBox box = sender as TextBox;
            string tag = (string)box.Tag;

            if (tag == "Friends")
            {
                int index = FriendsList.Items.IndexOf(box.DataContext);
                if (index != -1)
                {
                    character.Friends.RemoveAt(index);
                }
            }
            else if (tag == "Enemies")
            {
                int index = EnemiesList.Items.IndexOf(box.DataContext);
                if (index != -1)
                {
                    character.Enemies.RemoveAt(index);
                }
            }
            else if (tag == "Skills")
            {
                int index = SkillsList.Items.IndexOf(box.DataContext);
                if (index != -1)
                {
                    character.Skills.RemoveAt(index);
                }
            }
        }

        private void deleteChar_Click(object sender, RoutedEventArgs e)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RoleplayApp";
            string jsonPath = System.IO.Path.Combine(filePath, "fantasyCharacters.json");
            string jsonData = System.IO.File.ReadAllText(jsonPath);

            List<CharacterProp> characterList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CharacterProp>>(jsonData);

            CharacterProp characterToRemove = characterList.FirstOrDefault(c => c.Name == this.character.Name);

            if (characterToRemove != null)
            {
                characterList.Remove(characterToRemove);

                string updatedJsonData = Newtonsoft.Json.JsonConvert.SerializeObject(characterList, Newtonsoft.Json.Formatting.Indented);

                System.IO.File.WriteAllText(jsonPath, updatedJsonData);

                parentCharacterWindow.UpdateCharacterList();

                this.Close();
            }

        }

        private void RemoveLover(object sender, RoutedEventArgs e)
        {
            var jsonData = System.IO.File.ReadAllText(jsonPath);
            List<CharacterProp> existingCharacters = JsonConvert.DeserializeObject<List<CharacterProp>>(jsonData);

            CharacterProp existingCharacter = existingCharacters.FirstOrDefault(c => c.Name == character.Name);

            if (existingCharacter != null)
            {
                CharacterProp oldLoverCharacter = existingCharacters.FirstOrDefault(c => c.Name == existingCharacter.LoverId);

                if (oldLoverCharacter != null)
                {
                    if (oldLoverCharacter.LoverId == existingCharacter.Name)
                    {
                        oldLoverCharacter.LoverId = null;
                    }
                }
                existingCharacter.LoverId = null;

                string json = JsonConvert.SerializeObject(existingCharacters, Formatting.Indented);
                File.WriteAllText(jsonPath, json);
                try
                {
                    parentCharacterWindow.UpdateCharacterList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kunne ikke gemme fil: " + ex.Message);
                }
            }
        }

        private void Power_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedContent = (string)selectedItem.Content;
                Power selectedPower = (Power)Enum.Parse(typeof(Power), selectedContent);
                character.Power = selectedPower;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double x = this.Left;
            double y = this.Top;

            FantasyAbilityWindow fantasyAddPower = new FantasyAbilityWindow();
            fantasyAddPower.Left = x;
            fantasyAddPower.Top = y;
            fantasyAddPower.Show();
        }
    }
}