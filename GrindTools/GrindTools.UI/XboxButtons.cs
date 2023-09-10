using System;
using System.Collections.Generic;
using UnityEngine;

namespace GrindTools.UI
{
	public class XboxButtons
	{
		public Texture2D A;
		public Texture2D X;
		public Texture2D B;
		public Texture2D Y;
		public Texture2D LB;
		public Texture2D LT;
		public Texture2D RB;
		public Texture2D RT;
		public Texture2D Menu;
		public Texture2D View;
		public Texture2D DpadUp;
		public Texture2D DpadRight;
		public Texture2D DpadLeft;
		public Texture2D DpadDown;
		public Texture2D LeftStickClick;
		public Texture2D LeftStick;
		public Texture2D RightStick;
		public Texture2D RightStickClick;

		public XboxButtons(Texture2D a, Texture2D x, Texture2D b, Texture2D y, Texture2D lb, Texture2D lt, Texture2D rb, Texture2D rt, Texture2D menu, Texture2D view, Texture2D dpadUp, Texture2D dpadRight, Texture2D dpadLeft, Texture2D dpadDown, Texture2D leftStickClick, Texture2D leftStick, Texture2D rightStick, Texture2D rightStickClick)
		{
			A = a;
			X = x;
			B = b;
			Y = y;
			LB = lb;
			LT = lt;
			RB = rb;
			RT = rt;
			Menu = menu;
			View = view;
			DpadUp = dpadUp;
			DpadRight = dpadRight;
			DpadLeft = dpadLeft;
			DpadDown = dpadDown;
			LeftStickClick = leftStickClick;
			LeftStick = leftStick;
			RightStick = rightStick;
			RightStickClick = rightStickClick;

		}

		public static Texture2D SwapToPlaystationUI(Texture2D tex, bool isPS4)
		{
			if (isPS4)
			{
				return UIAssetLoader.Instance.xboxButtons.SwapToPS4UI()[tex];
			}
			return tex;
		}

		public Dictionary<Texture2D, Texture2D> SwapToPS4UI()
		{
			PlaystationButtons psButtons = UIAssetLoader.Instance.psButtons;
			return new Dictionary<Texture2D, Texture2D>
			{
				{ B, psButtons.Circle },
				{ X, psButtons.Square },
				{ A, psButtons.Cross },
				{ Y, psButtons.Triangle },
				{ DpadRight, psButtons.DpadRight },
				{ DpadUp, psButtons.DpadUp },
				{ DpadLeft, psButtons.DpadLeft },
				{ DpadDown, psButtons.DpadDown },
				{ LB, psButtons.L1 },
				{ LT, psButtons.L2 },
				{ RB, psButtons.R1 },
				{ RT, psButtons.R2 },
				{ LeftStickClick, psButtons.LeftStickClick },
				{ LeftStick, psButtons.LeftStick },
				{ RightStick, psButtons.RightStick },
				{ RightStickClick, psButtons.RightStickClick },
				{ Menu, psButtons.Options },
				{ View, psButtons.Share}
			};
		}
	}
}
