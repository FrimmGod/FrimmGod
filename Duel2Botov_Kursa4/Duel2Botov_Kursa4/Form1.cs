using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Duel2Botov_Kursa4
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //Начать игру
        {
            richTextBox1.Text = "";

            play p = new play();
            p.b1.Name = textBox1.Text;

            p.NeedLog += B1_NeedLog;

            p.b2.Name = textBox2.Text;

            //int cnt1win = 0;
            //int total = 10000;



            p.Start((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown4.Value, (int)numericUpDown3.Value); // hp1,at1, hp2,at2
            p.Run(checkBox1.Checked);


            MessageBox.Show("Победил "+p.win.Name);


            //MessageBox.Show(p.win.log);
            //MessageBox.Show(p.b1.log);
            //MessageBox.Show(p.b2.log);

        }

        private void B1_NeedLog(object sender, EventArgs e)
        {
            
            richTextBox1.Text += sender.ToString() + "\n";

            richTextBox1.Select(richTextBox1.TextLength-1,0);

        }

        private void button2_Click(object sender, EventArgs e) // Выйти из игры
        {
            if (MessageBox.Show(this, "Вы уверены?", "Действительно хотите выйти ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            Application.Exit();
        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
