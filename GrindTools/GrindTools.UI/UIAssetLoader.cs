using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace GrindTools.UI
{
	public class UIAssetLoader
	{
		public static UIAssetLoader __instance { get; private set; }
		public static UIAssetLoader Instance => __instance ?? (__instance = new UIAssetLoader());

		private PlaystationButtons _psButtons;
		private XboxButtons _xboxButtons;

		private static byte[] ExtractResources(string filename)
		{
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename))
			{
				if (manifestResourceStream == null)
					return null;

				byte[] buffer = new byte[manifestResourceStream.Length];
				manifestResourceStream.Read(buffer, 0, buffer.Length);
				return buffer;
			}
		}

        public PlaystationButtons psButtons
        {
            get
            {
                if (_psButtons == null)
                {
                    Texture2D icon = new Texture2D(100, 100);
                    Texture2D icon2 = new Texture2D(100, 100);
                    Texture2D icon3 = new Texture2D(100, 100);
                    Texture2D icon4 = new Texture2D(100, 100);
                    Texture2D icon5 = new Texture2D(100, 100);
                    Texture2D icon6 = new Texture2D(100, 100);
                    Texture2D icon7 = new Texture2D(100, 100);
                    Texture2D icon8 = new Texture2D(100, 100);
                    Texture2D icon9 = new Texture2D(100, 100);
                    Texture2D icon10 = new Texture2D(100, 100);
                    Texture2D icon11 = new Texture2D(100, 100);
                    Texture2D icon12 = new Texture2D(100, 100);
                    Texture2D icon13 = new Texture2D(100, 100);
                    Texture2D icon14 = new Texture2D(100, 100);
                    Texture2D icon15 = new Texture2D(100, 100);
                    Texture2D icon16 = new Texture2D(100, 100);
                    Texture2D icon17 = new Texture2D(100, 100);
                    Texture2D icon18 = new Texture2D(100, 100);
                    Texture2D icon19 = new Texture2D(100, 100);


                    // Extract Resources
                    byte[] iconData = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.Circle.png");
                    byte[] iconData2 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.Square.png");
                    byte[] iconData3 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.Cross.png");
                    byte[] iconData4 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.Triangle.png");
                    byte[] iconData5 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.Dpad_Right.png");
                    byte[] iconData6 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.Dpad_Left.png");
                    byte[] iconData7 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.Dpad_Up.png");
                    byte[] iconData8 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.Dpad_Down.png");
                    byte[] iconData9 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.Dpad.png");
                    byte[] iconData10 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.L1.png");
                    byte[] iconData11 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.L2.png");
                    byte[] iconData12 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.R1.png");
                    byte[] iconData13 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.R2.png");
                    byte[] iconData14 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.Left_Stick_Click.png");
                    byte[] iconData15 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.Left_Stick.png");
                    byte[] iconData16 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.Right_Stick.png");
                    byte[] iconData17 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.Right_Stick_Click.png"); 
                    byte[] iconData18 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.Options.png");
                    byte[] iconData19 = ExtractResources("GrindTools.Resources.ButtonIcons.Playstation.Share.png");

                    try
                    {
                        // Convert to Images
                        if (iconData != null) ImageConversion.LoadImage(icon, iconData);
                        if (iconData2 != null) ImageConversion.LoadImage(icon2, iconData2);
                        if (iconData3 != null) ImageConversion.LoadImage(icon3, iconData3);
                        if (iconData4 != null) ImageConversion.LoadImage(icon4, iconData4);
                        if (iconData5 != null) ImageConversion.LoadImage(icon5, iconData5);
                        if (iconData6 != null) ImageConversion.LoadImage(icon6, iconData6);
                        if (iconData7 != null) ImageConversion.LoadImage(icon7, iconData7);
                        if (iconData8 != null) ImageConversion.LoadImage(icon8, iconData8);
                        if (iconData9 != null) ImageConversion.LoadImage(icon9, iconData9);
                        if (iconData10 != null) ImageConversion.LoadImage(icon10, iconData10);
                        if (iconData11 != null) ImageConversion.LoadImage(icon11, iconData11);
                        if (iconData12 != null) ImageConversion.LoadImage(icon12, iconData12);
                        if (iconData13 != null) ImageConversion.LoadImage(icon13, iconData13);
                        if (iconData14 != null) ImageConversion.LoadImage(icon14, iconData14);
                        if (iconData15 != null) ImageConversion.LoadImage(icon15, iconData15);
                        if (iconData16 != null) ImageConversion.LoadImage(icon16, iconData16);
                        if (iconData17 != null) ImageConversion.LoadImage(icon17, iconData17);
                        if (iconData18 != null) ImageConversion.LoadImage(icon18, iconData18);
                        if (iconData19 != null) ImageConversion.LoadImage(icon19, iconData19);
                    }
                    catch (Exception ex)  // Catch any exception
                    {
                        Main.Logger.Log($"An error occurred while loading textures: {ex.Message}");
                    }
                    finally
                    {
                        _psButtons = new PlaystationButtons(icon, icon2, icon3, icon4, icon5, icon6, icon7, icon8, icon9, icon10, icon11, icon12, icon13, icon14, icon15, icon16, icon17, icon18, icon19);
                    }
                }
                return _psButtons;
            }
        }
        public XboxButtons xboxButtons
        {
            get
            {
                if (_xboxButtons == null)
                {
                    // Initialize all the textures
                    Texture2D icon = new Texture2D(100, 100);
                    Texture2D icon2 = new Texture2D(100, 100);
                    Texture2D icon3 = new Texture2D(100, 100);
                    Texture2D icon4 = new Texture2D(100, 100);
                    Texture2D icon5 = new Texture2D(100, 100);
                    Texture2D icon6 = new Texture2D(100, 100);
                    Texture2D icon7 = new Texture2D(100, 100);
                    Texture2D icon8 = new Texture2D(100, 100);
                    Texture2D icon9 = new Texture2D(100, 100);
                    Texture2D icon10 = new Texture2D(100, 100);
                    Texture2D icon11 = new Texture2D(100, 100);
                    Texture2D icon12 = new Texture2D(100, 100);
                    Texture2D icon13 = new Texture2D(100, 100);
                    Texture2D icon14 = new Texture2D(100, 100);
                    Texture2D icon15 = new Texture2D(100, 100);
                    Texture2D icon16 = new Texture2D(100, 100);
                    Texture2D icon17 = new Texture2D(100, 100);
                    Texture2D icon18 = new Texture2D(100, 100);



                    // Extract resources first
                    byte[] iconData = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.A.png");
                    byte[] iconData2 = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.X.png");
                    byte[] iconData3 = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.B.png");
                    byte[] iconData4 = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.Y.png");
                    byte[] iconData5 = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.LB.png");
                    byte[] iconData6 = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.LT.png");
                    byte[] iconData7 = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.RB.png");
                    byte[] iconData8 = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.RT.png");
                    byte[] iconData9 = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.Menu.png");
                    byte[] iconData10 = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.View.png");
                    byte[] iconData11 = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.DpadUp.png");
                    byte[] iconData12 = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.DpadLeft.png");
                    byte[] iconData13 = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.DpadRight.png");
                    byte[] iconData14 = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.DpadDown.png");
                    byte[] iconData15 = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.LeftStick.png");
                    byte[] iconData16 = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.LeftStickClick.png");
                    byte[] iconData17 = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.RightStick.png");
                    byte[] iconData18 = ExtractResources("GrindTools.Resources.ButtonIcons.Xbox.RightStickClick.png");

                    try
                    {
                        // Load images after resources have been extracted
                        if (iconData != null) ImageConversion.LoadImage(icon, iconData);
                        if (iconData2 != null) ImageConversion.LoadImage(icon2, iconData2);
                        if (iconData3 != null) ImageConversion.LoadImage(icon3, iconData3);
                        if (iconData4 != null) ImageConversion.LoadImage(icon4, iconData4);
                        if (iconData5 != null) ImageConversion.LoadImage(icon5, iconData5);
                        if (iconData6 != null) ImageConversion.LoadImage(icon6, iconData6);
                        if (iconData7 != null) ImageConversion.LoadImage(icon7, iconData7);
                        if (iconData8 != null) ImageConversion.LoadImage(icon8, iconData8);
                        if (iconData9 != null) ImageConversion.LoadImage(icon9, iconData9);
                        if (iconData10 != null) ImageConversion.LoadImage(icon10, iconData10);
                        if (iconData11 != null) ImageConversion.LoadImage(icon11, iconData11);
                        if (iconData12 != null) ImageConversion.LoadImage(icon12, iconData12);
                        if (iconData13 != null) ImageConversion.LoadImage(icon13, iconData13);
                        if (iconData14 != null) ImageConversion.LoadImage(icon14, iconData14);
                        if (iconData15 != null) ImageConversion.LoadImage(icon15, iconData15);
                        if (iconData16 != null) ImageConversion.LoadImage(icon16, iconData16);
                        if (iconData17 != null) ImageConversion.LoadImage(icon17, iconData17);
                        if (iconData18 != null) ImageConversion.LoadImage(icon18, iconData18);
                    }
                    catch (Exception ex)  // Catch any exception
                    {
                        Main.Logger.Log($"An error occurred while loading textures: {ex.Message}");
                    }
                    finally
                    {
                        _xboxButtons = new XboxButtons(icon, icon2, icon3, icon4, icon5, icon6, icon7, icon8, icon9, icon10, icon11, icon12, icon13, icon14, icon15, icon16, icon17, icon18);
                    }
                }
                return _xboxButtons;
            }
        }
    }
}
