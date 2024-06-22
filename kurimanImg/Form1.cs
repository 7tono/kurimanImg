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
               

                folderPath = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);
                getJpgFile(folderPath);
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

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(folderPath + "//" + listBox1.SelectedItem.ToString());

            int resizeWidth = 160;
            int resizeHeight = (int)(bmp.Height * ((double)resizeWidth / (double)bmp.Width));

            Bitmap resizeBmp = new Bitmap(resizeWidth, resizeHeight);
            Graphics g = Graphics.FromImage(resizeBmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.DrawImage(bmp, 0, 0, resizeWidth, resizeHeight);
            g.Dispose();

            Graphics pg = pictureBox1.CreateGraphics();
            pg.DrawImage(resizeBmp, new Point(0, 0));
            resizeBmp.Save("1.jpg");
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            label1.Text = folderPath+"//"+ listBox1.SelectedItem.ToString();
            pictureBox1.ImageLocation = label1.Text;

        }
    }
}