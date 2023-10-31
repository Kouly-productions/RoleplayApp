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
using RoleplayApp.Fantasy;

namespace RoleplayApp
{
    /// <summary>
    /// Interaction logic for FantasyShowCharacterStats.xaml
    /// </summary>
    public partial class FantasyShowCharacterStats : Window
    {
        private CharacterProp character;
        private FantasyShowCharacters parentCharacterWindow;
        private string jsonPath;

        public FantasyShowCharacterStats(CharacterProp character, FantasyShowCharacters parentWindow)
        {
            InitializeComponent();
            this.character = character;
            this.parentCharacterWindow = parentWindow;
            this.DataContext = character;
            this.jsonPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RoleplayApp", "fantasyCharacters.json");

            showCharacterInfo(character);
        }

        private void done_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void showCharacterInfo(CharacterProp character)
        {
            charName.Text = character.Name;
            selectedName.Text = character.Name.ToString();
            charLevel.Text = "Level:" + character.Level.ToString();
            charAge.Text = character.Age.ToString();
            charGender.Text = character.Gender.ToString();
            charType.Text = character.Type.ToString();
            charCountry.Text = character.Country;
            charMoney.Text = character.Money;
            charDescription.Text = character.Description;
            charHistory.Text = character.CharacterHistory;

            ShowLoverInfo(character.LoverId);

            if (!string.IsNullOrEmpty(character.ImagePath))
            {
                try
                {
                    // Konstruér den fulde sti dynamisk
                    string fullImagePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RoleplayApp\\Images", character.ImagePath);

                    // Forsøger at oprette en ny URI. Hvis det fejler, fanges det i catch blokken.
                    Uri imageUri = new Uri(fullImagePath, UriKind.Absolute);
                    BitmapImage imageBitMap = new BitmapImage(imageUri);
                    CharImage.Source = imageBitMap;
                }
                catch (UriFormatException)
                {
                    // Log fejlen, vis en advarsel, eller gør noget andet for at håndtere fejlen.
                    MessageBox.Show("Ugyldig billede sti.");
                }
            }

        }

        public void ShowLoverInfo(string loverId)
        {
            if (string.IsNullOrEmpty(loverId))
            {
                LoverInfoPanel.Visibility = Visibility.Collapsed;
                return;
            }

            string json = File.ReadAllText(jsonPath);
            List<CharacterProp> existringCharacters = JsonConvert.DeserializeObject<List<CharacterProp>>(json);

            CharacterProp loverCharacter = existringCharacters.FirstOrDefault(c => c.Name == loverId);

            if (loverCharacter != null)
            {
                string imagePath = loverCharacter.ImagePath;
                UpdateLoverUI(loverCharacter, imagePath);
                LoverInfoPanel.Visibility = Visibility.Visible;
            }
            else
            {
                LoverInfoPanel.Visibility = Visibility.Collapsed;
            }
        }

        public void UpdateLoverUI(CharacterProp lover, string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                string fullImagePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RoleplayApp\\Images", imagePath);
                if (System.IO.File.Exists(fullImagePath))
                {
                    BitmapImage image = new BitmapImage(new Uri(fullImagePath, UriKind.Absolute));
                    LoverImage.Source = image;
                }
            }
            loverName.Text = lover.Name;
            loverAge.Text = lover.Age.ToString();
            loverGender.Text = lover.Gender.ToString();
            loverRace.Text = lover.Type.ToString();
            loverMoney.Text = lover.Money.ToString();
            loverCountry.Text = lover.Country.ToString();
        }

        public void InitializeUI()
        {
            MainInfoContainer.Children.Add(CharacterMainInfoPanel);

            LoverInfoPanel = new StackPanel();

            MainInfoContainer.Children.Add(LoverInfoPanel);
            LoverInfoPanel.Visibility = Visibility.Collapsed;
        }

        public void ShowCharacterImage(string imageName)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                string fullImagePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RoleplayApp\\Images", imageName);

                if (System.IO.File.Exists(fullImagePath))
                {
                    BitmapImage image = new BitmapImage(new Uri(fullImagePath, UriKind.Absolute));
                    CharImage.Source = image;
                }
            }
        }

        private void LoverImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();

            string json = File.ReadAllText(jsonPath);
            List<CharacterProp> existingCharacters = JsonConvert.DeserializeObject<List<CharacterProp>>(json);

            CharacterProp loverCharacter = existingCharacters.FirstOrDefault(c => c.Name == this.character.LoverId);

            if (loverCharacter != null)
            {
                ShowCharacterInfo(loverCharacter);
            }
            else
            {
                return;
            }
        }

        private void ShowCharacterInfo(CharacterProp character)
        {

            FantasyShowCharacterStats fantasyShowCharacterStats = new FantasyShowCharacterStats(character, this.parentCharacterWindow);
            fantasyShowCharacterStats.ShowDialog();
        }
    }
}
