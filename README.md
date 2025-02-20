# FPShow - Fingerprint Display Utility

## Overview

**FPShow** is a **.NET 8 Windows Forms Application** designed to display **fingerprint images** captured from the Chrome Roads Card.  
It reads a **fingerprint hex dump from a debug log** and converts it into an image for visualization on a Windows machine.

🚀 **This utility is intended to work alongside the Chrome Roads Card fingerprint sample.** 🚀

### **How It Works**
1. The **Chrome Roads Card** captures fingerprint data and outputs it as a **hex dump** to the debug console.
2. The user **copies and pastes the hex dump** into a text file (`fphex.txt`).
3. **FPShow reads `fphex.txt`**, reconstructs the image, and displays it in a Windows Forms UI.

---

## Features

- Reads **160x160 grayscale fingerprint data** from a hex dump.
- Converts **hex values into an image**.
- Provides a **simple UI** for viewing fingerprints.
- Uses **Windows Forms (.NET 8)** for an easy-to-use interface.

---

## File Structure

- **`Form1.cs`** – Main application UI, loads and displays fingerprint image.
- **`HDF5Dump.cs`** – A placeholder form (not used in the main workflow).
- **`Program.cs`** – Application entry point.

---

## How to Use

### **1. Capture a Fingerprint Image**
On the **Chrome Roads Card**, run the fingerprint sample and **set a breakpoint** in `sf108.c` after:

```c
printk("post readimage status: %x\n", rd);
```

This occurs **after the fingerprint image is loaded into a buffer**.

### **2. Copy the Hex Dump**
- Open an RTT debug viewer (such as Segger RTT Viewer) and **connect to the card**.
- Locate the **hex dump output**, which looks like this:

```
00> 0x7c,0x4f,0x5a,0x52,0x5e,0x82,0xa2,0x85,0x90,0x6f,0x6c,0x59,0x47,0x33,...
```

- **Copy the hex values**, ensuring:
  - No leading `00>` characters.
  - 160 rows of **comma-separated hex values**.
  - The **last line has no trailing comma**.

### **3. Save to `fphex.txt`**
- Paste the hex dump into a new file named `fphex.txt`.
- Remove unnecessary formatting, keeping only the **160x160** grayscale values.

### **4. Run FPShow**
- Open FPShow (`FPShow.exe`).
- The fingerprint image should be displayed! 🎉

---

## Example Hex Dump Format

A valid `fphex.txt` should contain **exactly 160 lines**, similar to:

```
0x7c,0x4f,0x5a,0x52,0x5e,0x82,0xa2,0x85,0x90,...
0x3f,0x2b,0x13,0x1e,0x3c,0x39,0x2b,0x36,0x30,...
...
```

---

## Running the Application

1. **Compile FPShow** using .NET 8:
   ```sh
   dotnet build
   ```
2. **Run the executable**:
   ```sh
   ./bin/Debug/net8.0-windows/FPShow.exe
   ```
3. **Ensure `fphex.txt` is in the same directory as the EXE.**

---

## Example Code (Loading Fingerprint Image)

The fingerprint image is reconstructed in `Form1.cs`:

```c
private Bitmap GenerateFingerprintBitmap()
{
    byte[] hexData = LoadHexDataFromFile("fphex.txt");
    int width = 160, height = 160;

    if (hexData.Length != width * height) {
        throw new Exception("Data length does not match expected 160x160 dimensions.");
    }

    Bitmap bitmap = new Bitmap(width, height);
    int index = 0;
    for (int y = 0; y < height; y++) {
        for (int x = 0; x < width; x++) {
            byte intensity = hexData[index++];
            Color color = Color.FromArgb(intensity, intensity, intensity);
            bitmap.SetPixel(x, y, color);
        }
    }
    return bitmap;
}
```

---

## Troubleshooting

### **1. Image Not Displaying**
- Ensure `fphex.txt` exists in the same directory as `FPShow.exe`.
- Confirm the hex data is **160x160 pixels (25,600 values)**.
- Check for **extra commas or missing values**.

### **2. Errors Loading File**
- Ensure `fphex.txt` contains **only raw hex values**, no debug headers.
- Ensure hex values are formatted **as comma-separated integers**.

### **3. Compiling Issues**
- Ensure you have **.NET 8 SDK** installed:
  ```sh
  dotnet --version
  ```

---

## License

© 2024 Chrome Roads, Inc. All rights reserved.
