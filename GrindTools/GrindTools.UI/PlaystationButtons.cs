using System;
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
		public Texture2D LeftStickClick;
		public Texture2D DpadRight;
		public Texture2D DpadUp;
		public Texture2D DpadLeft;
		public Texture2D DpadDown;
		public Texture2D Dpad;
		public Texture2D L1;
		public Texture2D L2;
		public Texture2D R1;
		public Texture2D R2;
		public Texture2D LeftStick;
		public Texture2D RightStick;
		public Texture2D RightStickClick;
		public Texture2D Options;
		public Texture2D Share;

		public PlaystationButtons(Texture2D circle, Texture2D square, Texture2D cross, Texture2D triangle, Texture2D leftStickClick, Texture2D dpadRight, Texture2D dpadUp, Texture2D dpadLeft, Texture2D dpadDown, Texture2D dpad, Texture2D l1, Texture2D l2, Texture2D r1, Texture2D r2, Texture2D leftStick, Texture2D rightStick, Texture2D rightStickClick, Texture2D options, Texture2D share)
		{
			Circle = circle;
			Square = square;
			Cross = cross;
			Triangle = triangle;
			LeftStickClick = leftStickClick;
			DpadRight = dpadRight;
			DpadUp = dpadUp;
			DpadLeft = dpadLeft;
			DpadDown = dpadDown;
			Dpad = dpad;
			L1 = l1;
			L2 = l2;
			R1 = r1;
			R2 = r2;
			LeftStick = leftStick;
			RightStick = rightStick;
			RightStickClick = rightStickClick;
			Options = options;
			Share = share;
		}

		public static Texture2D convertPsTo(Texture2D tex, bool isXbox)
		{
			if (!isXbox)
			{
				return tex;
			}
			return UIAssetLoader.Instance.psButtons.mapPsToXbox()[tex];
		}

		public Dictionary<Texture2D, Texture2D> mapPsToXbox()
		{
			XboxButtons xboxButtons = UIAssetLoader.Instance.xboxButtons;
			return new Dictionary<Texture2D, Texture2D>
			{
				{
					Circle,
					xboxButtons.B
				},
				{
					Square,
					xboxButtons.X
				},
				{
					Cross,
					xboxButtons.A
				},
				{
					Triangle,
					xboxButtons.Y
				},
				{
					LeftStickClick,
					LeftStickClick
				},
				{
					DpadRight,
					xboxButtons.DpadRight
				},
				{
					DpadUp,
					xboxButtons.DpadUp
				},
				{
					DpadLeft,
					xboxButtons.DpadLeft
				},
				{
					DpadDown,
					xboxButtons.DpadDown
				},
				{
					Dpad,
					Dpad
				},
				{
					L1,
					xboxButtons.LB
				},
				{
					L2,
					xboxButtons.LT
				},
				{
					R1,
					xboxButtons.RB
				},
				{
					R2,
					xboxButtons.RT
				},
				{
					LeftStick,
					LeftStick
				},
				{
					RightStick,
					RightStick
				},
				{
					RightStickClick,
					RightStickClick
				},
				{
					Options,
					xboxButtons.Menu
				},
				{
					Share,
					xboxButtons.View
				}
			};
		}
	}
}
