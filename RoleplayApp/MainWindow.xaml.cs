using RoleplayApp.Fantasy;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RoleplayApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://wheelofnames.com/jt9-eas",
                UseShellExecute = true,
            });
        }

        //Dice
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            double x = this.Left;
            double y = this.Top;

            DiceRoll diceRoll = new DiceRoll();
            diceRoll.Left = x;
            diceRoll.Top = y;
            this.Close();
            diceRoll.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            double x = this.Left;
            double y = this.Top;

            VoteWindow voteWindow = new VoteWindow();
            voteWindow.Left = x;
            voteWindow.Top = y;
            this.Close();
            voteWindow.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            double x = this.Left;
            double y = this.Top;

            Character character = new Character();
            character.Left = x;
            character.Top = y;
            this.Close();
            character.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            double x = this.Left;
            double y = this.Top;

            FantasyShowCharacters fantasy = new FantasyShowCharacters();
            fantasy.Left = x;
            fantasy.Top = y;
            fantasy.Show();
            this.Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            FantasyAbilityWindow abilityWindow = new FantasyAbilityWindow(null);
            abilityWindow.Show();
            this.Close();
        }
    }
}
