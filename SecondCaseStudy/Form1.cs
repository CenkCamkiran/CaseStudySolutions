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
            Bitmap bitmap = new Bitmap(1920, 1080, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            Color red = Color.FromArgb(255, 255, 0, 0);
            Pen redPen = new Pen(red);
            redPen.Width = 5;

            Color blue = Color.FromArgb(255, 0, 0, 255);
            Pen bluePen = new Pen(blue);
            bluePen.Width = 5;

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

                    foreach (ResponseModel item in response)
                    {
                        foreach (Vertex vertex in item.boundingPoly.vertices)
                        {
                            verticeList.Add(vertex);
                        }
                    }

                    foreach (Vertex vertex in verticeList)
                    {
                        if (vertex.x > maxX)
                            maxX = vertex.x;

                        if (vertex.x < minX)
                            minX = vertex.x;

                        if (vertex.y > maxY)
                            maxY = vertex.y;

                        if (vertex.y < minY)
                            minY = vertex.y;
                    }

                    Rectangle rectangle = new Rectangle(minX, minY, (maxX - minX), (maxY - minY));
                    int counter = 0;

                    int x1 = 0;
                    int x2 = 0;
                    int x3 = 0;
                    int x4 = 0;
                    int y1 = 0;
                    int y2 = 0;
                    int y3 = 0;
                    int y4 = 0;

                    foreach (Vertex vertex in verticeList)
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

                        else if(counter == 2)
                        {
                            x3 = vertex.x;
                            y3 = vertex.y;

                            counter++;
                        }

                        else if(counter == 3)
                        {
                            x4 = vertex.x;
                            y4 = vertex.y;

                            int finalMinx = Math.Min(Math.Min(x1, x2), Math.Min(x3, x4));
                            int finalMiny = Math.Min(Math.Min(y1, y2), Math.Min(y3, y4));

                            int finalMaxx = Math.Max(Math.Max(x1, x2), Math.Max(x3, x4));
                            int finalMaxy = Math.Max(Math.Max(y1, y2), Math.Max(y3, y4));

                            rectangleList.Add(new Rectangle(finalMinx, finalMiny, (finalMaxx - finalMinx), (finalMaxy - finalMiny)));

                            counter = 0;
                        }

                    }

                    Console.WriteLine("Cenk CAMKIRAN");
                    graphics.DrawRectangle(redPen, rectangle);

                    foreach(Rectangle item in rectangleList)
                    {
                        graphics.DrawRectangle(bluePen, item);

                        var font = new Font(FontFamily.GenericSerif, 40f, FontStyle.Bold);
                        var brush = new SolidBrush(Color.Red);

                        graphics.DrawString("Welcome to Bitmap!", font, brush, 10, 20);
                    }

                    bitmap.Save(@"output.png");

                }
                catch (IOException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

    }
}