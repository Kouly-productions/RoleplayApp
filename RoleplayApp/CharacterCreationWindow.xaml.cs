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
            jsonPath = System.IO.Path.Combine(filePath, "characters.json");
            this.parentCharacterWindow = parentCharacterWindow;

            this.DataContext = character;
        }

        private bool AreAllFieldsFilled()
        {
            TextBox[] fields = { WriteName, WriteStrength, WriteIntellect, WriteCharisma, WriteLevel, WriteAge, WriteCountry, WriteWeapon, WriteMoney };

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
            newCharacter.Health = int.Parse(WriteHealth.Text);
            newCharacter.Strength = int.Parse(WriteStrength.Text);
            newCharacter.Intellect = int.Parse(WriteIntellect.Text);
            newCharacter.Charisma = int.Parse(WriteCharisma.Text);
            newCharacter.Level = int.Parse(WriteLevel.Text);
            newCharacter.Age = int.Parse(WriteAge.Text);
            newCharacter.ImagePath = destinationFilePath;
            newCharacter.Gender = character.Gender;
            newCharacter.Type = character.Type;
            newCharacter.Country = WriteCountry.Text;
            newCharacter.Money = WriteMoney.Text;
            newCharacter.Description = WriteDescription.Text;
            newCharacter.Skills = new ObservableCollection<SkillViewModel>(character.Skills.Select(s => new SkillViewModel { Skill = s.Skill }));
            newCharacter.Friends = new ObservableCollection<RelationViewModel>(character.Friends.Select(s => new RelationViewModel { Friend = s.Friend }));
            newCharacter.Enemies = new ObservableCollection<RelationViewModel>(character.Enemies.Select(s => new RelationViewModel { Enemy = s.Enemy }));

            //Read existing json data
            var jsonData = System.IO.File.ReadAllText(jsonPath);
            //Create list
            List<CharacterProp> existingCharacters = new List<CharacterProp>();
            //Put into list
            existingCharacters = JsonConvert.DeserializeObject<List<CharacterProp>>(jsonData);

            existingCharacters.Add(newCharacter);

            string json = JsonConvert.SerializeObject(existingCharacters, Formatting.Indented);

            try
            {
                File.WriteAllText(jsonPath, json);
                MessageBox.Show("Succes!");
                parentCharacterWindow.UpdateCharacterList();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kunne ikke gmme fil: " + ex.Message);
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png\")";
            if(openFileDialog.ShowDialog() == true)
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
            character.Friends.Add(new RelationViewModel { Friend = "" });
        }

        private void AddEnemy(object sender, RoutedEventArgs e)
        {
            character.Enemies.Add(new RelationViewModel { Enemy = "" });
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
    }
}
