using Newtonsoft.Json;
using SecondCaseStudy;
using System;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace SecondCaseStudy
{
    public partial class form : Form
    {

        public form()
        {
            InitializeComponent();
        }

        private void pickFileBtn_Click(object sender, EventArgs e)
        {
            int offSet = 15;

            Bitmap layoutBitmap = new Bitmap(2560, 1440, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap resultBitmap = new Bitmap(2560, 1440, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            Graphics layoutGraphics = Graphics.FromImage(layoutBitmap);
            Graphics resultGraphics = Graphics.FromImage(resultBitmap);

            //Kırmızı renk
            Color red = Color.FromArgb(255, 255, 0, 0);
            Pen redPen = new Pen(red);
            redPen.Width = 1;

            //Mavi renk
            Color blue = Color.FromArgb(255, 0, 0, 255);
            Pen bluePen = new Pen(blue);
            bluePen.Width = 5;

            //Yazılar için font
            var font = new Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Pixel);

            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog.FileName;

                try
                {
                    //response.json dosyasını oku.
                    string text = File.ReadAllText(file);

                    //response.json dosyasını Deserialize et.
                    List<ResponseModel> response = JsonConvert.DeserializeObject<List<ResponseModel>>(text);
                    Dictionary<Rectangle, string> items = new Dictionary<Rectangle, string>();

                    int counter = 0;

                    //dikdörtgen köşe koordinatları
                    int x1 = 0;
                    int x2 = 0;
                    int x3 = 0;
                    int x4 = 0;
                    int y1 = 0;
                    int y2 = 0;
                    int y3 = 0;
                    int y4 = 0;

                    foreach (ResponseModel item in response)
                    {
                        foreach (Vertex vertex in item.boundingPoly.vertices)
                        {
                            if (counter == 0) // dikdörtgen sol alt köşe noktası
                            {
                                x1 = vertex.x;
                                y1 = vertex.y;

                                counter++;
                            }

                            else if (counter == 1) // dikdörtgen sağ alt köşe noktası
                            {
                                x2 = vertex.x;
                                y2 = vertex.y;

                                counter++;
                            }

                            else if (counter == 2) // dikdörtgen sağ üst köşe noktası
                            {
                                x3 = vertex.x;
                                y3 = vertex.y;

                                counter++;
                            }

                            else if (counter == 3) // dikdörtgen sol üst köşe noktası
                            {
                                x4 = vertex.x;
                                y4 = vertex.y;

                                int finalMinx = Math.Min(Math.Min(x1, x2), Math.Min(x3, x4));
                                int finalMiny = Math.Min(Math.Min(y1, y2), Math.Min(y3, y4));

                                int finalMaxx = Math.Max(Math.Max(x1, x2), Math.Max(x3, x4));
                                int finalMaxy = Math.Max(Math.Max(y1, y2), Math.Max(y3, y4));

                                //yazının olduğu dikdörtgeni belirle
                                Rectangle rectangle = new Rectangle(finalMinx, finalMiny, (finalMaxx - finalMinx), (finalMaxy - finalMiny));

                                //yazının olduğu dikdörtgeni çiz
                                layoutGraphics.DrawRectangle(redPen, rectangle);

                                //yazıyı dikdörtgenin içinde yaz
                                resultGraphics.DrawStringInside(rectangle, font, Brushes.Blue, item.description);

                                items.Add(rectangle, item.description);

                                counter = 0;
                            }

                        }
                    }

                    //herşeyi resim olarak kaydet.
                    layoutBitmap.Save(@"layout.png");
                    resultBitmap.Save(@"result.png");

                    string message = "layout.png ve result.png oluşturulmuştur.";
                    string title = "Durum";
                    MessageBox.Show(message, title);

                }
                catch (IOException exception)
                {
                    //Hata mesajı yazdır
                    Console.WriteLine(exception.Message);

                    string message = exception.Message.ToString();
                    string title = "HATA";
                    MessageBox.Show(message, title);
                }
            }
        }
    }
}