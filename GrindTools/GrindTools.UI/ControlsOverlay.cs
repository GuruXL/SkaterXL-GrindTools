using System;
using Photon.Pun;
using UnityEngine;
using Rewired;
using System.Collections.Generic;

namespace GrindTools.UI
{
	public class ControlsOverlay : MonoBehaviour
	{
		private GUIStyle fontStyle;
		private GUIStyle menuStyle;
		private GUIStyle sectionStyle;
		private GUIStyle controllerButtonBoxStyle;
		private Vector3 scale = Vector3.one;
		//private int guiPadding = 2;
		private Texture2D backgroundTex;
		private bool isXbox;

		//public static ControlsOverlay __instance { get; private set; }
		//public static ControlsOverlay Instance => __instance ?? (__instance = new ControlsOverlay());
		public bool GUIReady { get; private set; }

		public void Show(string options)
		{
			if (!GUIReady)
			{
				initGui();
				return;
			}
            switch (options)
            {
				case"Grind":
					ShowGrindUI();
					break;
				case"Wax":
					ShowWaxUI();
					break;
            }
		}

		private void initGui()
		{
			IList<Joystick> joySticks = PlayerController.Instance.inputController.player.controllers.Joysticks;
			//string[] joystickNames = Input.GetJoystickNames();
			for (int i = 0; i < joySticks.Count; i++)
			{
				if (joySticks[i].name.ToLower().Contains("xbox") || joySticks[i].hardwareName.ToLower().Contains("xbox"))
				{
					isXbox = true;
				}
			}
			backgroundTex = new Texture2D(1, 1);
			backgroundTex.wrapMode = TextureWrapMode.Repeat;
			backgroundTex.SetPixel(0, 0, new Color(0.24f, 0.24f, 0.24f, 0.85f));
			backgroundTex.Apply();
			scale.y = Screen.height / 1440f;
			scale.x = scale.y;
			scale.z = 1f;
			fontStyle = new GUIStyle
			{
				fontSize = 25,
				normal =
				{
					textColor = new Color(1f, 1f, 1f, 1f)
				},
				alignment = TextAnchor.MiddleLeft,
				fixedHeight = 50f,
				padding = new RectOffset(10, 10, 0, 0)
			};
			menuStyle = new GUIStyle(GUI.skin.window)
			{
				padding = new RectOffset(10, 10, 50, 50),
				normal =
				{
					background = backgroundTex
				}
			};
			sectionStyle = new GUIStyle(GUI.skin.window)
			{
				fixedWidth = Screen.width / 3f / scale.y,
				fontSize = 30,
				normal =
				{
					textColor = new Color(1f, 1f, 1f, 1f),
					background = backgroundTex
				},
				alignment = TextAnchor.MiddleLeft,
				fixedHeight = 60f,
				padding = new RectOffset(30, 30, 30, 0)
			};
			controllerButtonBoxStyle = new GUIStyle(GUI.skin.label)
			{
				fixedHeight = 50f,
				fixedWidth = 50f
			};
			
			GUIReady = true;
		}

		private void ShowGrindUI()
        {
			Matrix4x4 matrix = GUI.matrix;
			scale.y = Screen.height / 1440f;
			scale.x = scale.y;
			scale.z = 1f;
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
			float padding = 25f;
			float width = (Screen.width / 3.8f) - (padding * 2);
			float height = (Screen.height - (Screen.height / 3f)) - (padding * 2);

			// Position the UI in the top-right corner with padding
			float x = Screen.width - width - padding;
			float y = padding;

			GUILayout.BeginArea(new Rect(x / scale.x, y / scale.y, width / scale.x, height / scale.y), backgroundTex, menuStyle);
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			GUILayout.Label("General", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Switch Modes", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.Triangle
			});
			CreateLabel("Increase/Decrease Speed", "/", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.DpadUp,
				UIAssetLoader.Instance.psButtons.DpadDown
			});
			GUILayout.Label("Camera", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Left / Right / In / Out", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.LeftStick
			});
			CreateLabel("Up / Down", "/", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.L2,
				UIAssetLoader.Instance.psButtons.R2
			});
			CreateLabel("Rotate", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.RightStick
			});
			GUILayout.Label("Splines", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Remove Active Spline Points", "or", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.DpadLeft,
				UIAssetLoader.Instance.psButtons.DpadRight
			});
			CreateLabel("Scale Splines", "+", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.R1,
				UIAssetLoader.Instance.psButtons.LeftStick
			});
			CreateLabel("Add Spline Points", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.Cross
			});
			CreateLabel("Create New Spline", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.Square
			});
			GUILayout.EndVertical();
			GUILayout.EndArea();
			GUI.matrix = matrix;
		}

		private void ShowWaxUI()
        {
			Matrix4x4 matrix = GUI.matrix;
			scale.y = Screen.height / 1440f;
			scale.x = scale.y;
			scale.z = 1f;
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
			float padding = 25f;
			float width = (Screen.width / 3.8f) - (padding * 2);
			float height = (Screen.height - (Screen.height / 2.25f)) - (padding * 2);

			// Position the UI in the top-right corner with padding
			float x = Screen.width - width - padding;
			float y = padding;

			GUILayout.BeginArea(new Rect(x / scale.x, y / scale.y, width / scale.x, height / scale.y), backgroundTex, menuStyle);
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			GUILayout.Label("General", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Switch Modes", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.Triangle
			});
			CreateLabel("Increase/Decrease Speed", "/", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.DpadUp,
				UIAssetLoader.Instance.psButtons.DpadDown
			});
			GUILayout.Label("Camera", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Left / Right / In / Out", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.LeftStick
			});
			CreateLabel("Up / Down", "/", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.L2,
				UIAssetLoader.Instance.psButtons.R2
			});
			CreateLabel("Rotate", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.RightStick
			});
			GUILayout.Label("Splines", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Change Friction", "+", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.R1,
				UIAssetLoader.Instance.psButtons.LeftStick
			});
			CreateLabel("Toggle Coping", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.Cross
			});
			GUILayout.EndVertical();
			GUILayout.EndArea();
			GUI.matrix = matrix;
		}
		private void CreateLabel(string text, string delimiter, params Texture2D[] buttons)
		{
			string[] array = new string[buttons.Length];
			for (int i = 0; i < buttons.Length; i++)
			{
				array[i] = delimiter;
			}
			CreateLabel(text, array, buttons);
		}

		private void CreateLabel(string text, params Texture2D[] buttons)
		{
			CreateLabel(text, (string)null, buttons);
		}

		private void CreateLabel(string text, string[] delimiter, params Texture2D[] buttons)
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label(text, fontStyle, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			for (int i = 0; i < buttons.Length; i++)
			{
				GUILayout.Label(PlaystationButton.convertPsTo(buttons[i], isXbox), controllerButtonBoxStyle, Array.Empty<GUILayoutOption>());
				if (delimiter != null && i != buttons.Length - 1)
				{
					GUILayout.Label(delimiter[i], fontStyle, Array.Empty<GUILayoutOption>());
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(20f);
		}
	}
}
