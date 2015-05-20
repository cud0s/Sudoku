using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int[,] ua;
        int difficulty;
        public Form1()
        {
            InitializeComponent();
        }
        TextBox[,] tb = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            tb = new TextBox[9, 9];
            int space = 33;
            int yV = 0;
            for (int j = 0; j < 9; j++)
            {
                bool z = false;
                int x = 10;
                int y = 10;
                int xV = 0;
                for (int i = 0; i < 9; i++)
                {
                    tb[i, j] = new TextBox();
                    tb[i, j].Visible = true;
                    tb[i, j].TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                    tb[i, j].Top = tb[i, j].Height / 2;
                    
                    if (i == 3 || i == 6)
                    {
                        x = i * space;
                        xV += 10;
                    }
                    else
                    {
                        x = i * space;
                    }

                    if ((j == 3 || j == 6) && z==false)
                    {
                        y = j * space;
                        yV += 10;
                        z = true;
                    }
                    else
                    {
                        y = j * space;
                    }

                    tb[i, j].Location = new Point(x + 10 + xV, y+15 + yV);

                    tb[i, j].Width = 30;
                    tb[i, j].MaxLength = 1;
                    tb[i, j].Multiline = true;
                    tb[i, j].Height = 30;
                    this.Controls.Add(tb[i, j]);
                }
            }

            mediumToolStripMenuItem_Click(sender,e);
            button1_Click(sender, e);
            radioButton2.Checked = true;
        }

        static Random _r = new Random();
        static int F(int a)
        {
            int n = _r.Next(a);
            // Can return 0, 1, 2, 3, or 4
            return n;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ua = new int[10, 10];

            var grid = new generat();
            int[,] arr = grid.main();
            for (int a = 0; a < 9; a++)
            {
                for (int b = 0; b < 9; b++)
                {
                    tb[a, b].BackColor = Color.White;
                    ua[a, b] = arr[a, b];
                    tb[a, b].Text = arr[a, b].ToString();
                }
            }
            for (int a = 0; a < 9; a++)
            {
                for (int b = 0; b < 9; b++)
                {
                    int ret = F(difficulty);
                    if (ret == 1)
                    {
                        tb[a, b].Text = "";
                    }
                }
            }
        }
        class generat
        {
            private int[] av;
            int[,] cell;
            public generat()
            {
                av = new int[10];
                cell = new int[10, 10];
            }

            public int[,] main()
            {
                int cont = 0;
                while (cont == 0)
                {
                    cont = populate(0, 0);
                }

                return cell;
            }
            private void availability(int curr_x, int curr_y, bool[] tried)
            {
                int lya, lyb, lxa, lxb;
                lya = lyb = lxa = lxb = 0;
                for (int a = 0; a < 10; a++)
                {       
                    av[a] = 1;
                }
                for (int a = 0; a < 9; a++)
                {
                    if (cell[curr_x, a] != 0)
                    {
                        for (int b = 0; b < 9; b++)
                        {
                            av[cell[curr_x, a] - 1] = 0;
                        }
                    }
                }
                for (int a = 0; a < 9; a++)
                {
                    if (cell[a, curr_y] != 0)
                    {
                        av[cell[a, curr_y] - 1] = 0;
                    }
                }

                if (curr_y <= 2)
                {
                    lya = 0;
                    lyb = 3;
                }
                else
                {
                    if (curr_y > 5)
                    {
                        lya = 6;
                        lyb = 9;
                    }
                    else
                    {
                        lya = 3;
                        lyb = 6;
                    }
                }

                if (curr_x <= 2)
                {                
                    lxa = 0;
                    lxb = 3;
                }
                else
                {
                    if (curr_x > 5)
                    {
                        lxa = 6;
                        lxb = 9;
                    }
                    else
                    {
                        lxa = 3;
                        lxb = 6;
                    }
                }

                for (int x = lxa; x < lxb; x++)
                {
                    for (int y = lya; y < lyb; y++)
                    {
                        if (cell[x, y] != 0)
                        {
                            av[cell[x, y] - 1] = 0;
                        }
                    }
                }
                for (int a = 0; a < 9; a++)
                {
                    if (tried[a] == true)
                    {
                        av[a] = 0;
                    }
                }

            }
            private void crash(int a)
            {
                //sudoku.MainWindow.Close();
            }
            private int populate(int curr_x, int curr_y)
            {
                bool[] tried;
                tried = new bool[10];
                for (int a = 0; a < 9; a++)
                {
                    tried[a] = false;
                }
                /*  for(int a=0;a<9;a++){
                      for(int b=0;b<9;b++){
                          cout<<cell[b][a];
                      }
                      cout<<"\n";
                  }*/
                int good = 0;
                int next_x = 0;
                int next_y = 0;
                if (curr_x == 9 && curr_y == 8)
                {
                    return 1;
                }
                if (curr_x == 9)
                {
                    next_x = 0;
                    next_y = curr_y + 1;
                    good = populate(next_x, next_y);
                    if (good == 1)
                    {
                        return 1;
                    }
                }
                else
                {
                    next_x = curr_x + 1;
                    next_y = curr_y;
                }
                while (good != 1)
                {
                    availability(curr_x, curr_y, tried);
                    cell[curr_x, curr_y] = choose();
                    if (cell[curr_x, curr_y] == -1)
                    {
                        cell[curr_x, curr_y] = 0;
                        return 0;
                    }
                    tried[cell[curr_x, curr_y] - 1] = true;
                    good = populate(next_x, next_y);

                    if (good == 1)
                    {
                        return 1;
                    }
                }
                return 0;
            }
            private int choose()
            {
                int[] array2;
                array2 = new int[10];
                for (int a = 0; a < 9; a++)
                {
                    array2[a] = 0;
                }
                int length = 0;
                for (int a = 0; a < 9; a++)
                {
                    if (av[a] == 1)
                    {
                        array2[length] = a;
                        length++;
                    }
                }
                if (length == 0)
                {  
                    return -1;
                }
                return array2[F(length)] + 1;
            }
            static Random _r = new Random();
            static int F(int a)
            {
                int n = _r.Next(a);
                return n;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void easyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "Easy";
            difficulty = 5;
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Medium";
            difficulty = 4;
        }

        private void hardToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "Hard";
            difficulty = 3;
        }

        private void ultraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Insane";
            difficulty = 2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for(int a=0;a<9;a++){
                for(int b=0;b<9;b++){
                    if (tb[a, b].Text != "")
                    {
                        if (ua[a, b].ToString() != tb[a, b].Text)
                        {
                            tb[a, b].BackColor = Color.LightPink;
                        }
                        else
                        {
                            tb[a, b].BackColor = Color.White;
                        }
                    }
                    else
                    {
                        tb[a, b].BackColor = Color.White;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2_Click(sender,e);
            for (int a = 0; a < 9; a++)
            {
                for (int b = 0; b < 9; b++)
                {
                    if (tb[a, b].BackColor != Color.White) {
                        tb[a, b].Text = ua[a, b].ToString();
                    }else{
                        if (tb[a, b].Text == "")
                        {
                            tb[a, b].Text = ua[a, b].ToString();
                            tb[a, b].BackColor = Color.Pink;
                        }
                    }
                }
            }
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "Easy";
            difficulty = 5;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "Medium";
            difficulty = 4;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "Hard";
            difficulty = 3;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "Insane";
            difficulty = 2;
        }

       
    }
}

 
 
           