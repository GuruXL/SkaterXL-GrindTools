﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace GrindTools.UI
{
	public class PlaystationButtons
	{
		public Texture2D Circle;
		public Texture2D Square;
		public Texture2D Cross;
		public Texture2D Triangle;
		public Texture2D DpadRight;
		public Texture2D DpadUp;
		public Texture2D DpadLeft;
		public Texture2D DpadDown;
		public Texture2D Dpad;
		public Texture2D L1;
		public Texture2D L2;
		public Texture2D R1;
		public Texture2D R2;
		public Texture2D LeftStickClick;
		public Texture2D LeftStick;
		public Texture2D RightStick;
		public Texture2D RightStickClick;
		public Texture2D Options;
		public Texture2D Share;

		public PlaystationButtons(Texture2D circle, Texture2D square, Texture2D cross, Texture2D triangle, Texture2D dpadRight, Texture2D dpadUp, Texture2D dpadLeft, Texture2D dpadDown, Texture2D dpad, Texture2D l1, Texture2D l2, Texture2D r1, Texture2D r2, Texture2D leftStickClick, Texture2D leftStick, Texture2D rightStick, Texture2D rightStickClick, Texture2D options, Texture2D share)
		{
			Circle = circle;
			Square = square;
			Cross = cross;
			Triangle = triangle;
			DpadRight = dpadRight;
			DpadUp = dpadUp;
			DpadLeft = dpadLeft;
			DpadDown = dpadDown;
			Dpad = dpad;
			L1 = l1;
			L2 = l2;
			R1 = r1;
			R2 = r2;
			LeftStickClick = leftStickClick;
			LeftStick = leftStick;
			RightStick = rightStick;
			RightStickClick = rightStickClick;
			Options = options;
			Share = share;
		}
	}
}
