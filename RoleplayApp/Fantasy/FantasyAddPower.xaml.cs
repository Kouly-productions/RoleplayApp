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
    /// <summary>
    /// Interaction logic for FantasyAddPower.xaml
    /// </summary>
    public partial class FantasyAddPower : Window
    {
        string filePath;
        string jsonPath;
        public string SelectedAbility { get; set; }
        string fileName = "RoleplayApp";

        CharacterProp character = new CharacterProp();
        Forces forces = new Forces();

        public FantasyAddPower()
        {
            InitializeComponent();

            filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RoleplayApp";
            jsonPath = System.IO.Path.Combine(filePath, "fantasyAbility.json");

            this.DataContext = character;
        }

        private bool AreAllFieldsFilled()
        {
            TextBox[] fields = { };

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

            Forces newAbility = new Forces();
            newAbility.Name = WriteName.Text;
            newAbility.Description = WriteDescription.Text;
            newAbility.AbilityLevelRequirement = int.Parse(WriteAbilityLevelRequirement.Text);
            newAbility.imagePath = fileName;
            newAbility.abilityType = forces.abilityType;
            newAbility.IsAOE = forces.IsAOE;

            List<CharacterProp> existingAbilities = new List<CharacterProp>();

            string json = JsonConvert.SerializeObject(existingAbilities, Formatting.Indented);

            try
            {
                File.WriteAllText(jsonPath, json);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kunne ikke gmme fil: " + ex.Message);
            }
        }
    }
}
