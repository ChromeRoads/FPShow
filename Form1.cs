namespace FPShow;


public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        Text = "Image Example";
        Width = 800;
        Height = 600;        
        InitializeUI();
        InitializeImage();
    }

    // Method to handle the button click
    private void OnButtonClick(object sender, EventArgs e)
    {
        FPShow.HDF5Dump newWindow = new HDF5Dump();
        newWindow.Show(); // Show the new window        
    }

    private void InitializeUI()
        {
            Button button = new Button
            {
                Text = "show HDR",
                Location = new System.Drawing.Point(5, 5)
            };
            // button.Click += (sender, e) => MessageBox.Show("Button clicked!");
            // Assign the method to the button click event
            button.Click += OnButtonClick;
            this.Controls.Add(button);
        }

    private void InitializeImage()
        {
            PictureBox pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            Controls.Add(pictureBox);
            Bitmap fingerprintImage = GenerateFingerprintBitmap();
            pictureBox.Image = fingerprintImage;            
        }            
        private byte[] LoadHexDataFromFile(string filePath)
        {
            try
            {
                // Read the file content
                string fileContent = System.IO.File.ReadAllText(filePath);

                // Split the content into individual hex values, removing any whitespace
                string[] hexStrings = fileContent.Split(',', StringSplitOptions.None);

                // Convert the hex strings into a byte array
                byte[] data = new byte[hexStrings.Length];
                for (int i = 0; i < hexStrings.Length; i++)
                {
                    // Trim whitespace and convert each hex string to a byte
                    data[i] = Convert.ToByte(hexStrings[i].Trim(), 16);
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading hex data from file: {ex.Message}");
                throw;
            }
        }        

        private Bitmap GenerateFingerprintBitmap()
        {
            // Sample hex data (replace this with your actual data)
            byte[] hexData = LoadHexDataFromFile("fphex.txt");

            // Image dimensions
            int width = 160;
            int height = 160;

            // Ensure data length matches the expected dimensions
            if (hexData.Length != width * height)
            {
                throw new Exception("Data length does not match expected 160x160 dimensions.");
            }

            // Create a bitmap to hold the grayscale image
            Bitmap bitmap = new Bitmap(width, height);

            // Fill the bitmap with pixel values
            int index = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Get the grayscale value from the data
                    byte intensity = hexData[index++];
                    // Create a grayscale color
                    Color color = Color.FromArgb(intensity, intensity, intensity);
                    // Set the pixel in the bitmap
                    bitmap.SetPixel(x, y, color);
                }
            }

            return bitmap;
        }
}
