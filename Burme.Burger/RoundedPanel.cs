using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedPanel : Panel
{
    public int CornerRadius { get; set; } = 20; // Köşe yarıçapı

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Grafik kenar yumuşatma modu
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        // Yuvarlak köşeli dikdörtgen
        Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
        GraphicsPath path = GetRoundedRectanglePath(rect, CornerRadius);

        // Çizim alanını sınırlama (Region ile)
        this.Region = new Region(path);

        // Arka plan rengi
        using (SolidBrush brush = new SolidBrush(this.BackColor))
        {
            e.Graphics.FillPath(brush, path);
        }

        // Kenarlık çizimi
        using (Pen pen = new Pen(this.ForeColor, 2))
        {
            e.Graphics.DrawPath(pen, path);
        }
    }

    private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();

        // Yuvarlak köşe çizimleri
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90); // Sol üst köşe
        path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90); // Sağ üst köşe
        path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90); // Sağ alt köşe
        path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90); // Sol alt köşe

        path.CloseFigure();
        return path;
    }
}

