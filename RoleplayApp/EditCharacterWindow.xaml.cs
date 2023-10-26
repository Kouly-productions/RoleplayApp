using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

namespace RoleplayApp
{
    /// <summary>
    /// Interaction logic for EditCharacterWindow.xaml
    /// </summary>
    public partial class EditCharacterWindow : Window
    {
        string filePath;
        string jsonPath;
        string destinationFilePath;
        string oldLover;
        public string SelectedLover { get; set; }

        CharacterProp character = new CharacterProp();

        private Character parentCharacterWindow;

        private CharacterProp charInfo = new CharacterProp();

        List<CharacterProp> characters = new List<CharacterProp>();

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            UseShellExecute = true,
        };

        public EditCharacterWindow(CharacterProp characterToEdit ,Character parentCharacterWindow)
        {
            InitializeComponent();

            filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RoleplayApp";
            jsonPath = System.IO.Path.Combine(filePath, "characters.json");
            this.parentCharacterWindow = parentCharacterWindow;
            this.character = characterToEdit;
            this.DataContext = character;

            WriteName.Text = characterToEdit.Name;
            WriteStrength.Text = characterToEdit.Strength.ToString();
            WriteIntellect.Text = characterToEdit.Intellect.ToString();
            WriteCharisma.Text = characterToEdit.Charisma.ToString();
            WriteLevel.Text = characterToEdit.Level.ToString();
            WriteAge.Text = characterToEdit.Age.ToString();
            WriteCountry.Text = characterToEdit.Country;
            WriteMoney.Text = characterToEdit.Money;
            WriteDescription.Text = characterToEdit.Description;
            WriteDexterity.Text = characterToEdit.Dexterity.ToString();
            WriteConstitution.Text = characterToEdit.Constitution.ToString();
            WriteWisdom.Text = characterToEdit.Wisdom.ToString();
            WriteArmor.Text = characterToEdit.Armor.ToString();
            WriteHealth.Text = characterToEdit.Health.ToString();
            WriteHaste.Text = characterToEdit.Haste.ToString();
            WriteAcrobatic.Text = characterToEdit.Actrobatic.ToString();
            WriteAnimalTaiming.Text = characterToEdit.AnimalTaiming.ToString();
            WriteArcana.Text = characterToEdit.Arcana.ToString();
            WriteAthletics.Text = characterToEdit.Athletics.ToString();
            WriteDeception.Text = characterToEdit.Deception.ToString();
            WriteHistory.Text = characterToEdit.History.ToString();
            WriteInsight.Text = characterToEdit.Insight.ToString();
            WriteIntimidation.Text = characterToEdit.Intimidation.ToString();
            WriteInvestigation.Text = characterToEdit.Investigation.ToString();
            WriteMedicine.Text = characterToEdit.Medicine.ToString();
            WriteNature.Text = characterToEdit.Nature.ToString();
            WritePerception.Text = characterToEdit.Perception.ToString();
            WritePerformance.Text = characterToEdit.Performance.ToString();
            WritePersuasion.Text = characterToEdit.Persuasion.ToString();
            WriteSleightOfHand.Text = characterToEdit.SleightOfHand.ToString();
            WriteStealth.Text = characterToEdit.Stealth.ToString();
            WriteSurvival.Text = characterToEdit.Survival.ToString();
            SavingStrength.Text = characterToEdit.SavingStrength.ToString();
            SavingDexterity.Text = characterToEdit.SavingDexterity.ToString();
            SavingConstitution.Text = characterToEdit.SavingConstitution.ToString();
            SavingInteligence.Text = characterToEdit.SavingIntellect.ToString();
            SavingWisdom.Text = characterToEdit.SavingWisdom.ToString();
            SavingCharisma.Text = characterToEdit.SavingCharisma.ToString();
        }



        private void Done_Click(object sender, RoutedEventArgs e)
        {
            var jsonData = System.IO.File.ReadAllText(jsonPath);
            List<CharacterProp> existingCharacters = JsonConvert.DeserializeObject<List<CharacterProp>>(jsonData);

            CharacterProp existingCharacter = existingCharacters.FirstOrDefault(c => c.Name == character.Name);

            if (existingCharacter != null)
            {
                existingCharacter.Name = WriteName.Text;
                existingCharacter.Strength = int.Parse(WriteStrength.Text);
                existingCharacter.Intellect = int.Parse(WriteIntellect.Text);
                existingCharacter.Charisma = int.Parse(WriteCharisma.Text);
                existingCharacter.Level = int.Parse(WriteLevel.Text);
                existingCharacter.Age = int.Parse(WriteAge.Text);
                existingCharacter.ImagePath = string.IsNullOrEmpty(destinationFilePath) ? existingCharacter.ImagePath : destinationFilePath;
                existingCharacter.Gender = character.Gender;
                existingCharacter.Type = character.Type;
                existingCharacter.Country = WriteCountry.Text;
                existingCharacter.Money = WriteMoney.Text;
                existingCharacter.Description = WriteDescription.Text;
                existingCharacter.Dexterity = int.Parse(WriteDexterity.Text);
                existingCharacter.Constitution = int.Parse(WriteConstitution.Text);
                existingCharacter.Wisdom = int.Parse(WriteWisdom.Text);

                existingCharacter.Armor = int.Parse(WriteArmor.Text);
                existingCharacter.Health = int.Parse(WriteHealth.Text);
                existingCharacter.Haste = int.Parse(WriteHaste.Text);

                existingCharacter.Actrobatic = int.Parse(WriteAcrobatic.Text);
                existingCharacter.AnimalTaiming = int.Parse(WriteAnimalTaiming.Text);
                existingCharacter.Arcana = int.Parse(WriteArcana.Text);
                existingCharacter.Athletics = int.Parse(WriteAthletics.Text);
                existingCharacter.Deception = int.Parse(WriteDeception.Text);
                existingCharacter.History = int.Parse(WriteHistory.Text);
                existingCharacter.Insight = int.Parse(WriteInsight.Text);
                existingCharacter.Intimidation = int.Parse(WriteIntimidation.Text);
                existingCharacter.Investigation = int.Parse(WriteInvestigation.Text);
                existingCharacter.Medicine = int.Parse(WriteMedicine.Text);
                existingCharacter.Nature = int.Parse(WriteNature.Text);
                existingCharacter.Perception = int.Parse(WritePerception.Text);
                existingCharacter.Performance = int.Parse(WritePerformance.Text);
                existingCharacter.Persuasion = int.Parse(WritePersuasion.Text);
                existingCharacter.SleightOfHand = int.Parse(WriteSleightOfHand.Text);
                existingCharacter.Stealth = int.Parse(WriteStealth.Text);
                existingCharacter.Survival = int.Parse(WriteSurvival.Text);

                existingCharacter.SavingStrength = int.Parse(SavingStrength.Text);
                existingCharacter.SavingDexterity = int.Parse(SavingDexterity.Text);
                existingCharacter.SavingConstitution = int.Parse(SavingConstitution.Text);
                existingCharacter.SavingIntellect = int.Parse(SavingInteligence.Text);
                existingCharacter.SavingWisdom = int.Parse(SavingWisdom.Text);
                existingCharacter.SavingCharisma = int.Parse(SavingCharisma.Text);

                existingCharacter.Skills = new ObservableCollection<SkillViewModel>(character.Skills.Select(s => new SkillViewModel { Skill = s.Skill }));
                existingCharacter.Friends = new ObservableCollection<FriendViewModel>(character.Friends.Select(s => new FriendViewModel { Friend = s.Friend }));
                existingCharacter.Enemies = new ObservableCollection<EnemyViewModel>(character.Enemies.Select(s => new EnemyViewModel { Enemy = s.Enemy }));

                existingCharacter.ModifiersCombined = CalculateModifier(existingCharacter.Strength) +
                    CalculateModifier(existingCharacter.Dexterity) +
                    CalculateModifier(existingCharacter.Constitution) +
                    CalculateModifier(existingCharacter.Intellect) +
                    CalculateModifier(existingCharacter.Wisdom) +
                    CalculateModifier(existingCharacter.Charisma);


                oldLover = existingCharacter.LoverId;
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
        public static int CalculateModifier(int abilityScore)
        {
            return (int)Math.Floor((abilityScore - 10) / 2.0);
        }
        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png\")";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                string destinationDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RoleplayApp\\Images";
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }
                destinationFilePath = System.IO.Path.Combine(destinationDirectory, System.IO.Path.GetFileName(selectedFilePath));
                File.Copy(selectedFilePath, destinationFilePath, true);

                character.ImagePath = destinationFilePath;
                BitmapImage image = new BitmapImage(new Uri(destinationFilePath, UriKind.Absolute));
                ImageUploadText.Foreground = new SolidColorBrush(Colors.Green);
                ImageUploadText.Text = "Billede er uploaded";
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
        }

        private void genderFemale_Click(object sender, RoutedEventArgs e)
        {
            character.Gender = Gender.Kvinde;
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
            string jsonPath = System.IO.Path.Combine(filePath, "characters.json");
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

                string json = JsonConvert.SerializeObject(existingCharacter, Formatting.Indented);
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
    }
}