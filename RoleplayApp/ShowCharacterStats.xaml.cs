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

namespace RoleplayApp
{
    /// <summary>
    /// Interaction logic for ShowCharacterStats.xaml
    /// </summary>
    public partial class ShowCharacterStats : Window
    {
        public ShowCharacterStats(CharacterProp character)
        {
            InitializeComponent();
            showCharacterInfo(character);
        }

        private void done_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void showCharacterInfo(CharacterProp character)
        {
            charName.Text = "Navn: " + character.Name;
            charAge.Text = "Alder: " + character.Age.ToString() + "år";
            charDescription.Text = character.Description;

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
    }
}
