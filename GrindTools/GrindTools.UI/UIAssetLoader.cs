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
					string format = Main.modEntry.Path + "ButtonIcons\\Playstation\\{0}.png";
					try
					{
						ImageConversion.LoadImage(icon, File.ReadAllBytes(string.Format(format, "Cross")));
						ImageConversion.LoadImage(icon2, File.ReadAllBytes(string.Format(format, "Triangle")));
						ImageConversion.LoadImage(icon3, File.ReadAllBytes(string.Format(format, "Circle")));
						ImageConversion.LoadImage(icon4, File.ReadAllBytes(string.Format(format, "Square")));
						ImageConversion.LoadImage(icon6, File.ReadAllBytes(string.Format(format, "Left_Stick_Click")));
						ImageConversion.LoadImage(icon5, File.ReadAllBytes(string.Format(format, "Left_Stick")));
						ImageConversion.LoadImage(icon8, File.ReadAllBytes(string.Format(format, "Right_Stick_Click")));
						ImageConversion.LoadImage(icon7, File.ReadAllBytes(string.Format(format, "Right_Stick")));
						ImageConversion.LoadImage(icon9, File.ReadAllBytes(string.Format(format, "Dpad_Right")));
						ImageConversion.LoadImage(icon10, File.ReadAllBytes(string.Format(format, "Dpad_Left")));
						ImageConversion.LoadImage(icon11, File.ReadAllBytes(string.Format(format, "Dpad_Up")));
						ImageConversion.LoadImage(icon12, File.ReadAllBytes(string.Format(format, "Dpad_Down")));
						ImageConversion.LoadImage(icon13, File.ReadAllBytes(string.Format(format, "Dpad")));
						ImageConversion.LoadImage(icon14, File.ReadAllBytes(string.Format(format, "L1")));
						ImageConversion.LoadImage(icon15, File.ReadAllBytes(string.Format(format, "L2")));
						ImageConversion.LoadImage(icon16, File.ReadAllBytes(string.Format(format, "R1")));
						ImageConversion.LoadImage(icon17, File.ReadAllBytes(string.Format(format, "R2")));
						ImageConversion.LoadImage(icon18, File.ReadAllBytes(string.Format(format, "Options")));
						ImageConversion.LoadImage(icon19, File.ReadAllBytes(string.Format(format, "Share")));
					}
					finally
					{
						_psButtons = new PlaystationButtons(icon3, icon4, icon, icon2, icon6, icon9, icon11, icon10, icon12, icon13, icon14, icon15, icon16, icon17, icon5, icon7, icon8, icon18, icon19);
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
					string format = Main.modEntry.Path + "ButtonIcons\\Xbox\\{0}.png";
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
					try
					{
						ImageConversion.LoadImage(icon, File.ReadAllBytes(string.Format(format, "A")));
						ImageConversion.LoadImage(icon2, File.ReadAllBytes(string.Format(format, "X")));
						ImageConversion.LoadImage(icon3, File.ReadAllBytes(string.Format(format, "B")));
						ImageConversion.LoadImage(icon4, File.ReadAllBytes(string.Format(format, "Y")));
						ImageConversion.LoadImage(icon5, File.ReadAllBytes(string.Format(format, "LB")));
						ImageConversion.LoadImage(icon6, File.ReadAllBytes(string.Format(format, "LT")));
						ImageConversion.LoadImage(icon7, File.ReadAllBytes(string.Format(format, "RB")));
						ImageConversion.LoadImage(icon8, File.ReadAllBytes(string.Format(format, "RT")));
						ImageConversion.LoadImage(icon9, File.ReadAllBytes(string.Format(format, "Menu")));
						ImageConversion.LoadImage(icon10, File.ReadAllBytes(string.Format(format, "View")));
						ImageConversion.LoadImage(icon11, File.ReadAllBytes(string.Format(format, "DpadUp")));
						ImageConversion.LoadImage(icon12, File.ReadAllBytes(string.Format(format, "DpadLeft")));
						ImageConversion.LoadImage(icon13, File.ReadAllBytes(string.Format(format, "DpadRight")));
						ImageConversion.LoadImage(icon14, File.ReadAllBytes(string.Format(format, "DpadDown")));
					}
					finally
					{
						_xboxButtons = new XboxButtons(icon, icon2, icon3, icon4, icon5, icon6, icon7, icon8, icon9, icon10, icon11, icon13, icon12, icon14);
					}
				}
				return _xboxButtons;
			}
		}
	}
}
