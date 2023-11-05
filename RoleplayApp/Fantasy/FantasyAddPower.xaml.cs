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
    public partial class FantasyAddPower : Window
    {
        private readonly string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RoleplayApp");
        private readonly string jsonPath;
        public string SelectedAbility { get; set; }
        private string fileName = "RoleplayApp";

        private readonly Window parentWindow;

        public FantasyAddPower(Window parentindow)
        {
            InitializeComponent();
            jsonPath = System.IO.Path.Combine(filePath, "fantasyAbility.json");
            this.parentWindow = parentWindow;
        }

        private bool AreAllFieldsFilled()
        {
            TextBox[] fields = { WriteName, WriteDescription, WriteAbilityLevelRequirement };
            return fields.All(field => !string.IsNullOrEmpty(field.Name));
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!AreAllFieldsFilled())
            {
                MessageBox.Show("Udfyld alle felter, før du fortsætter");
                return;
            }

            if (!int.TryParse(WriteAbilityLevelRequirement.Text, out int abilityLevel))
            {
                MessageBox.Show("Level må ikke indeholder bogstaver kun tal");
                return;
            }
            AbilityType selectedType = (AbilityType)Enum.Parse(typeof(AbilityType), (ChooseType.SelectedItem as ComboBoxItem)?.Content.ToString());
            Forces newAbility = new Forces
            {
                Name = WriteName.Text,
                Description = WriteDescription.Text,
                AbilityLevelRequirement = abilityLevel,
                imagePath = fileName,
                abilityType = selectedType,
                IsAOE = (bool)IsAOEBox.IsChecked
            };

            List<Forces> existingAbilities;
            if (File.Exists(jsonPath))
            {
                var jsonData = File.ReadAllText(jsonPath);
                existingAbilities = JsonConvert.DeserializeObject<List<Forces>>(jsonData) ?? new List<Forces>();
            }
            else
            {
                existingAbilities = new List<Forces>();
            }

            existingAbilities.Add(newAbility);

            string json = JsonConvert.SerializeObject(existingAbilities, Formatting.Indented);
            try
            {
                File.WriteAllText(jsonPath, json);
                //parentAbilityWindow.UpdateCharacterList();
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
                try
                {
                    string selectedFilePath = openFileDialog.FileName;
                    string destinationDirectory = System.IO.Path.Combine(filePath, "Images");
                    if (!Directory.Exists(destinationDirectory))
                    {
                        Directory.CreateDirectory(destinationDirectory);
                    }
                    string uniqueFileName = $"{System.IO.Path.GetFileNameWithoutExtension(selectedFilePath)}-{DateTime.Now.Ticks}{System.IO.Path.GetExtension(selectedFilePath)}";
                    string destinationFilePath = System.IO.Path.Combine(destinationDirectory, uniqueFileName);
                    File.Copy(selectedFilePath, destinationFilePath, true);


                    this.fileName = uniqueFileName;


                    // Konstruér den fulde sti dynamisk (ingen ændring her, da det allerede var korrekt)
                    string fullImagePath = System.IO.Path.Combine(destinationDirectory, fileName);
                    BitmapImage image = new BitmapImage(new Uri(fullImagePath, UriKind.Absolute));
                }
                catch
                {
                    MessageBox.Show("Fejl. Kunne ikke læse billede, find et andet");
                }
            }
        }
    }
}
