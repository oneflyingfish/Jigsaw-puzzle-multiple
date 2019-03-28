using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace 智力拼图多样版
{
    class MyPicture
    {
        public static void CreatPicture()
        {
            
            int n = Form1.n;
            string PictureFilePath = null;
            DialogResult choose = MessageBox.Show("是否选择内置图片,如果选择“否”，将打开对话框要求您自己选择", "图片决定选项", MessageBoxButtons.YesNo);
            if (choose == DialogResult.No)
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.ShowDialog();
                PictureFilePath = fileDialog.FileName;
            }
            if (PictureFilePath == null || PictureFilePath == "")
            {
                PictureFilePath = System.Environment.CurrentDirectory + "\\init.jpg";
            }//获得了正确的路径

            //开始切割\
            Image picture = Image.FromFile(PictureFilePath);
            Bitmap NewPictureFile= NewPictureFile = new Bitmap(picture, new Size(50 * n, 50 * n));//指定画布大小
            for (int i = 0;i < n;i++)
            {
                for(int j=0;j<n;j++)
                {
                     
                     Form1.pictures[i,j]= NewPictureFile.Clone(new Rectangle(50*i,50*j,50,50), NewPictureFile.PixelFormat);
                }           
            }         
        }
    }
}
