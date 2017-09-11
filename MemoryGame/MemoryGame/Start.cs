using System;
using System.Windows.Forms;


namespace MemoryGame
{
    public partial class Start : Form
    {
        Database db = new Database();
        public Start()
        {
            InitializeComponent();
            db.selectScore();
            dataGridView1.DataSource = Database.bindingSource1;
        }

        private void Start_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            Menu.Visible = false;
            dataGridView1.Visible = true;
        }



        private void Start_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)
            {
                Memory memory = new Memory();
                memory.Show();
                Visible = false;
            }
        }


        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            label2.Visible = true;
            Menu.Visible = true;
            dataGridView1.Visible = false;
        }
    }
}
