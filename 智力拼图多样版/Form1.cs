using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace 智力拼图多样版
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeComponent0();
        }

        private void InitializeComponent0()
        {
            this.start = new System.Windows.Forms.Button();
            this.resultn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(147, 206);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(79, 28);
            this.start.TabIndex = 0;
            this.start.Text = "开始";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // resultn
            // 
            this.resultn.Location = new System.Drawing.Point(115, 133);
            this.resultn.Name = "resultn";
            this.resultn.Size = new System.Drawing.Size(156, 21);
            this.resultn.TabIndex = 1;
            resultn.KeyDown +=start_KeyDown;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "请输入一个整数，范围在3~13，表示拼图的规模";
            // 
            // Getn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 295);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.resultn);
            this.Controls.Add(this.start);
            //this.Name = "Getn";
            //this.Text = "Getn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private  void start_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                startcode();
            }
        }
        private void start_Click(object sender, EventArgs e)
        {
            startcode();

        }

        private void startcode()
        {
            int n;
            if (int.TryParse(resultn.Text, out n))
            {
                if (n < 3 || n > 13)
                {
                    MessageBox.Show("请检查数字范围是否合理");
                    return;
                }
                Form1.n = n;
                //this.timer1.Enabled = false;
                MyPicture.CreatPicture();
                this.CreatePictureBoxs();
                this.Width = 70 + 52 * n;
                this.Height = 130 + 52 * n;
                this.resultn.Dispose();
                this.start.Dispose();
                this.label1.Dispose();
                JudgeWin();

            }
            else
            {
                MessageBox.Show("请确保只输入了一个数字");
            }
        }
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.TextBox resultn;
        private System.Windows.Forms.Label label1;
        public static Bitmap[,] pictures = new Bitmap[13, 13];
        public static int n = 0;
        public static PictureBox [,] picturemap=new PictureBox  [13,13];//记录所有图片的地址
        //制造图形
        public void CreatePictureBoxs()//拼图规模是n*n
        {
            int n = Form1.n;
            PictureBox[,] pictureboxs = new PictureBox[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    pictureboxs[i, j] = new PictureBox();
                    pictureboxs[i, j].Image = pictures[i, j];// MyPicture.pictures[i, j]
                    pictureboxs[i, j].Height = 50;
                    pictureboxs[i, j].Width = 50;
                    picturemap[i, j] = pictureboxs[i, j];//每个位置图片的地址记录下来
                    pictureboxs[i, j].Location = new Point(30 + 52 * i, 30 + 52 * j);
                    pictureboxs[i, j].Visible = true;
                    pictureboxs[i, j].Name = (i * n + j).ToString();
                    pictureboxs[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureboxs[i, j].Tag = i * n + j;
                    pictureboxs[i, j].Click += pictureboxs_clike;
                    this.Controls.Add(pictureboxs[i, j]);
                }
            }
            pictureboxs[n - 1, n - 1].Visible = false;
            Button startbutton = new Button();
            startbutton.Location = new Point(26 * n + 10, 50 + 52 * n);
            startbutton.Width = 50;
            startbutton.Height = 20;
            startbutton.Text = "开始";
            startbutton.Click += Startbutton_Click;
            this.Controls.Add(startbutton);
        }
        
        /// <summary>
        /// 点击交换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureboxs_clike(object sender, EventArgs e)
        {
            PictureBox box = sender as PictureBox;
            PictureBox box0 = Findblank();
            int m = int.Parse(box.Name.ToString());
            int m0= int.Parse(box0.Name.ToString());
            int i0 = m0 / n;
            int j0 = m0 % n;
            int i = m / n;
            int j = m % n;
            if ((i==i0&&Math.Abs(j-j0)==1)||((j == j0 && Math.Abs(i - i0) == 1)))
            {            
                swap(box0, box);
                if(JudgeWin()==true)
                {
                    MessageBox.Show("Congratulations!");
                }
            }
        }
        private void Startbutton_Click(object sender, EventArgs e)
        {
            shuffer();
        }

        /// <summary>
        /// 随机打乱图片
        /// </summary>
        private void shuffer()
        {
            Random rand = new Random();
            int a = 0;
            for (int i=0;i<1000;i++)
            {
                a = rand.Next(4);
                PictureBox box0 = Findblank();
                int m = int.Parse(box0.Name.ToString());
                if (a==0&& (m/n!=0))//最上面
                {
                    swap(box0,picturemap[m/n-1,m%n]);
                }
                else if(a==1&&(m % n != (n-1)))//最右边
                {
                    swap(box0, picturemap[m/n,m%n+1]);
                }
                else if(a==2&& (m / n != (n-1)))//最下边
                {
                    swap(box0, picturemap[m/n+1,m%n]);
                }
                else if(a==3&& (m % n != 0))//最左边
                {
                    swap(box0, picturemap[m/n,m%n-1]);
                }
            }
            

        }
        
        /// <summary>
        /// 查找空格
        /// </summary>
        private PictureBox Findblank()
        {
            for(int i=0;i<n;i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if(picturemap[i,j].Visible ==false)
                    {
                        return picturemap[i, j];
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 交换两个图片的内容
        /// </summary>
        /// <param name="box1"></param>
        /// <param name="box2"></param>
        private void swap(PictureBox box1, PictureBox box2)
        {
            PictureBox box = new PictureBox();
            box.Image = box1.Image;
            box.Tag = box1.Tag;
            box.Visible = box1.Visible;

            box1.Image = box2.Image;
            box1.Tag = box2.Tag;
            box1.Visible = box2.Visible;

            box2.Image = box.Image;
            box2.Tag = box.Tag;
            box2.Visible = box.Visible;

        }

        //判断是否完成
        private bool JudgeWin()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (int.Parse(picturemap[i,j].Tag.ToString() )!= (i * n + j))
                    {
                        return false;
                    }

                }
            }
            //MessageBox.Show("Congratulations!");
            return true;
        }


        private void Start_Clike()
        {
            shuffer();
        }
        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    if (n != 0)
        //    {
        //        this.timer1.Enabled = false;
        //        MyPicture.CreatPicture();
        //        this.CreatePictureBoxs();
        //        this.Width = 70 + 52 * n;
        //        this.Height = 130 + 52 * n;
        //        JudgeWin();
        //    }

        //}

    }
}
