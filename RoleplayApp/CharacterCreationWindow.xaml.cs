﻿using Newtonsoft.Json;
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
using System.Diagnostics;
using static RoleplayApp.CharacterProp;
using System.Collections.ObjectModel;

namespace RoleplayApp
{
    /// <summary>
    /// Interaction logic for CharacterCreationWindow.xaml
    /// </summary>
    public partial class CharacterCreationWindow : Window
    {
        string filePath;
        string jsonPath;
        string destinationFilePath;
        public string SelectedLover {  get; set; }
        string fileName = "RoleplayApp";

        CharacterProp character = new CharacterProp();

        private Character parentCharacterWindow;

        private CharacterProp charInfo = new CharacterProp();

        List<CharacterProp> characters = new List<CharacterProp>();

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            UseShellExecute = true,
        };


        public CharacterCreationWindow(Character parentCharacterWindow)
        {
            InitializeComponent();


            filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RoleplayApp";
            jsonPath = System.IO.Path.Combine(filePath, "Characters");
            this.parentCharacterWindow = parentCharacterWindow;

            this.DataContext = character;
        }

        private bool AreAllFieldsFilled()
        {
            TextBox[] fields = { WriteName, WriteLevel, WriteStrength, WriteDexterity, WriteConstitution,
            WriteIntellect, WriteWisdom, WriteCharisma, WriteAcrobatic, WriteAnimalTaiming, WriteArcana,
            WriteAthletics, WriteDeception, WriteHistory, WriteInsight, WriteIntimidation, WriteInvestigation,
            WriteMedicine, WriteNature, WritePerception, WriteSleightOfHand, WriteStealth, WriteSurvival, WriteAge,
            WriteArmor, WriteHealth, WriteHaste};

            foreach (TextBox field in fields) 
            {
                if (string.IsNullOrEmpty(field.Text))
                {
                    return false;
                }
            }
            return true;
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!AreAllFieldsFilled())
            {
                MessageBox.Show("Udfyld alle felter, før du fortsætter");
                return;
            }

            CharacterProp newCharacter = new CharacterProp();
            newCharacter.Name = WriteName.Text;
            newCharacter.Strength = int.Parse(WriteStrength.Text);
            newCharacter.Intellect = int.Parse(WriteIntellect.Text);
            newCharacter.Charisma = int.Parse(WriteCharisma.Text);
            newCharacter.Level = int.Parse(WriteLevel.Text);
            newCharacter.Age = int.Parse(WriteAge.Text);
            newCharacter.ImagePath = fileName;
            newCharacter.Gender = character.Gender; 
            newCharacter.Type = character.Type;
            newCharacter.Country = WriteCountry.Text;
            newCharacter.Money = WriteMoney.Text;
            newCharacter.Description = WriteDescription.Text;
            newCharacter.Dexterity = int.Parse(WriteDexterity.Text);
            newCharacter.Constitution = int.Parse(WriteConstitution.Text);
            newCharacter.Wisdom = int.Parse(WriteWisdom.Text);

            newCharacter.Armor = int.Parse(WriteArmor.Text);
            newCharacter.Health = int.Parse(WriteHealth.Text);
            newCharacter.Haste = int.Parse(WriteHaste.Text);

            newCharacter.Actrobatic = int.Parse(WriteAcrobatic.Text);
            newCharacter.AnimalTaiming = int.Parse(WriteAnimalTaiming.Text);
            newCharacter.Arcana = int.Parse(WriteArcana.Text);
            newCharacter.Athletics = int.Parse(WriteAthletics.Text);
            newCharacter.Deception = int.Parse(WriteDeception.Text);
            newCharacter.History = int.Parse(WriteHistory.Text);
            newCharacter.Insight = int.Parse(WriteInsight.Text);
            newCharacter.Intimidation = int.Parse(WriteIntimidation.Text);
            newCharacter.Investigation = int.Parse(WriteInvestigation.Text);
            newCharacter.Medicine = int.Parse(WriteMedicine.Text);
            newCharacter.Nature = int.Parse(WriteNature.Text);
            newCharacter.Perception = int.Parse(WritePerception.Text);
            newCharacter.Performance = int.Parse(WritePerformance.Text);
            newCharacter.Persuasion = int.Parse(WritePersuasion.Text);
            newCharacter.SleightOfHand = int.Parse(WriteSleightOfHand.Text);
            newCharacter.Stealth = int.Parse(WriteStealth.Text);
            newCharacter.Survival = int.Parse(WriteSurvival.Text);

            /*
            newCharacter.SavingStrength = int.Parse(SavingStrength.Text);
            newCharacter.SavingDexterity = int.Parse(SavingDexterity.Text);
            newCharacter.SavingConstitution = int.Parse(SavingConstitution.Text);
            newCharacter.SavingIntellect = int.Parse(SavingInteligence.Text);
            newCharacter.SavingWisdom = int.Parse(SavingWisdom.Text);
            newCharacter.SavingCharisma = int.Parse(SavingCharisma.Text);
            */

            newCharacter.Skills = new ObservableCollection<SkillViewModel>(character.Skills.Select(s => new SkillViewModel { Skill = s.Skill }));
            newCharacter.Friends = new ObservableCollection<FriendViewModel>(character.Friends.Select(s => new FriendViewModel { Friend = s.Friend }));
            newCharacter.Enemies = new ObservableCollection<EnemyViewModel>(character.Enemies.Select(s => new EnemyViewModel { Enemy = s.Enemy }));

            newCharacter.ModifiersCombined = CalculateModifier(newCharacter.Strength) + 
                CalculateModifier(newCharacter.Dexterity) + 
                CalculateModifier(newCharacter.Constitution) + 
                CalculateModifier(newCharacter.Intellect) +
                CalculateModifier(newCharacter.Wisdom) +
                CalculateModifier(newCharacter.Charisma);

            newCharacter.StatsCombined = newCharacter.Health + newCharacter.Haste + newCharacter.Armor;

            newCharacter.LoverId = this.SelectedLover;


            //Read existing json data
            var jsonData = System.IO.File.ReadAllText(jsonPath);
            //Create list
            List<CharacterProp> existingCharacters = new List<CharacterProp>();
            //Put into list
            existingCharacters = JsonConvert.DeserializeObject<List<CharacterProp>>(jsonData) ?? new List<CharacterProp>();

            CharacterProp existingLover = existingCharacters.FirstOrDefault(c => c.Name == this.SelectedLover);

            if(existingLover != null)
            {
                UpdateCharacterLoverId(existingLover.LoverId, null);

                UpdateCharacterLoverId(existingLover.Name, newCharacter.Name);
            }

            existingCharacters.Add(newCharacter);


            string characterFileName = $"{newCharacter.Name}.json";
            string characterFilePath = System.IO.Path.Combine(jsonPath, characterFileName);

            string json = JsonConvert.SerializeObject(newCharacter, Formatting.Indented);

            try
            {
                File.WriteAllText(characterFilePath, json);
                parentCharacterWindow.UpdateCharacterList();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kunne ikke gmme fil: " + ex.Message);
            }
        }

        private void UpdateCharacterLoverId(string characterName, string newLoverId)
        {
            string characterFileName = $"{characterName}.json";
            string characterFilePath = System.IO.Path.Combine(jsonPath, characterFileName);

            if (File.Exists(characterFilePath))
            {
                var existingCharacterJson = File.ReadAllText(characterFilePath);
                CharacterProp existingCharacter = JsonConvert.DeserializeObject<CharacterProp>(existingCharacterJson);

                existingCharacter.LoverId = newLoverId;

                string updatedJson = JsonConvert.SerializeObject(existingCharacter, Formatting.Indented);
                File.WriteAllText(characterFilePath , updatedJson);
            }
            else
            {
                MessageBox.Show($"Karakteren {characterName} findes ikke.");
            }
        }

        public static int CalculateModifier(int abilityScore)
        {
            return (int)Math.Floor((abilityScore - 10) / 2.0);
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                string uniqueFileName = $"{System.IO.Path.GetFileNameWithoutExtension(selectedFilePath)}-{DateTime.Now.Ticks}{System.IO.Path.GetExtension(selectedFilePath)}";
                string destinationFilePath = System.IO.Path.Combine(destinationDirectory, uniqueFileName);
                File.Copy(selectedFilePath, destinationFilePath, true);


                // Inde i AddImage_Click
                this.fileName = System.IO.Path.GetFileName(selectedFilePath);


                // Gem kun filnavnet i JSON (ingen ændring her)
                character.ImagePath = fileName;

                // Konstruér den fulde sti dynamisk (ingen ændring her, da det allerede var korrekt)
                string fullImagePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RoleplayApp\\Images", character.ImagePath);

                // Vis billedet (ingen ændring nødvendig her)
                BitmapImage image = new BitmapImage(new Uri(fullImagePath, UriKind.Absolute));
                ImageUploadText.Foreground = new SolidColorBrush(Colors.Green);
                ImageUploadText.Text = "Billede er uploaded";
            }
        }


        private void genderMale_Click(object sender, RoutedEventArgs e)
        {
            character.Gender = Gender.Mand;
        }

        private void genderFemale_Click(object sender, RoutedEventArgs e)
        {
            character.Gender = Gender.Kvinde;
        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if(comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedContent = (string)selectedItem.Content;
                Type selectedType = (Type)Enum.Parse(typeof(Type), selectedContent);
                character.Type = selectedType;
            }
        }

        private void AddSkillButton(object sender, RoutedEventArgs e)
        {
            character.Skills.Add(new SkillViewModel { Skill = "" });
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

        private void AddFriend(object sender, RoutedEventArgs e)
        {
            character.Friends.Add(new FriendViewModel { Friend = "" });
        }

        private void AddEnemy(object sender, RoutedEventArgs e)
        {
            character.Enemies.Add(new EnemyViewModel { Enemy = "" });
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

        private void AddLover_Click(object sender, RoutedEventArgs e)
        {
            FindLover findLoverWindow = new FindLover(this);
            findLoverWindow.Show();
            LoverAddedText.Foreground = new SolidColorBrush(Colors.Green);
            LoverAddedText.Text = "Elsker er valgt";
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

    }
}
