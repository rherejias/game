using System;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class Highscore : Form
    {
        Database db = new Database();
        string time = "";
        public Highscore(string Time)
        {
            InitializeComponent();

            time = Time;
            label5.Text = time;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start memo = new Start();
            db.Insert(textBox1.Text, label5.Text);
            Visible = false;
            memo.Show();

        }
    }
}
