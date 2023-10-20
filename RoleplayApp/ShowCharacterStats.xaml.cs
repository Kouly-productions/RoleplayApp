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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace RoleplayApp
{
    /// <summary>
    /// Interaction logic for ShowCharacterStats.xaml
    /// </summary>
    public partial class ShowCharacterStats : Window
    {
        private CharacterProp character;
        private Character parentCharacterWindow;

        public ShowCharacterStats(CharacterProp character, Character parentWindow)
        {
            InitializeComponent();
            this.character = character;
            this.parentCharacterWindow = parentWindow;
            this.DataContext = character;
            showCharacterInfo(character);
        }

        private void done_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void showCharacterInfo(CharacterProp character)
        {
            charName.Text = character.Name;
            charLevel.Text = "Level:" + character.Level.ToString();
            charAge.Text = character.Age.ToString();
            charGender.Text = character.Gender.ToString();
            charType.Text = character.Type.ToString();
            charHealth.Text = character.Health.ToString();
            charStrength.Text = character.Strength.ToString();
            charDexterity.Text = character.Dexterity.ToString();
            charConstitution.Text = character.Constitution.ToString();
            charIntelect.Text = character.Intellect.ToString();
            charWisdom.Text = character.Wisdom.ToString();
            charCharisma.Text = character.Charisma.ToString();
            charArmor.Text = character.Armor.ToString();
            charHaste.Text = character.Haste.ToString();
            charCountry.Text = character.Country;
            charMoney.Text = character.Money;
            charDescription.Text = character.Description;
            
            StrengthMod.Text = CalculateModifier(character.Strength).ToString();
            DexterityMod.Text = CalculateModifier(character.Dexterity).ToString();
            ConstitutionMod.Text = CalculateModifier(character.Constitution).ToString();
            IntelectMod.Text = CalculateModifier(character.Intellect).ToString();
            WisdomMod.Text = CalculateModifier(character.Wisdom).ToString();
            CharismaMod.Text = CalculateModifier(character.Charisma).ToString();

            if (!string.IsNullOrEmpty(character.ImagePath))
            {
                Uri imageUri = new Uri(character.ImagePath, UriKind.Absolute);
                BitmapImage imageBitMap = new BitmapImage(imageUri);

                CharImage.Source = imageBitMap;
            }
        }

        public void ShowCharacterImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
            {
                BitmapImage image = new BitmapImage(new Uri (imagePath, UriKind.Absolute));
                CharImage.Source = image;
            }
        }

        public static int CalculateModifier(int abilityScore)
        {
            return (int)Math.Floor((abilityScore - 10) / 2.0);
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
    }
}
