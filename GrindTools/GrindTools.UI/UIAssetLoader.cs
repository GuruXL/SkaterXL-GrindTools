using System;
using System.IO;
using UnityEngine;

namespace GrindTools.UI
{
	public class UIAssetLoader
	{
		private static UIAssetLoader _instance;
		private PlaystationButton _psButtons;
		private XboxButton _xboxButtons;

		public static UIAssetLoader Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new UIAssetLoader();
				}
				return _instance;
			}
		}

		public PlaystationButton psButtons
		{
			get
			{
				if (_psButtons == null)
				{
					Texture2D texture2D = new Texture2D(100, 100);
					Texture2D texture2D2 = new Texture2D(100, 100);
					Texture2D texture2D3 = new Texture2D(100, 100);
					Texture2D texture2D4 = new Texture2D(100, 100);
					Texture2D texture2D5 = new Texture2D(100, 100);
					Texture2D texture2D6 = new Texture2D(100, 100);
					Texture2D texture2D7 = new Texture2D(100, 100);
					Texture2D texture2D8 = new Texture2D(100, 100);
					Texture2D texture2D9 = new Texture2D(100, 100);
					Texture2D texture2D10 = new Texture2D(100, 100);
					Texture2D texture2D11 = new Texture2D(100, 100);
					Texture2D texture2D12 = new Texture2D(100, 100);
					Texture2D texture2D13 = new Texture2D(100, 100);
					Texture2D texture2D14 = new Texture2D(100, 100);
					Texture2D texture2D15 = new Texture2D(100, 100);
					Texture2D texture2D16 = new Texture2D(100, 100);
					Texture2D texture2D17 = new Texture2D(100, 100);
					Texture2D texture2D18 = new Texture2D(100, 100);
					Texture2D texture2D19 = new Texture2D(100, 100);
					string format = Main.modEntry.Path + "ControllerSprites\\Playstation\\{0}.png";
					try
					{
						ImageConversion.LoadImage(texture2D, File.ReadAllBytes(string.Format(format, "Cross")));
						ImageConversion.LoadImage(texture2D2, File.ReadAllBytes(string.Format(format, "Triangle")));
						ImageConversion.LoadImage(texture2D3, File.ReadAllBytes(string.Format(format, "Circle")));
						ImageConversion.LoadImage(texture2D4, File.ReadAllBytes(string.Format(format, "Square")));
						ImageConversion.LoadImage(texture2D6, File.ReadAllBytes(string.Format(format, "Left_Stick_Click")));
						ImageConversion.LoadImage(texture2D5, File.ReadAllBytes(string.Format(format, "Left_Stick")));
						ImageConversion.LoadImage(texture2D8, File.ReadAllBytes(string.Format(format, "Right_Stick_Click")));
						ImageConversion.LoadImage(texture2D7, File.ReadAllBytes(string.Format(format, "Right_Stick")));
						ImageConversion.LoadImage(texture2D9, File.ReadAllBytes(string.Format(format, "Dpad_Right")));
						ImageConversion.LoadImage(texture2D10, File.ReadAllBytes(string.Format(format, "Dpad_Left")));
						ImageConversion.LoadImage(texture2D11, File.ReadAllBytes(string.Format(format, "Dpad_Up")));
						ImageConversion.LoadImage(texture2D12, File.ReadAllBytes(string.Format(format, "Dpad_Down")));
						ImageConversion.LoadImage(texture2D13, File.ReadAllBytes(string.Format(format, "Dpad")));
						ImageConversion.LoadImage(texture2D14, File.ReadAllBytes(string.Format(format, "L1")));
						ImageConversion.LoadImage(texture2D15, File.ReadAllBytes(string.Format(format, "L2")));
						ImageConversion.LoadImage(texture2D16, File.ReadAllBytes(string.Format(format, "R1")));
						ImageConversion.LoadImage(texture2D17, File.ReadAllBytes(string.Format(format, "R2")));
						ImageConversion.LoadImage(texture2D18, File.ReadAllBytes(string.Format(format, "Options")));
						ImageConversion.LoadImage(texture2D19, File.ReadAllBytes(string.Format(format, "Share")));
					}
					finally
					{
						_psButtons = new PlaystationButton(texture2D3, texture2D4, texture2D, texture2D2, texture2D6, texture2D9, texture2D11, texture2D10, texture2D12, texture2D13, texture2D14, texture2D15, texture2D16, texture2D17, texture2D5, texture2D7, texture2D8, texture2D18, texture2D19);
					}
				}
				return _psButtons;
			}
		}

		public XboxButton xboxButtons
		{
			get
			{
				if (_xboxButtons == null)
				{
					string format = Main.modEntry.Path + "ControllerSprites\\Xbox\\{0}.png";
					Texture2D texture2D = new Texture2D(100, 100);
					Texture2D texture2D2 = new Texture2D(100, 100);
					Texture2D texture2D3 = new Texture2D(100, 100);
					Texture2D texture2D4 = new Texture2D(100, 100);
					Texture2D texture2D5 = new Texture2D(100, 100);
					Texture2D texture2D6 = new Texture2D(100, 100);
					Texture2D texture2D7 = new Texture2D(100, 100);
					Texture2D texture2D8 = new Texture2D(100, 100);
					Texture2D texture2D9 = new Texture2D(100, 100);
					Texture2D texture2D10 = new Texture2D(100, 100);
					Texture2D texture2D11 = new Texture2D(100, 100);
					Texture2D texture2D12 = new Texture2D(100, 100);
					Texture2D texture2D13 = new Texture2D(100, 100);
					Texture2D texture2D14 = new Texture2D(100, 100);
					try
					{
						ImageConversion.LoadImage(texture2D, File.ReadAllBytes(string.Format(format, "A")));
						ImageConversion.LoadImage(texture2D2, File.ReadAllBytes(string.Format(format, "X")));
						ImageConversion.LoadImage(texture2D3, File.ReadAllBytes(string.Format(format, "B")));
						ImageConversion.LoadImage(texture2D4, File.ReadAllBytes(string.Format(format, "Y")));
						ImageConversion.LoadImage(texture2D5, File.ReadAllBytes(string.Format(format, "LB")));
						ImageConversion.LoadImage(texture2D6, File.ReadAllBytes(string.Format(format, "LT")));
						ImageConversion.LoadImage(texture2D7, File.ReadAllBytes(string.Format(format, "RB")));
						ImageConversion.LoadImage(texture2D8, File.ReadAllBytes(string.Format(format, "RT")));
						ImageConversion.LoadImage(texture2D9, File.ReadAllBytes(string.Format(format, "Menu")));
						ImageConversion.LoadImage(texture2D10, File.ReadAllBytes(string.Format(format, "View")));
						ImageConversion.LoadImage(texture2D11, File.ReadAllBytes(string.Format(format, "DpadUp")));
						ImageConversion.LoadImage(texture2D12, File.ReadAllBytes(string.Format(format, "DpadLeft")));
						ImageConversion.LoadImage(texture2D13, File.ReadAllBytes(string.Format(format, "DpadRight")));
						ImageConversion.LoadImage(texture2D14, File.ReadAllBytes(string.Format(format, "DpadDown")));
					}
					finally
					{
						_xboxButtons = new XboxButton(texture2D, texture2D2, texture2D3, texture2D4, texture2D5, texture2D6, texture2D7, texture2D8, texture2D9, texture2D10, texture2D11, texture2D13, texture2D12, texture2D14);
					}
				}
				return _xboxButtons;
			}
		}
	}
}
