using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Drawing.Drawing2D;

namespace wolfensteinTest
{
    public partial class Form1 : Form
    {
        Image image1 = Image.FromFile(@"C:\Users\Skaral\Desktop\wall.jpg", true);
        float pos_x = 0;
        float pos_y = 0;
        float p2d_half_w;
        float p2d_half_h;
        float p3d_half_w;
        float p3d_half_h;
        List<Line> wulfWalls;
        float fov = 70;
        float fov_vertical = 70;
        const float viewRange = 2500f;
        double rad = 0;
        double Lrad;
        double Rrad;
        float WallHeight;
        const float wmult = 10; // 2d view multiplikator
        List<PointF> toDraw = new List<PointF>();

        Line Lfovl;
        Line Rfovl;

        public Form1()
        {
            InitializeComponent();

            // Form double buffer.
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            // Precalculating frequently used values.
            this.p2d_half_w = this.pictureBox_2D.Width / 2;
            this.p2d_half_h = this.pictureBox_2D.Height / 2;
            this.p3d_half_w = this.pictureBox_3D.Width / 2;
            this.p3d_half_h = this.pictureBox_3D.Height / 2;

            // Initializing WoflWalls.
            wulfWalls = new List<Line>();
            wulfWalls.Add(new Line(15, 7, -8, 28));
            this.WallHeight = 10;

            // Initializing FOV lines
            this.Lfovl = new Line(pos_x, pos_y, 
                (float)Math.Cos(rad - dgrToRad(fov / 2)) * viewRange + pos_x, 
                (float)Math.Sin(rad - dgrToRad(fov / 2)) * viewRange + pos_y);
            this.Rfovl = new Line(pos_x, pos_y, 
                (float)Math.Cos(rad + dgrToRad(fov / 2)) * viewRange + pos_x, 
                (float)Math.Sin(rad + dgrToRad(fov / 2)) * viewRange + pos_y);


            this.Lrad = rad - dgrToRad(fov / 2);
            this.Rrad = rad + dgrToRad(fov / 2);
            this.Lrad -= this.Lrad >= this.Rrad ? Math.PI * 2 : 0;

            // Starting a refresh timer.
            this.timerWulf.Start();
        }
        struct D3
        {
            public PointF P;
            public double D3x;
            public double D3y;
        };
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Into the middle translation
            g.TranslateTransform(this.p3d_half_w, this.p3d_half_h);
            
            //Floor, Sky
            g.FillRectangle(Brushes.LightGreen, -this.p3d_half_w, 0f, this.p3d_half_w * 2, this.p3d_half_h);
            g.FillRectangle(Brushes.SkyBlue, -this.p3d_half_w, -this.p3d_half_h, this.p3d_half_w * 2, this.p3d_half_h);

            // Setting up a brush with linear gradient
            // ---- TODO: Move to initialization.
            /* LinearGradientBrush linGrBrush = new LinearGradientBrush(
                new PointF((float)D3x, 0),
                new PointF(this.p3d_half_w, 0),
                Color.FromArgb(255, 255, 0, 0),   // Opaque red
                Color.FromArgb(255, 0, 0, 255));  // Opaque blue
             /*
             // Texture brush experiments.
             TextureBrush texture = new TextureBrush(image1);
             texture.WrapMode = WrapMode.Tile;*/
            List<D3> l = new List<D3>();
            foreach (PointF item in this.toDraw)
            {
                D3 s = new D3();
                s.P = item;
                double dist = Line.getLenght(item, new PointF(pos_x, pos_y));
                double cos = (item.X - pos_x) / dist;
                double Arad = Math.Acos(cos);
                if (item.Y - pos_y < 0)
                    Arad = 2 * Math.PI - Arad;

                s.D3x = (Arad - Lrad) / dgrToRad(fov);

                /*double tan = WallHeight / dist;
                double Brad = Math.Atan(tan);
                s.D3y = Brad * 2 / dgrToRad(fov_vertical);*/

                cos = dist / Math.Sqrt(Math.Pow(WallHeight, 2) + Math.Pow(dist, 2));
                Arad = Math.Acos(cos);
                s.D3y = Arad / dgrToRad(fov_vertical);

                l.Add(s);
            }

            l.Sort( (x, y) => x.D3x.CompareTo(y.D3y) );
            

            for (int i = 0; i < l.Count - 1; i++)
            {
                g.FillPolygon(Brushes.Red, new PointF[] {
                                                        new PointF((float)l[i].D3x   * this.pictureBox_3D.Width - this.p3d_half_w, (float)l[i].D3y     * this.p3d_half_h),
                                                        new PointF((float)l[i+1].D3x * this.pictureBox_3D.Width - this.p3d_half_w, (float)l[i+1].D3y   * this.p3d_half_h),
                                                        new PointF((float)l[i+1].D3x * this.pictureBox_3D.Width - this.p3d_half_w, (float)-l[i+1].D3y   *   this.p3d_half_h),
                                                        new PointF((float)l[i].D3x   * this.pictureBox_3D.Width - this.p3d_half_w, (float)-l[i].D3y   * this.p3d_half_h)
                                                    });

                /*
                 * 
                 * 
                 * 
                 * 
                 * 
                 * */
            }

            //Drawing the wall.

        }

        private void pictureBox_2D_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            this.toDraw.Clear();

            g.TranslateTransform(this.p2d_half_w, this.p2d_half_h);
            g.FillEllipse(Brushes.Red, pos_x - 2.5f, pos_y - 2.5f, 5, 5);
            
            foreach (Line item in wulfWalls)
            {   
                item.Draw2D(g);
            }

            this.label_1.Text = "LRAD: " + Math.Round(Lrad, 2);
            this.label_WierdAngle.Text = "RRAD: " + Math.Round(Rrad,2);
            
            g.DrawLine(Pens.DarkRed, Lfovl.A, Lfovl.B);
            g.DrawLine(Pens.DarkBlue, Rfovl.A, Rfovl.B);

            // Calculating the intersections and checking whether it's still on the line.
            PointF P1 = Line.getIntersection(wulfWalls[0], Lfovl);

            Vector2 s1 = Line.getVector(wulfWalls[0].A, P1);
            Vector2 s2 = Line.getVector(wulfWalls[0].A, wulfWalls[0].B);
            Vector2 s3 = s1 / s2;
            Vector2 s4 = Line.getVector(Lfovl.A, P1);
            Vector2 s5 = Line.getVector(Lfovl.A, Lfovl.B);
            Vector2 s6 = s4 / s5;
            if (s3.X >= 0 && s3.X <= 1 && s6.X >= 0 && s6.X <= 1)
            {
                this.toDraw.Add(P1);
                g.FillEllipse(Brushes.Pink, P1.X * wmult - 2.5f, P1.Y * wmult - 2.5f, 5, 5);
            }
            else
            {

            }
                

            PointF P2 = Line.getIntersection(wulfWalls[0], Rfovl);

            s1 = Line.getVector(wulfWalls[0].A, P2);
            s2 = Line.getVector(wulfWalls[0].A, wulfWalls[0].B);
            s3 = s1 / s2;
            s4 = Line.getVector(Rfovl.A, P2);
            s5 = Line.getVector(Rfovl.A, Rfovl.B);
            s6 = s4 / s5;
            if (s3.X >= 0 && s3.X <= 1 && s6.X >= 0 && s6.X <= 1)
            {
                this.toDraw.Add(P2);
                g.FillEllipse(Brushes.Pink, P2.X * wmult - 2.5f, P2.Y * wmult - 2.5f, 5, 5);
            }
            else
            {

            }
            // (X Y) (X Y) -> [X Y] -> P(X Y) -> S[X Y]-> S < 0 -> [S*X S*Y] -> [S*X / X S*Y / Y] -> [S S] -> S > 1

            double cos = (wulfWalls[0].A.X - pos_x) / Line.getLenght(wulfWalls[0].A, new PointF(pos_x, pos_y));
            double Arad = Math.Acos(cos);
            if (wulfWalls[0].A.Y - pos_y < 0)
                Arad = 2*Math.PI - Arad;

            if (Arad >= Lrad && Arad <= Rrad)
            {
                this.toDraw.Add(wulfWalls[0].A);
                g.FillEllipse(Brushes.Blue, wulfWalls[0].A.X* wmult - 2.5f, wulfWalls[0].A.Y* wmult - 2.5f, 5, 5);
            }

            cos = (wulfWalls[0].B.X - pos_x) / Line.getLenght(wulfWalls[0].B, new PointF(pos_x, pos_y));
            Arad = Math.Acos(cos);
            if (wulfWalls[0].B.Y - pos_y < 0)
                Arad = 2 * Math.PI - Arad;

            if (Arad >= Lrad && Arad <= Rrad)
            {
                this.toDraw.Add(wulfWalls[0].B);
                g.FillEllipse(Brushes.Yellow, wulfWalls[0].B.X*wmult - 2.5f, wulfWalls[0].B.Y* wmult - 2.5f, 5, 5);
            }



        }

        private void timerWulf_Tick(object sender, EventArgs e)
        {
            this.pictureBox_2D.Invalidate();
            this.pictureBox_3D.Invalidate();
            rad += (Math.PI / 1200);
            rad = rad % (2 * Math.PI);
            Lrad += (Math.PI / 1200);
            Lrad = Lrad % (2 * Math.PI);
            Rrad += (Math.PI / 1200);
            Rrad = Rrad % (2 * Math.PI);
            Lfovl.B.X = (float)Math.Cos(Lrad) * viewRange;
            Lfovl.B.Y = (float)Math.Sin(Lrad) * viewRange;
            Rfovl.B.X = (float)Math.Cos(Rrad) * viewRange;
            Rfovl.B.Y = (float)Math.Sin(Rrad) * viewRange;
            Lfovl.setLineEquation();
            Rfovl.setLineEquation();

            this.Lrad -= this.Lrad >= this.Rrad ? Math.PI * 2 : 0;
        }


        private double dgrToRad(float dgr) {
            return ((dgr / 180) * Math.PI) % (2*Math.PI);
        }

       

    }
    public class Line
    {
        public PointF A;
        public PointF B;
        private float tangen;
        private float offset;

        public float Tangen { get => tangen; private set => tangen = value; }
        public float Offset { get => offset; private set => offset = value; }

        public Line(float ax, float ay, float bx, float by)
        {
            this.A = new PointF(ax, ay);
            this.B = new PointF(bx, by);
            setLineEquation();
        }
        public Line(PointF A, PointF B)
        {
            this.Move(A, B);
        }

        public void Draw2D(Graphics g) {
            g.DrawLine(Pens.Blue, A.X * 10, A.Y * 10, B.X * 10, B.Y * 10);
        }
        public void Move(PointF A, PointF B)
        {
            this.A = A;
            this.B = B;
            setLineEquation();
        }
        public void Move(float ax, float ay, float bx, float by)
        {
            this.Move(new PointF(ax, ay), new PointF(bx, by));
        }
        public void setLineEquation()
        {
            getEquation(this.A, this.B, this);
        }

        
        // Static extensions
        public static double getLenght(PointF A, PointF B)
        {
            float x = A.X - B.X;
            float y = A.Y - B.Y;

            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        public static Vector2 getVector(Line w)
        {
            return getVector(w.A, w.B);
        }

        public static Vector2 getVector(PointF A, PointF B)
        {
            Vector2 lineVector = new Vector2((float)(B.X - A.X), (float)(B.Y - A.Y));

            return lineVector;
        }
        public static float getGradient(Vector2 v)
        {
            if (v.X == 0)
                return 0;
            return v.Y / v.X;
        }

        public static float getOffset(PointF A, float tan)
        {
            return A.Y - tan * A.X;
        }

        public static Line getLineEquation(float ax, float ay, float bx, float by)
        {
            return getEquation(new PointF(ax, ay), new PointF(bx, by));
        }
        public static Line getEquation(PointF A, PointF B, Line L = null)
        {
            Line l = L == null ? new Line(A, B) : L;
            l.Tangen = getGradient(getVector(A, B));
            l.Offset = getOffset(A, l.Tangen);
            return l;
        }

        public static PointF getIntersection(Line first, Line second)
        {
            float a = first.Tangen;
            float b = second.Tangen;
            float c = first.Offset;
            float d = second.Offset;
            PointF P = new PointF((d - c) / (a - b), ((a * d) - (b * c)) / (a - b));
            return P;
        }
    }
}
