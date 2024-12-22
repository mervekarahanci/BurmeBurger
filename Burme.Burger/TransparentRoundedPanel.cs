using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class TransparentRoundedPanel : Panel
{
    public int CornerRadius { get; set; } = 50; // Köşe yarıçapı
    public int Alpha { get; set; } = 255; // Şeffaflık seviyesi (0-255)

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Kenar yumuşatma
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        // Yuvarlak köşeli dikdörtgen
        Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
        GraphicsPath path = GetRoundedRectanglePath(rect, CornerRadius);

        // Panelin sadece yuvarlak kısmını görünür yap
        this.Region = new Region(path);

        // Şeffaf arka plan rengi
        using (SolidBrush brush = new SolidBrush(Color.FromArgb(Alpha, this.BackColor)))
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

        // Yuvarlak köşeler
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
        path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
        path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
        path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);

        path.CloseFigure();
        return path;
    }
}

