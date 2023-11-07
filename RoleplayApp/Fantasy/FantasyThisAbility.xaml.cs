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
    /// Interaction logic for FantasyThisAbility.xaml
    /// </summary>
    public partial class FantasyThisAbility : Window
    {
        private Forces forces;
        private FantasyAbilityWindow parentFantasyWindow;
        private Window parentWindow;
        private string jsonPath;

        public FantasyThisAbility(Forces forces, FantasyAbilityWindow parentWindow)
        {
            InitializeComponent();

            this.forces = forces;
            this.parentFantasyWindow = parentWindow;
            this.DataContext = forces;
            this.jsonPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RoleplayApp", "fantasyAbility.json");

            showAbilityInfo(forces);
        }

        public FantasyThisAbility(Forces forces, Window parentWindow)
        {
            InitializeComponent();

            this.forces = forces;
            this.parentWindow = parentWindow;  // Gemmer referencen til forældrevinduet
            this.DataContext = forces;
            this.jsonPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RoleplayApp", "fantasyAbility.json");

            showAbilityInfo(forces);
        }

        public void showAbilityInfo(Forces forces)
        {
            AbilityName.Text = forces.Name;
            MinLevel.Text = forces.AbilityLevelRequirement.ToString();
            AbilityType.Text = forces.abilityType.ToString();
            AbilityDescription.Text = forces.Description;

            if (forces.IsAOE)
            {
                IsAOE.Text = "Er AOE";
            }
            else
            {
                IsAOE.Text = "Er ikke AOE";
            }

            if (!string.IsNullOrEmpty(forces.imagePath))
            {
                try
                {
                    // Konstruér den fulde sti dynamisk
                    string fullImagePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RoleplayApp\\Images", forces.imagePath);

                    if (File.Exists(fullImagePath))
                    {
                        Uri imageUri = new Uri(fullImagePath, UriKind.Absolute);
                        BitmapImage imageBitMap = new BitmapImage(imageUri);
                        AbilityImage.Source = imageBitMap;
                    }
                    else
                    {
                        MessageBox.Show($"Karakteren har ikke noget billede");
                    }
                }
                catch (UriFormatException)
                {
                    // Log fejlen, vis en advarsel, eller gør noget andet for at håndtere fejlen.
                    MessageBox.Show("Ugyldig billede sti.");
                }
            }

        }
    }
}
