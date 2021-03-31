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
    public partial class SelectDifficulty : Form
    {
        public int difficulty;
        public SelectDifficulty()
        {
            InitializeComponent();
        }

        
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (rbEasy.Checked)
            {
                difficulty = 10;
            }
            if (rbMedium.Checked)
            {
                difficulty = 15;
            }
            if (rbHard.Checked)
            {
                difficulty = 20;
            }
            this.Close();
        }
        private void SelectDifficulty_Load(object sender, EventArgs e)
        {

        }

    }
}
