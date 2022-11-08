using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenvilleRevenueGUI
{
    class Hand
    {
        Card[] MyCards = new Card[5];
        int totalvalue = 0;
        int numberofcards = 0;


        public Hand(String Name)
        {
            String NameOfPlayer = Name;
        }

        public void DealACardtoMe(Card ACard)
        {

            if (numberofcards < 5)
            {
                MyCards[numberofcards] = ACard;
                numberofcards = numberofcards + 1;
                totalvalue = totalvalue + ACard.GetValue();
            }
        }
        public int GetNumberofCards()
        {
            return numberofcards;
        }
        public int GetTotalValueofCards()
        {
            return totalvalue;
        }

        public int GetAceValue(int index)
        {
            return MyCards[index].GetValue();
        }

        public bool IsCardAce(int index)
        {
            if (MyCards[index].IsCardAce())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ResetHand()
        {
            totalvalue = 0;
            numberofcards = 0;

            for (int i = 0; i < 5; i++)
            {
                MyCards[i] = null;
            }
        }
        public Card GetaCard(int index)
        {
            Card ACard = MyCards[index];

            return (ACard);
        }
        public void SetAceValueto1(int i)
        {
            Card ACard = MyCards[i];
            int oldValue = 11;
            ACard.SetValue1();
            ResetTotalValue(oldValue);
        }

        public void SetAceValueto11(int i)
        {
            Card ACard = MyCards[i];
            int oldValue = 1;
            ACard.SetValue11();
            ResetTotalValue(oldValue);
        }
        public void ResetTotalValue(int oldValue)
        {
            switch (oldValue)
            {
                case 11:

                    totalvalue = (totalvalue + 1) - oldValue;
                    break;

                case 1:

                    totalvalue = (totalvalue + 11) - oldValue;
                    break;
            }

        }

    }
}