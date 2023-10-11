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
    /// Interaction logic for CharacterCreationWindow.xaml
    /// </summary>
    public partial class CharacterCreationWindow : Window
    {
        string filePath;
        string jsonPath;

        List<CharacterProp> characters = new List<CharacterProp>();


        public CharacterCreationWindow()
        {
            InitializeComponent();

            filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RoleplayApp";
            jsonPath = System.IO.Path.Combine(filePath, "characters.json");
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            CharacterProp newCharacter = new CharacterProp();
            newCharacter.Name = WriteName.Text;
            newCharacter.Description = WriteDesc.Text;
            newCharacter.Age = int.Parse(WriteAge.Text);

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
                Character.GetCharacters();
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
    }
}
