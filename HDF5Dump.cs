namespace FPShow;

using System.Windows.Forms;
using System.Drawing; // Ensure you can set sizes, points


public class HDF5Dump : Form
{
    public HDF5Dump()
    {
        // Configure the new form/window
        Text = "New Window";
        Size = new System.Drawing.Size(300, 200);

        Label label = new Label
        {
            Text = "Hello! This is a new window.",
            AutoSize = true,
            Location = new System.Drawing.Point(50, 50)
        };

        Controls.Add(label);
    }
}