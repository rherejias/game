using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class Memory : Form
    {
        Database db = new Database();
        string[] colorValue = new string[20];
        int clickCtr = 0;
        List<string> holder = new List<string>();
        List<PictureBox> boxHolder = new List<PictureBox>();
        PictureBox[] reference;
        int sec = 30;
        int score = 0;
        public Memory()
        {
            InitializeComponent();

            Random rand = new Random();
            string[] values = new string[20];
            List<int> exclude = new List<int>();
            int[] ctrVal = new int[20];
            int x = 12;
            int y = 12;
            int ctr = 0;
            int color = 0;


            PictureBox[] box = { pictureBox1,pictureBox2,pictureBox3,pictureBox4,pictureBox5,
                                 pictureBox6,pictureBox7,pictureBox8,pictureBox9,pictureBox10,
                                 pictureBox11,pictureBox12,pictureBox13,pictureBox14,pictureBox15,
                                 pictureBox16,pictureBox17,pictureBox18,pictureBox19,pictureBox20};

            reference = box;

            string[] pics = {"ariel.jpg",
                             "eyre.jpg",
                             "ian.jpg",
                             "jun.jpg",
                             "kaye.jpg",
                             "oc.jpg",
                             "ren.jpg",
                             "ron.jpg",
                             "ryan.jpg",
                             "tonying.jpg"};

            for (int i = 0; i < 20; i++)
            {
                do
                {
                    color = rand.Next(0, 10);
                } while (exclude.Contains(color));
                values[i] = color.ToString();

                for (int z = 0; z < values.Length; z++)
                {
                    if (values[z] == color.ToString())
                    {
                        ctrVal[i]++;
                        if (ctrVal[i] >= 3)
                        {
                            exclude.Add(Convert.ToInt32(values[i]));
                            do
                            {
                                color = rand.Next(0, 10);
                                values[i] = color.ToString();
                            } while (exclude.Contains(color));
                        }
                        else if (ctrVal[i] == 2)
                        {
                            exclude.Add(Convert.ToInt32(values[i]));
                        }
                    }
                }

                box[i].Image = Image.FromFile(@"..\..\Images\Question copy.png");
                colorValue[i] = pics[color].ToString();
                box[i].Visible = true;
                box[i].Enabled = true;


                if (i % 5 == 0 && ctr != 0)
                {
                    y += 106;
                    x = 12;
                    box[i].Location = new Point(x, y);
                }
                else if (ctr == 0)
                {
                    ctr = 1;
                    x = 12;
                    box[i].Location = new Point(x, y);
                }
                else
                {
                    ctr = 1;
                    x += 108;
                    box[i].Location = new Point(x, y);
                }

                box[i].Size = new Size(102, 100);
                this.Controls.Add(box[i]);
            }
        }

        private void clickPicture(object sender, EventArgs e)
        {
            clickCtr++;
            var Object = (PictureBox)sender;
            Object.Image = Image.FromFile(@"..\..\Images\" + colorValue[Convert.ToInt32(Object.Tag)] + "");
            holder.Add(colorValue[Convert.ToInt32(Object.Tag)]);
            boxHolder.Add(Object);
            Object.Enabled = false;

            if (clickCtr == 2)
            {
                if (holder[0].ToString() == holder[1].ToString())
                {
                    boxHolder[0].Visible = false;
                    boxHolder[1].Visible = false;
                    score += 10;
                    label4.Text = score.ToString();
                    boxHolder.Clear();
                    clickCtr = 0;

                    if (score == 100)
                    {
                        timer1.Enabled = false;
                        MessageBox.Show("Score: " + score + "\n" + "Time: " + label2.Text);
                        if (Convert.ToInt32(label2.Text) >= Convert.ToInt32(db.read()) || Convert.ToInt32(db.read()) == 0)
                        {
                            Highscore hs = new Highscore(label2.Text);
                            Visible = false;
                            hs.Show();
                        }
                        else
                        {
                            Visible = false;
                            Start st = new Start();
                            st.Show();

                        }
                    }
                }
                holder.Clear();
            }

            if (clickCtr == 3)
            {
                boxHolder[0].Image = Image.FromFile(@"..\..\Images\Question copy.png");
                boxHolder[1].Image = Image.FromFile(@"..\..\Images\Question copy.png");
                boxHolder[0].Enabled = true;
                boxHolder[1].Enabled = true;
                clickCtr = 1;
                boxHolder.RemoveRange(0, 2);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sec--;
            label2.Text = sec.ToString();

            if (sec == 0)
            {
                timer1.Enabled = false;
                MessageBox.Show("Game Over! \n Score: " + score);
                Application.Restart();
            }
        }

        private void Memory_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Space)
            {

                if (timer1.Enabled)
                {
                    timer1.Enabled = false;
                    for (int i = 0; i < reference.Length; i++)
                        reference[i].Enabled = false;
                    label5.Visible = true;
                }
                else
                {
                    timer1.Enabled = true;
                    for (int i = 0; i < reference.Length; i++)
                        reference[i].Enabled = true;
                    label5.Visible = false;
                }
            }
        }

        private void Memory_Load(object sender, EventArgs e)
        {

        }
    }
}
