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

        private Character parentCharacterWindow;

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
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            CharacterProp newCharacter = new CharacterProp();
            newCharacter.Name = WriteName.Text;
            newCharacter.Description = WriteDesc.Text;
            newCharacter.Age = int.Parse(WriteAge.Text);
            newCharacter.ImagePath = destinationFilePath;

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

                CharacterProp character = new CharacterProp();
                character.ImagePath = destinationFilePath;
                BitmapImage image = new BitmapImage(new Uri(destinationFilePath, UriKind.Absolute));
            }
        }
    }
}
