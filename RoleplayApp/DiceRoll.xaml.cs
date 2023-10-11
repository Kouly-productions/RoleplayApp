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
    /// Interaction logic for DiceRoll.xaml
    /// </summary>
    public partial class DiceRoll : Window
    {
        int randomNum;

        Random random = new Random();

        public DiceRoll()
        {
            InitializeComponent();
        }

        private void Dice6_Click(object sender, RoutedEventArgs e)
        {
            randomNum = random.Next(1, 7);
            rollText.Text = randomNum.ToString();
        }

        private void Dice10_Click(object sender, RoutedEventArgs e)
        {
            randomNum = random.Next(1, 11);
            rollText.Text = randomNum.ToString();
        }

        private void Dice20_Click(object sender, RoutedEventArgs e)
        {
            randomNum = random.Next(1, 21);
            rollText.Text = randomNum.ToString();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            double x = this.Left;
            double y = this.Top;

            MainWindow mainWindow = new MainWindow();
            mainWindow.Left = x;
            mainWindow.Top = y;
            this.Close();
            mainWindow.Show();
        }
    }
}
