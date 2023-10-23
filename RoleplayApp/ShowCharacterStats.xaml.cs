using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        private string jsonPath;

        public ShowCharacterStats(CharacterProp character, Character parentWindow)
        {
            InitializeComponent();
            this.character = character;
            this.parentCharacterWindow = parentWindow;
            this.DataContext = character;
            this.jsonPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RoleplayApp", "characters.json");

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

            charArmor.Text = character.Armor.ToString();
            charHealth.Text = character.Health.ToString();
            charHaste.Text = character.Haste.ToString();

            charAcrobatic.Text = character.Actrobatic.ToString();
            charAnimalTaiming.Text = character.AnimalTaiming.ToString();
            charArcana.Text = character.Arcana.ToString();
            charAthletics.Text = character.Athletics.ToString();
            charDeception.Text = character.Deception.ToString();
            charHistory.Text = character.History.ToString();
            charInsight.Text = character.Insight.ToString();
            charIntimidation.Text = character.Intimidation.ToString();
            charInvestigation.Text = character.Investigation.ToString();
            charMedicine.Text = character.Medicine.ToString();
            charNature.Text = character.Nature.ToString();
            charPerception.Text = character.Perception.ToString();
            charPerformance.Text = character.Performance.ToString();
            charPersuasion.Text = character.Persuasion.ToString();
            charSleightOfHand.Text = character.SleightOfHand.ToString();
            charStealth.Text = character.Stealth.ToString();
            charSurvival.Text = character.Survival.ToString();

            charSavingStrength.Text = character.SavingStrength.ToString();
            charSavingDexterity.Text = character.SavingDexterity.ToString();
            charSavingConstitution.Text = character.SavingConstitution.ToString();
            charSavingIntellect.Text = character.SavingIntellect.ToString();
            charSavingWisdom.Text = character.SavingWisdom.ToString();
            charSavingCharisma.Text = character.SavingCharisma.ToString();

            StrengthMod.Text = CalculateModifier(character.Strength).ToString();
            DexterityMod.Text = CalculateModifier(character.Dexterity).ToString();
            ConstitutionMod.Text = CalculateModifier(character.Constitution).ToString();
            IntelectMod.Text = CalculateModifier(character.Intellect).ToString();
            WisdomMod.Text = CalculateModifier(character.Wisdom).ToString();
            CharismaMod.Text = CalculateModifier(character.Charisma).ToString();

            ShowLoverInfo(character.LoverId);

            if (!string.IsNullOrEmpty(character.ImagePath))
            {
                Uri imageUri = new Uri(character.ImagePath, UriKind.Absolute);
                BitmapImage imageBitMap = new BitmapImage(imageUri);

                CharImage.Source = imageBitMap;
            }
        }

        public void ShowLoverInfo(string loverId)
        {
            if (string.IsNullOrEmpty(loverId))
            {
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
                LoverInfoPanel.Visibility= Visibility.Collapsed;
            }
        }

        public void UpdateLoverUI(CharacterProp lover, string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
            {
                BitmapImage image = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
                LoverImage.Source = image;
            }
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

        public void ShowCharacterImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
            {
                BitmapImage image = new BitmapImage(new Uri (imagePath, UriKind.Absolute));
                CharImage.Source = image;
            }
        }

        public void showLoverImage()
        {

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
