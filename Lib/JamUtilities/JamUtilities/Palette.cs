using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JamUtilities
{
    public class Palette
    {
        public static Color color1 { get; private set;}
        public static Color color2 { get; private set; }
        public static Color color3 { get; private set; }
        public static Color color4 { get; private set; }
        public static Color color5 { get; private set; }

        public static void LoadPalette(string fileName)
        {
            StreamReader reader = File.OpenText(fileName);
            string line = "";
            bool foundRGB = false;
            int count = 1;
            while ((line = reader.ReadLine()) != null)
            {
                if (!foundRGB)
                {
                    if (line.Contains("RGB"))
                    {
                        foundRGB = true;
                        continue;
                    }
                }
                else
                {
                    
                    string[] items = line.Split('(');
                    string rgb = items[1];
                    rgb = rgb.Replace(");","");

                    string[] rgbvals = rgb.Split(',');
                    byte r = Byte.Parse(rgbvals[0]);
                    byte g = Byte.Parse(rgbvals[1]);
                    byte b = Byte.Parse(rgbvals[2]);

                    if (count == 1)
                        color1 = new Color(r, g, b, 255);
                    else if (count == 2)
                        color2 = new Color(r, g, b, 255);
                    else if (count == 3)
                        color3 = new Color(r, g, b, 255);
                    else if (count == 4)
                        color4 = new Color(r, g, b, 255);
                    else if (count == 5)
                        color5 = new Color(r, g, b, 255);
                    count++;
                }
            }


            System.Console.WriteLine(color1);
            System.Console.WriteLine(color2);
            System.Console.WriteLine(color3);
            System.Console.WriteLine(color4);
            System.Console.WriteLine(color5);
        }
    }
}
