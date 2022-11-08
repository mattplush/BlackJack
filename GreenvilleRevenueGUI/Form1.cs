using System;
using System.Windows.Forms;

namespace GreenvilleRevenueGUI
{
    public partial class Form1 : Form
    {
        MBox message = new MBox();
        Cards DeckOfCards = new Cards();
        Card BackCard;
        Hand PlayerHand;
        Hand DealerHand;
        Money myMoney = new Money(5000);
        Boolean hideLabel = true;
        Boolean gameEnd;

        public Form1()
        {

            InitializeComponent();
            PlayerHand = new Hand("Player");
            DealerHand = new Hand("Dealer");
            BackCard = DeckOfCards.GetBackCard();
            textBox2.Text = " $" + myMoney.GetTotalMoney();
            playerBetChange.Value = myMoney.GetBetAmount();
            playerBetChange.Maximum = myMoney.GetTotalMoney();
            gameEnd = false;
            HitButton.Enabled = false;
            StayButton.Enabled = false;
            playerBetChange.Enabled = true;

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            ResetAllHands();
            Start();
        }

        private void Start()
        {
            playerBetChange.Enabled = false;
            AllInButton.Enabled = false;
            HitButton.Enabled = true;       //unlocks the Hit button
            StayButton.Enabled = true;
            DealACardtoDealer(hideLabel);
            DealACardtoPlayer();
            DealACardtoDealer(hideLabel);
            DealACardtoPlayer();

            int playerScore = PlayerHand.GetTotalValueofCards();
            int dealerScore = DealerHand.GetTotalValueofCards();

            CheckPlayerBust();
            CheckDealerBust();

            if (playerScore == 21 & dealerScore == 21)
            {
                FlipDealerCard();
                message.DoubleBlackJack(playerScore, dealerScore);
            }
            else if (dealerScore == 21)
            {
                FlipDealerCard();
                LostBet();
            }
            else if (playerScore == 21)
            {
                WonBet();
            }
        }

        private void HitButton_Click(object sender, EventArgs e)
        {

            if (HitButton.Enabled)
            {
                playerBetChange.Enabled = false;
                DealACardtoPlayer();
                CheckPlayerBust();
            }
        }
        private void StayButton_Click(object sender, EventArgs e)
        {
            HitButton.Enabled = false;
            FlipDealerCard();
            playerBetChange.Enabled = false;
            DealerBrain();
        }


        private void DealerBrain()
        {
            Change11to1WhenBust();

            while (DealerHand.GetNumberofCards() < 5 & DealerHand.GetTotalValueofCards() < 17 | DealerHand.GetNumberofCards() < 5 & DealerHand.GetTotalValueofCards() < PlayerHand.GetTotalValueofCards() & PlayerHand.GetTotalValueofCards() > 16 & PlayerHand.GetTotalValueofCards() < 22)
            //deal a card to dealer while cards < 5 and value is less than 17.. [OR] ... while cards < 5, dealer's value is less then player's, and player's value is 17,18,19,20, or 21 //
            {
                DealACardtoDealer(hideLabel);

                Change11to1WhenBust();
                
            }

            Evaluation();

        }
        private void Change11to1WhenBust()
        {
            if (CheckDealerBust())
            {
                if (AceSwapSuccess())
                {
                    return;
                }
                else
                {
                    Evaluation();
                }

            }
            else
            {
                return;
            }
        }


        private bool AceSwapSuccess()
        {
            int dCards = DealerHand.GetNumberofCards();

            for (int i = 0; i < dCards; i++)
            {
                Card ACard = DealerHand.GetaCard(i);

                if (ACard.GetValue() == 11)
                {
                    SetDealerCardAceValue(i);
                    return true;
                }
            }
            return false;
        }

      
        private void Evaluation()
        {
            int pValue = PlayerHand.GetTotalValueofCards();
            int dValue = DealerHand.GetTotalValueofCards();


            if (CheckPlayerBust() & CheckDealerBust())
            {
                LostBet();
            }
            else if (CheckPlayerBust())
            {
                LostBet();
            }
            else if (CheckDealerBust())
            {
                WonBet();
            }
            else if (pValue == 21 & dValue == 21) //evaluates if player and dealer both reach 21
            {
                Tie(pValue, dValue);
            }
            else if (pValue == 21) // evaluates if player reaches 21
            {
                WonBet();
            }
            else if (dValue == 21) // evaluates if dealer reaches 21
            {
                LostBet();
            }
            else if (pValue == dValue & pValue != 0)
            {
                Tie(pValue, dValue);
            }
            else
            {

                if ((21 - dValue) < (21 - pValue))
                {
                    LostBet();
                }
                else if ((21 - pValue) < (21 - dValue))
                {
                    WonBet();
                }
                else
                {
                    return;
                }
            }
        }


        private bool CheckPlayerBust()
        {
            int playerScore = PlayerHand.GetTotalValueofCards();
            int dealerScore = DealerHand.GetTotalValueofCards();

            if (playerScore > 21) //evaluates if player busts
            {
                HitButton.Enabled = false;
                return true;
            }
            else if (playerScore == 21)
            {
                HitButton.Enabled = false;
                return false;
            }
            else
            {
                HitButton.Enabled = true;
                return false;
            }
        }
        private bool CheckDealerBust()
        {
            int pValue = PlayerHand.GetTotalValueofCards();
            int dValue = DealerHand.GetTotalValueofCards();

            if (dValue > 21) //evaluates if dealer busts
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Tie(int playerScore, int dealerScore)
        {
            gameEnd = true;
            message.Tie(playerScore, dealerScore);
        }


        private void WonBet()
        {
            gameEnd = true;
            int playerScore = PlayerHand.GetTotalValueofCards();
            int dealerScore = DealerHand.GetTotalValueofCards();
            int playerCards = PlayerHand.GetNumberofCards();
            myMoney.WonBet();

            textBox2.Text = " $" + myMoney.GetTotalMoney();

            if (playerCards == 2 & playerScore == 21)
            {
                FlipDealerCard();
                message.PlayerBlackJack(playerScore, dealerScore);

            }

            else if (playerScore == 21)
            {
                message.Player21(playerScore, dealerScore);

            }

            else if (dealerScore > 21)
            {
                message.DealerBust(playerScore, dealerScore);
            }

            else if (playerScore > dealerScore)
            {
                message.PlayerHighScore(playerScore, dealerScore);

            }
        }

        private void LostBet()
        {
            gameEnd = true;
            int playerScore = PlayerHand.GetTotalValueofCards();
            int dealerScore = DealerHand.GetTotalValueofCards();
            int dealerCards = DealerHand.GetNumberofCards();

            myMoney.LostBet();
            textBox2.Text = " $" + myMoney.GetTotalMoney();


            if (dealerScore > 21 & playerScore > 21)
            {
                message.DoubleBust(playerScore, dealerScore);
            }
            else if (dealerCards == 2 & dealerScore == 21)
            {
                message.DealerBlackJack(playerScore, dealerScore);

            }

            else if (dealerScore == 21)
            {
                message.Dealer21(playerScore, dealerScore);

            }

            else if (playerScore > 21)
            {
                message.PlayerBust(playerScore, dealerScore);

            }

            else if (playerScore < dealerScore)
            {
                message.DealerHighScore(playerScore, dealerScore);

            }

            gameEnd = true;

        }

        public void ResetAllHands()
        {

            if (gameEnd)
            {

                if (myMoney.GetTotalMoney() == 0)
                {
                    playerBetChange.Value = 0;
                    AllInButton.Enabled = false;
                }
                else if (myMoney.GetTotalMoney() >= 10)
                {
                    playerBetChange.Value = 10;
                }

                playerBetChange.Enabled = true;
                AllInButton.Enabled = true;
                HitButton.Enabled = false;
                StayButton.Enabled = false;

            }

            gameEnd = false;
            hideLabel = true; //resets boolean for hidden card values


            playerBetChange.Maximum = myMoney.GetTotalMoney();

            DealerHand.ResetHand();
            PlayerHand.ResetHand();


            ResetAllCardImagesIntheHands();
        }

        private void ResetAllCardImagesIntheHands()
        {

            DCard1.Image = BackCard.GetImage();
            DLabel1.Text = "";
            DCard2.Image = BackCard.GetImage();
            DLabel2.Text = "";
            DCard3.Image = BackCard.GetImage();
            DLabel3.Text = "";
            DCard4.Image = BackCard.GetImage();
            DLabel4.Text = "";
            DCard5.Image = BackCard.GetImage();
            DLabel5.Text = "";

            PCard1.Image = BackCard.GetImage();
            PLabel1.Text = "";
            PCard2.Image = BackCard.GetImage();
            PLabel2.Text = "";
            PCard3.Image = BackCard.GetImage();
            PLabel3.Text = "";
            PCard4.Image = BackCard.GetImage();
            PLabel4.Text = "";
            PCard5.Image = BackCard.GetImage();
            PLabel5.Text = "";

            PScore.Text = "";
            DScore.Text = "";

        }

        private void DealACardtoDealer(Boolean hidelabel)
        {
            int cardno = DealerHand.GetNumberofCards();
            Card ACard = DeckOfCards.GetNextCard();
            DealerHand.DealACardtoMe(ACard);

            UpdateDealerGraphics(cardno, hidelabel);
        }
        private void DealACardtoPlayer()
        {
            int cardnumber = PlayerHand.GetNumberofCards();

            if (cardnumber < 5)
            {
                Card ACard = DeckOfCards.GetNextCard();
                PlayerHand.DealACardtoMe(ACard);
                UpdatePlayerGraphics(cardnumber);
            }
        }
        private void UpdateDealerGraphics(int cardno, Boolean hidelabel)
        {

            Card ACard = DealerHand.GetaCard(cardno);
            switch (cardno)
            {

                case 0:

                    DCard1.Image = ACard.GetImage();
                    DLabel1.Text = "" + ACard.GetValue();
                    DScore.Text = "" + DealerHand.GetTotalValueofCards();
                    break;

                case 1:

                    if (hidelabel)
                    {
                        DCard2.Image = BackCard.GetImage();
                        DLabel2.Text = "?";
                        DScore.Text = (DealerHand.GetTotalValueofCards() - ACard.GetValue()) + " + ?";
                    }
                    else
                    {
                        DCard2.Image = ACard.GetImage();
                        DLabel2.Text = "" + ACard.GetValue();
                        DScore.Text = "" + DealerHand.GetTotalValueofCards();
                    }
                    break;

                case 2:


                    DCard3.Image = ACard.GetImage();
                    DLabel3.Text = "" + ACard.GetValue();
                    DScore.Text = "" + DealerHand.GetTotalValueofCards();

                    break;

                case 3:

                    DCard4.Image = ACard.GetImage();
                    DLabel4.Text = "" + ACard.GetValue();
                    DScore.Text = "" + DealerHand.GetTotalValueofCards();

                    break;

                case 4:


                    DCard5.Image = ACard.GetImage();
                    DLabel5.Text = "" + ACard.GetValue();
                    DScore.Text = "" + DealerHand.GetTotalValueofCards();

                    break;

            }
        }


        private void UpdatePlayerGraphics(int cardnumber)
        {
            Card ACard = PlayerHand.GetaCard(cardnumber);

            switch (cardnumber)
            {
                case 0:
                    PCard1.Image = ACard.GetImage();
                    PLabel1.Text = "" + ACard.GetValue();
                    PScore.Text = "" + PlayerHand.GetTotalValueofCards();
                    break;
                case 1:
                    PCard2.Image = ACard.GetImage();
                    PLabel2.Text = "" + ACard.GetValue();
                    PScore.Text = "" + PlayerHand.GetTotalValueofCards();
                    break;
                case 2:
                    PCard3.Image = ACard.GetImage();
                    PLabel3.Text = "" + ACard.GetValue();
                    PScore.Text = "" + PlayerHand.GetTotalValueofCards();
                    break;
                case 3:
                    PCard4.Image = ACard.GetImage();
                    PLabel4.Text = "" + ACard.GetValue();
                    PScore.Text = "" + PlayerHand.GetTotalValueofCards();
                    break;
                case 4:
                    PCard5.Image = ACard.GetImage();
                    PLabel5.Text = "" + ACard.GetValue();
                    PScore.Text = "" + PlayerHand.GetTotalValueofCards();
                    break;
            }
        }
        private void FlipDealerCard()
        {

            hideLabel = false;


            UpdateDealerGraphics(1, hideLabel);
        }



        private void SetDealerCardAceValue(int cardNumber)
        {
            DealerHand.SetAceValueto1(cardNumber);
            UpdateDealerGraphics(cardNumber, false);
            CheckDealerBust();

        }
        private void SetPlayerCardAceValue(int cardNumber)
        {


            switch (PlayerHand.GetAceValue(cardNumber))
            {
                case 11:

                    PlayerHand.SetAceValueto1(cardNumber);
                    CheckPlayerBust();
                    break;

                case 1:

                    PlayerHand.SetAceValueto11(cardNumber);

                    CheckPlayerBust();
                    break;
            }

        }

        private void PCard1_Click(object sender, EventArgs e)
        {
            Card Card1 = PlayerHand.GetaCard(0);

            if (!(Card1 is null))
            {


                if (Card1.IsCardAce())
                {
                    SetPlayerCardAceValue(0);
                    UpdatePlayerGraphics(0);
                    CheckPlayerBust();
                }
                else
                {
                    return;
                }
            }

        }

        private void PCard2_Click(object sender, EventArgs e)
        {
            Card Card2 = PlayerHand.GetaCard(1);

            if (!(Card2 is null))
            {
                if (Card2.IsCardAce())
                {
                    SetPlayerCardAceValue(1);
                    UpdatePlayerGraphics(1);
                    CheckPlayerBust();
                }
                else
                {
                    return;
                }
            }



        }
        private void PCard3_Click(object sender, EventArgs e)
        {
            Card Card3 = PlayerHand.GetaCard(2);

            if (!(Card3 is null))
            {
                if (Card3.IsCardAce())
                {
                    SetPlayerCardAceValue(2);
                    UpdatePlayerGraphics(2);
                    CheckPlayerBust();
                }
                else
                {
                    return;
                }
            }

        }
        private void PCard4_Click(object sender, EventArgs e)
        {
            Card Card4 = PlayerHand.GetaCard(3);

            if (!(Card4 is null))
            {
                if (Card4.IsCardAce())
                {
                    SetPlayerCardAceValue(3);
                    UpdatePlayerGraphics(3);
                    CheckPlayerBust();
                }
                else
                {
                    return;
                }
            }
        }



        private void PCard5_Click(object sender, EventArgs e)
        {
            Card Card5 = PlayerHand.GetaCard(4);
            if (!(Card5 is null))
            {
                if (Card5.IsCardAce())
                {
                    SetPlayerCardAceValue(4);
                    UpdatePlayerGraphics(4);
                }
                else
                {
                    return;
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void playerBetChange_ValueChanged(object sender, EventArgs e)
        {
            myMoney.SetBetAmount((int)playerBetChange.Value);
        }

        private void AllInButton_Click(object sender, EventArgs e)
        {
            myMoney.SetBetAmount(myMoney.GetTotalMoney());
            playerBetChange.Value = myMoney.GetBetAmount();
        }
    }
}



