using System;
using UnityEngine;

namespace GrindTools.UI
{
	public class XboxButtons
	{
		public XboxButtons(Texture2D a, Texture2D x, Texture2D b, Texture2D y, Texture2D lb, Texture2D lt, Texture2D rb, Texture2D rt, Texture2D menu, Texture2D view, Texture2D dpadUp, Texture2D dpadRight, Texture2D dpadLeft, Texture2D dpadDown)
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
		}

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
	}
}
