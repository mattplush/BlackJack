using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreenvilleRevenueGUI
{
    public partial class MBox : Form
    {

        public MBox()
        {
            InitializeComponent();
 

        }

        public void Okay_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["Form1"] != null)
            {
                (System.Windows.Forms.Application.OpenForms["Form1"] as Form1).ResetAllHands();
            }
            this.Hide();
        }

        public void PlayerBlackJack(int playerScore, int dealerScore)
        {
            label2.Text = "Black Jack! You win!";
            label1.Text = "Player: " + playerScore + "\n" + "Dealer: " + dealerScore;
            this.Show();
        }

        public void DealerBlackJack(int playerScore, int dealerScore)
        {
            label2.Text = "Dealer has Black Jack...";
            label1.Text = "Player: " + playerScore + "\n" + "Dealer: " + dealerScore;
            this.Show();
        }
        public void Player21(int playerScore, int dealerScore)
        {
            label2.Text = "21! You win!";
            label1.Text = "Player: " + playerScore + "\n" + "Dealer: " + dealerScore;
            this.Show();
        }
        public void Dealer21(int playerScore, int dealerScore)
        {
            label2.Text = "Dealer has 21...";
            label1.Text = "Player: " + playerScore + "\n" + "Dealer: " + dealerScore;
            this.Show();
        }
        public void PlayerBust(int playerScore, int dealerScore)
        {
            label2.Text = "Player Bust...";
            label1.Text = "Player: " + playerScore + "\n" + "Dealer: " + dealerScore;
            this.Show();
        }
        public void DealerBust(int playerScore, int dealerScore)
        {
            label2.Text = "Dealer Bust! You Win!";

            label1.Text = "Player: " + playerScore + "\n" + "Dealer: " + dealerScore;
            this.Show();
        }
        public void PlayerHighScore(int playerScore, int dealerScore)
        {
            label2.Text = "You win!";

            label1.Text = "Player: " + playerScore + "\n" + "Dealer: " + dealerScore;
            this.Show();
        }
        public void DealerHighScore(int playerScore, int dealerScore)
        {
            label2.Text = "Dealer wins!";

            label1.Text = "Player: " + playerScore + "\n" + "Dealer: " + dealerScore;
            this.Show();
        }

        public void Tie(int playerScore, int dealerScore)
        {
            label2.Text = "Tie!";

            label1.Text = "Player: " + playerScore + "\n" + "Dealer: " + dealerScore;
            this.Show();
        }

        public void DoubleBlackJack(int playerScore, int dealerScore)
        {
            label2.Text = "Double Black Jack! Tie!";

            label1.Text = "Player: " + playerScore + "\n" + "Dealer: " + dealerScore;
            this.Show();
        }

        public void DoubleBust(int playerScore, int dealerScore)
        {
            label2.Text = "Double Bust... house wins";
            label1.Text = "Player: " + playerScore + "\n" + "Dealer: " + dealerScore;
            this.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
