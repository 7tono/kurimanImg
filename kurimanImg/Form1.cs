using System.IO;
using System.Windows.Forms;
using System;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kurimanImg
{
    public partial class Form1 : Form
    {
        string folderPath="";
        string filename ="";
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.DefaultExt = @"jpg";
            this.openFileDialog1.Filter =
              @"JPEG(*.jpg)|*.jpg|PNG(*.png)|*.png";
            this.openFileDialog1.FilterIndex = 1;
            this.openFileDialog1.Title = @"ファイルを開く";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
               label1.Text = this.openFileDialog1.FileName;
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
               

                folderPath = Path.GetDirectoryName(openFileDialog1.FileName);
                filename = Path.GetFileName(openFileDialog1.FileName);
                if (folderPath.Length != 0 && filename.Length != 0)
                {
                    getJpgFile(folderPath);
                }
            }
        }
        private void getJpgFile(string str)
        {
            string[] filesFullPath = System.IO.Directory.GetFiles(str, "*.jpg", System.IO.SearchOption.TopDirectoryOnly);

            listBox1.Items.Clear();
            //filesFullPath配列から一つずつ画像のフルパスを取得する→fileFullPathへ格納
            foreach (string fileFullPath in filesFullPath)
            {

                //GetFileNameメソッドをつかって、ファイル名を取得する(using System.IO;が必要)
                string fileName = Path.GetFileName(fileFullPath);

                //ファイル名のみを出力
                
                listBox1.Items.Add(fileName);
                listBox1.SelectedIndex = listBox1.Items.IndexOf(filename);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (folderPath.Length == 0 || filename.Length == 0) return;
            int no = comboBox1.SelectedIndex;

            Bitmap bmp = new Bitmap(folderPath + "//" + filename);

            int resizeWidth = 32;
            switch (no)
            {
                case 0:
                    resizeWidth = 32;break;
                case 1:
                    resizeWidth = 48;break;
                case 2:
                    resizeWidth = 64;break;

            }
            
            int resizeHeight = (int)(bmp.Height * ((double)resizeWidth / (double)bmp.Width));

            Bitmap resizeBmp = new Bitmap(resizeWidth, resizeHeight);
            Graphics g = Graphics.FromImage(resizeBmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.DrawImage(bmp, 0, 0, resizeWidth, resizeHeight);
            g.Dispose();

            //pictureBox1.Image.Dispose();
            //pictureBox1.Image = null;
            Graphics pg = pictureBox1.CreateGraphics();
            pg.DrawImage(resizeBmp, new Point(0, 0));
            resizeBmp.Save(filename + "_1.jpg");
            resizeBmp.RotateFlip(RotateFlipType.Rotate90FlipX);
            resizeBmp.Save(filename + "_2.jpg");
            resizeBmp.RotateFlip(RotateFlipType.Rotate90FlipX);
            resizeBmp.Save(filename + "_3.jpg");
            resizeBmp.RotateFlip(RotateFlipType.Rotate90FlipX);
            resizeBmp.Save(filename + "_4.jpg");
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            pictureBox1.ImageLocation = filename+"_1.jpg";
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            label1.Text = folderPath+"//"+ listBox1.SelectedItem.ToString();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.ImageLocation = label1.Text;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (object o in listBox1.Items)
            {
                filename = o.ToString();
                button1_Click(sender, e);
            }
        }
    }
}