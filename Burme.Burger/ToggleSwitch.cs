using System;
using System.Drawing;
using System.Windows.Forms;

namespace Burme.Burger
{
    public class ToggleSwitch : Control
    {
        private bool isToggled = false; // Toggle durumu
        private Color onColor = Color.LimeGreen;
        private Color offColor = Color.LightGray;

        private Button toggleButton;

        public event EventHandler Toggled;

        public ToggleSwitch()
        {
            // Toggle Button'ı oluşturmayı en önce yapıyoruz
            toggleButton = new Button
            {
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(Height - 4, Height - 4),
                Location = new Point(2, 2)
            };
            toggleButton.FlatAppearance.BorderSize = 0;
            toggleButton.Click += (s, e) => Toggle();

            // Kontrol eklemeden önce temel özellikleri ayarlayın
            Controls.Add(toggleButton);
            this.BackColor = offColor;
            this.Size = new Size(60, 30); // Default boyut
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (toggleButton != null) // Null kontrolü
            {
                toggleButton.Size = new Size(Height - 4, Height - 4);
                UpdateSwitch();
            }
        }

        private void Toggle()
        {
            isToggled = !isToggled; // Durumu değiştir
            UpdateSwitch(); // Renk ve konumu güncelle
            Toggled?.Invoke(this, EventArgs.Empty); // Olayı tetikle
        }

        private void UpdateSwitch()
        {
            if (toggleButton == null) return; // Güvenlik kontrolü
            this.BackColor = isToggled ? onColor : offColor;
            toggleButton.Location = isToggled
                ? new Point(Width - toggleButton.Width - 2, 2)
                : new Point(2, 2);
        }
    }
}
