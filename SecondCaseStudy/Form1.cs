using Newtonsoft.Json;
using SecondCaseStudy;
using System;
using System.Drawing;

namespace SecondCaseStudy
{
    public partial class form : Form
    {
        public form()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bitmap = new Bitmap(2560, 1440, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap bitmap2 = new Bitmap(2560, 1440, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Bitmap bitmap3 = new Bitmap(2560, 1440, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            Graphics graphics2 = Graphics.FromImage(bitmap2);
            Graphics graphics3 = Graphics.FromImage(bitmap3);

            Color red = Color.FromArgb(255, 255, 0, 0);
            Pen redPen = new Pen(red);
            redPen.Width = 1;

            Color blue = Color.FromArgb(255, 0, 0, 255);
            Pen bluePen = new Pen(blue);
            bluePen.Width = 5;

            var font = new Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Pixel);
            var brush = new SolidBrush(Color.Red);

            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog.FileName;

                try
                {
                    string text = File.ReadAllText(file);
                    List<ResponseModel> response = JsonConvert.DeserializeObject<List<ResponseModel>>(text);
                    List<Vertex> verticeList = new List<Vertex>();
                    List<Rectangle> rectangleList = new List<Rectangle>();

                    int minX = 500; int minY = 500;
                    int maxX = 0; int maxY = 0;

                    int counter = 0;

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
                            if (counter == 0)
                            {
                                x1 = vertex.x;
                                y1 = vertex.y;

                                counter++;
                            }

                            else if (counter == 1)
                            {
                                x2 = vertex.x;
                                y2 = vertex.y;

                                counter++;
                            }

                            else if (counter == 2)
                            {
                                x3 = vertex.x;
                                y3 = vertex.y;

                                counter++;
                            }

                            else if (counter == 3)
                            {
                                x4 = vertex.x;
                                y4 = vertex.y;

                                int finalMinx = Math.Min(Math.Min(x1, x2), Math.Min(x3, x4));
                                int finalMiny = Math.Min(Math.Min(y1, y2), Math.Min(y3, y4));

                                int finalMaxx = Math.Max(Math.Max(x1, x2), Math.Max(x3, x4));
                                int finalMaxy = Math.Max(Math.Max(y1, y2), Math.Max(y3, y4));

                                Rectangle rectangle = new Rectangle(finalMinx, finalMiny, (finalMaxx - finalMinx), (finalMaxy - finalMiny));

                                //https://learn.microsoft.com/tr-tr/dotnet/desktop/winforms/advanced/how-to-draw-wrapped-text-in-a-rectangle?view=netframeworkdesktop-4.8

                                graphics.DrawString(item.description, font, Brushes.Blue, rectangle);
                                graphics2.DrawRectangle(redPen, rectangle);

                                graphics3.DrawStringInside(rectangle, font, Brushes.Blue ,item.description);

                                verticeList.Add(vertex);

                                counter = 0;
                            }

                        }
                    }

                    Console.WriteLine("Cenk CAMKIRAN");

                    bitmap.Save(@"output.png");
                    bitmap2.Save(@"output2.png");
                    bitmap3.Save(@"output3.png");

                }
                catch (IOException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }
}