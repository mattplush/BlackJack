using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreenvilleRevenueGUI
{
    class Card
    {
        int value;
        bool isAce;
        Image image;
        public Card(Image myImage, int myValue)
        {
            image = myImage;
            value = myValue;
            isAce = false;
        }


        public Image GetImage()
        {
            return image;
        }
        public int GetValue()
        {
            return value;
        }

        public void SetCardToAce()
        {
            isAce = true;
        }

        public void SetValue1()
        {
            value = 1;
        }

        public void SetValue11()
        {
            value = 11;
        }

        public bool IsCardAce()
        {
            if(isAce)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }    
}
