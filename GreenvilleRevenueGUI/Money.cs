using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace GreenvilleRevenueGUI
{
    class Money
    {
        private int total = 0;
        private int bet = 10;

        public Money(int initialMoney)
        {
            total = initialMoney;
        }

        public int GetBetAmount()
        {
            return bet;
        }

        public int GetTotalMoney()
        {
            return total;
        }

        public void SetBetAmount(int amount)
        {

            bet = amount;

        }

        public void WonBet()
        {
            total = total + bet;
        }

        public void LostBet()
        {
            total = total - bet;
        }


    }




}
