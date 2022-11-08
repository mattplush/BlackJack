using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.CodeDom.Compiler;
using System.Security.Cryptography;

namespace GreenvilleRevenueGUI
{
    class Cards
    {
        Random ranNumberGenerator;
        int currentcardnumber;
        Card[] AllCards = new Card[52];
        Card[] Aces = new Card[52];
        Card ACardBack;
        Boolean giveMeAces;

        public Cards()
        {
            ranNumberGenerator = new Random();
            LoadCards();
            ShuffleCards();
        }

        public void LoadCards()
        {
            Card ACard;
            string[] list = Directory.GetFiles(@"cards", "*.gif");
            Array.Sort(list);

            for (int index = 0; index < 52; index++)
            {
                int value = GetNextCardValue(index);
                Image image = Image.FromFile(list[index]);



                ACard = new Card(image, value);

                if (value == 11)
                {
                    ACard.SetCardToAce();
                }

                AllCards[index] = ACard;
            }

            //
            string[] list2 = Directory.GetFiles(@"cards", "USD0490860-20040601-D00000.png");
            Image Backimage = Image.FromFile(list2[0]);
            ACardBack = new Card(Backimage, 0);

        }

        private int GetNextCardValue(int currentcardnumber)
        {
            int cardvalue = 0;
            if (currentcardnumber < 33)
                cardvalue = (currentcardnumber / 4) + 2;
            else
            {
                cardvalue = 10;
            }
            if (currentcardnumber > 31 && currentcardnumber < 36)
                cardvalue = 11;//aces

            return cardvalue;
        }

        public Card GetNextCard()
        {

            int cardnumber = currentcardnumber++;

            if (currentcardnumber >= 52)
            {

                currentcardnumber = 0;

                ShuffleCards();
                return AllCards[cardnumber];

            }
            else
            {

                return AllCards[cardnumber];

            }
            
        }

        public void ShuffleCards()
        {
            giveMeAces = false;

            int timesShuffled = ranNumberGenerator.Next(100, 200); //dealer will shuffle deck between 100 - 200 times

            for (int i = 0; i < timesShuffled; i++)
            {
                int deck = AllCards.Length;

//------------------------Reset Ace Values--------------------------------------------//
                for (int index = 0; index < (deck); index++)
                {
                    Card Card1 = AllCards[index];

                    if(Card1.IsCardAce())
                    {
                        Card1.SetValue11();
                    }

//------------------------------------------------------------------------------------//

                    int random = index + ranNumberGenerator.Next(deck - index);

                    var shuffled = AllCards[index];

                    AllCards[index] = AllCards[random];

                    AllCards[random] = shuffled;

                }
            }
        }


        public Card GetBackCard()
        {
            return ACardBack;
        }



        public void PutAcesFirst()
        {
            int aceindex = 0;
            int card9index = 0;
            int card5index = 0;
            int card6index = 0;

            Boolean keepgoing = true;
            ShuffleCards();

            for (int index = 0; index < 52; index++)
            {
                Card TempCard1 = AllCards[index];

                if (TempCard1.IsCardAce())
                {
                    Card OriginalCard = AllCards[aceindex];
                    TempCard1.SetValue11();
                    AllCards[aceindex] = TempCard1;
                    AllCards[index] = OriginalCard;
                    aceindex++;
                }

            }
            // find a 9
            card9index = aceindex = 4;
            while (keepgoing)
            {
                Card TempCard1 = AllCards[aceindex];

                if (TempCard1.GetValue() == 9)
                {
                    Card OriginalCard = AllCards[card9index];// original card spot to swap

                    AllCards[card9index] = TempCard1;//put the 9 in the 5th card spot
                    AllCards[aceindex] = OriginalCard;
                    keepgoing = false;
                }
                aceindex++;

            }

            // find a 5
            card5index = aceindex = 5;
            keepgoing = true;
            while (keepgoing)
            {
                Card TempCard1 = AllCards[aceindex];

                if (TempCard1.GetValue() == 5)
                {
                    Card OriginalCard = AllCards[card5index];// original card spot to swap

                    AllCards[card5index] = TempCard1;//put the 5 in the 6th card spot
                    AllCards[aceindex] = OriginalCard;
                    keepgoing = false;
                }
                aceindex++;

            }

            // find a 6 put it in the 7th card spot
            card6index = aceindex = 6;
            keepgoing = true;
            while (keepgoing)
            {
                Card TempCard1 = AllCards[aceindex];

                if (TempCard1.GetValue() == 6)
                {
                    Card OriginalCard = AllCards[card6index];// original card spot to swap

                    AllCards[card6index] = TempCard1;//put the 6 in the 7th card spot
                    AllCards[aceindex] = OriginalCard;
                    keepgoing = false;
                }
                aceindex++;

            }


            currentcardnumber = 0;//RWW
        }


        public void Dealall4AcestoDealer()
        {
            int aceindex = 0;

            ShuffleCards();
            for (int index = 0; index < 52; index++)
            {
                Card TempCard1 = AllCards[index];

                if (TempCard1.IsCardAce())
                {
                    Card OriginalCard = AllCards[aceindex];
                    TempCard1.SetValue11();
                    AllCards[aceindex] = TempCard1;
                    AllCards[index] = OriginalCard;
                    aceindex++;
                    aceindex++;
                }
            }
            currentcardnumber = 0;//RWW
        }

    }
}

