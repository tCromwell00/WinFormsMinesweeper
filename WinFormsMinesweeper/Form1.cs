using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsMinesweeper
{
    public partial class Minesweeper : Form
    {
        Random r = new Random();
        public int c = 0;
        public GridCell[,] tiles;
        public int size;
        int time = 0;
        int counter = 0;
        Encoding encoding = Encoding.Unicode;
        public Minesweeper()
        {

            SelectDifficulty diff = new SelectDifficulty();
            diff.ShowDialog();
            InitializeComponent();
            tiles = new GridCell[diff.difficulty, diff.difficulty];
            this.Size = new Size((tiles.GetUpperBound(0) + 1) * 42, (tiles.GetUpperBound(tiles.Rank - 1) + 1) * 49 + 2);
            size = diff.difficulty;
            GenerateTiles(tiles);
            LayMines();
            FindNearby();
        }

        private void Form1_Load(object sender,EventArgs e)
        {
            
        }

        

        public void GenerateTiles(GridCell[,] cells)
        {
            int rowNum = cells.GetUpperBound(0) + 1;
            int colNum = cells.GetUpperBound(cells.Rank - 1) + 1;

            for (int i=0; i < rowNum; i++)
            {
                for (int j=0; j<colNum; j++)
                {
                    cells[i, j] = new GridCell(i,j);
                    cells[i, j].Location = new Point(j * 40, 50 + (i * 40));
                    cells[i, j].MouseDown += new MouseEventHandler(button_MouseClick);
                    cells[i, j].Paint += new PaintEventHandler(_Paint);

                    Controls.Add(cells[i, j]);
                }
            }
        }
        public void _Paint(object sender, PaintEventArgs e)
        {
            if((sender as GridCell).getRevealed() == false)
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as System.Windows.Forms.Button).ClientRectangle,
                    System.Drawing.SystemColors.ControlLightLight, 3, ButtonBorderStyle.Outset,
                    System.Drawing.SystemColors.ControlLightLight, 3, ButtonBorderStyle.Outset,
                    System.Drawing.SystemColors.ControlLightLight, 3, ButtonBorderStyle.Outset,
                    System.Drawing.SystemColors.ControlLightLight, 3, ButtonBorderStyle.Outset);
            }
            else if ((sender as GridCell).getRevealed() == true)
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as System.Windows.Forms.Button).ClientRectangle,
                    System.Drawing.SystemColors.ControlLightLight, 3, ButtonBorderStyle.Inset,
                    System.Drawing.SystemColors.ControlLightLight, 3, ButtonBorderStyle.Inset,
                    System.Drawing.SystemColors.ControlLightLight, 3, ButtonBorderStyle.Inset,
                    System.Drawing.SystemColors.ControlLightLight, 3, ButtonBorderStyle.Inset);
            }
                
            
            
        }
        public void LayMines()
        {
            GridCell[] liveTiles = new GridCell[100];
            for(int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (r.Next(1, 101) < 15)
                    {
                        tiles[i, j].setActive(true);
                        tiles[i, j].setNearby(9);
                        liveTiles[c] = tiles[i, j];
                        c++;
                    }

                }
            }
        }

        public void RevealNearby(int row, int column)
        {
            if (row<0 || row >= size || column<0 ||column >=size) { return; }
            if (tiles[row, column].getNearby() < 9 && tiles[row, column].getRevealed()==false)
            {
                if (tiles[row, column].getNearby() == 0)
                {
                    tiles[row, column].setRevealed(true);
                    tiles[row, column].Text = " ";
                    counter++;
                    RevealNearby(row + 1, column);
                    RevealNearby(row - 1, column);
                    RevealNearby(row, column+1);
                    RevealNearby(row, column-1);
                    RevealNearby(row + 1, column+1);
                    RevealNearby(row + 1, column-1);
                    RevealNearby(row - 1, column + 1);
                    RevealNearby(row - 1, column - 1);


                }
                if (tiles[row, column].getNearby() > 0)
                {
                    tiles[row, column].setRevealed(true);
                    tiles[row, column].Text = tiles[row, column].getNearby().ToString();
                    
                    switch (tiles[row,column].getNearby())
                    {
                        case 1:
                            tiles[row, column].ForeColor = Color.Blue;
                            break;
                        case 2:
                            tiles[row, column].ForeColor = Color.Green;
                            break;
                        case 3:
                            tiles[row, column].ForeColor = Color.Red;
                            break;
                        case 4:
                            tiles[row, column].ForeColor = Color.DarkBlue;
                            break;
                        case 5:
                            tiles[row, column].ForeColor = Color.Maroon;
                            break;
                        case 6:
                            tiles[row, column].ForeColor = Color.Aquamarine;
                            break;
                        case 7:
                            tiles[row, column].ForeColor = Color.Black;
                            break;
                        case 8:
                            tiles[row, column].ForeColor = Color.DimGray;
                            break;
                        default:
                            tiles[row, column].ForeColor = Color.Black ;
                            break;
                    }
                    counter++;
                }
            }

            else if(tiles[row,column].getActive()==true)
            {
                tiles[row, column].setRevealed(true);
                tiles[row, column].Text = "💣";
                tiles[row, column].Refresh();
                DialogResult dialog = MessageBox.Show("Game Over. You Lose.");
                if (dialog == DialogResult.OK)
                {
                    Application.Exit();
                }
            }

            if (counter + c == (size * size))
            {
                DialogResult dialog = MessageBox.Show("You win!!!" + time);
                if(dialog == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }

        public void FindNearby()
        {
            int rowLimit = tiles.GetLength(0);
            int colLimit = tiles.GetLength(1);

            for(int i = 0; i < tiles.GetLength(0); i++)
            {
                for(int j = 0; j < tiles.GetLength(1); j++)
                {
                    if(tiles[i, j].getNearby() < 9)
                    {
                        int neighbors = 0;
                        for(int x = Math.Max(0, i - 1); x <= Math.Min(i + 1, rowLimit - 1); x++)
                        {
                            for(int y= Math.Max(0, j - 1); y <= Math.Min(j + 1, colLimit - 1); y++)
                            {
                                if (x != i || y != j)
                                {
                                    bool check = tiles[x, y].getActive();
                                    if (check)
                                    {
                                        neighbors++;
                                    }
                                }
                            }
                        }
                        tiles[i, j].setNearby(neighbors);
                    }
                }
            }
        }

        private void button_MouseClick(object sender, MouseEventArgs e)
        {
            GridCell clicked = (GridCell)sender;

            if (e.Button == MouseButtons.Right)
            {
                if (clicked.Text.Equals(""))
                {
                    clicked.Text = "🚩";
                }
                else if (clicked.Text.Equals("🚩"))
                {
                    clicked.Text = "?";
                }
                else if (clicked.Text.Equals("?"))
                {
                    clicked.Text = "";
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                RevealNearby(clicked.getRow(),clicked.getCol());
            }
        }

        private void Timer1_Tick(object sender,EventArgs e)
        {
          
        }
    }
}
