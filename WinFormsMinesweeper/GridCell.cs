using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsMinesweeper
{
    public class GridCell : Button 
    {
        int row = -1;
        int col = -1;
        bool revealed = false;
        bool active = false;
        int nearby = 0;

        public GridCell(int x,int y)
        {
            row = x;
            col = y;
            this.FontHeight = 25;
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(40, 40);
            }
        }
        


        //Getter methods
        public int getRow()
        {
            return row;
        }

        public int getCol()
        {
            return col;
        }

        public bool getRevealed()
        {
            return revealed;
        }

        public bool getActive()
        {
            return active;
        }

        public int getNearby()
        {
            return nearby;
        }

        //Setter methods

        public void setRow(int x)
        {
            row = x;
        }

        public void setCol(int y)
        {
            col = y;
        }

        public void setRevealed(bool r)
        {
            revealed = r;
        }

        public void setActive(bool a)
        {
            active = a;
        }

        public void setNearby(int n)
        {
            nearby = n;
        }

       
    }
}
