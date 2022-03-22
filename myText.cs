/*
* FILE          : myText.cs
* PROJECT       : PROG2121 - A00
* PROGRAMMER    : Andrea Ferro
* FIRST VERSION : 2021 - 09 - 24
* DESCRIPTION   :
*    This file contains all the necessary a simple class to help tracking changes inside the textbox to determine 
*    if the user needs to be asked if the file needs to be saved.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A02___WPF
{
    class myText
    {
        private bool hasTextChanged;
        private bool close;
        public bool HasTextChanged
        {
            get
            {
                return hasTextChanged;

            }
            set
            {
                hasTextChanged = value;
            }
        }

        public bool Close
        {
            get
            {
                return close;

            }
            set
            {
                close = value;
            }
        }



        /* Name	    : myText -- CONSTRUCTOR
           Purpose  : To instantiate a new text object to help keep track of the event of the text changed inside the main form.
           Inputs	:	NONE
           Outputs	:	NONE
           Returns	:	Nothing
        */

        public myText()
        {
            hasTextChanged = false;
            close = false;
        }

    }

}
