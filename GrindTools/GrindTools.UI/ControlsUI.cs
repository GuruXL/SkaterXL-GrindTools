using System;
using Photon.Pun;
using UnityEngine;
using Rewired;
using System.Collections.Generic;
using System.Linq;

namespace GrindTools.UI
{
	public class ControlsUI
	{
		public static ControlsUI __instance { get; private set; }
		public static ControlsUI Instance => __instance ?? (__instance = new ControlsUI());

		private GUIStyle fontStyle;
        private GUIStyle headerStyle;
        private GUIStyle menuStyle;
		private GUIStyle sectionStyle;
		private GUIStyle controllerButtonBoxStyle;
		private Vector3 scale = Vector3.one;
		private Texture2D backgroundTex;
		public bool isPS4 { get; private set; }
		public bool isUISetup { get; private set; } = false;
	
		private bool GetControllerType()
        {
			Joystick joystick = PlayerController.Instance.inputController.player.controllers.Joysticks.FirstOrDefault();
			string text = ((joystick != null) ? joystick.name : null) ?? "unknown";
			if (text.ToLower().Contains("dual shock") || text.ToLower().Contains("dualshock") || text.ToLower().Contains("playstation"))
			{
				return isPS4;
			}
			return !isPS4;

			/*
			Joystick joystick = PlayerController.Instance.inputController.player.controllers.Joysticks.FirstOrDefault();
			string text = ((joystick != null) ? joystick.name : null) ?? "unknown";
			if (text.ToLower().Contains("xbox"))
			{
				//return isPS4;
				return !Main.controller.TempisPS4;
			}
			//return !isPS4;
			return Main.controller.TempisPS4;
			*/
		}

		public void Show(string options)
		{
			if (!isUISetup)
			{
				SetupUIlayout();
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

		private void SetupUIlayout()
		{
			GetControllerType();

			backgroundTex = new Texture2D(1, 1);
			backgroundTex.wrapMode = TextureWrapMode.Repeat;
			backgroundTex.SetPixel(0, 0, new Color(0.24f, 0.24f, 0.24f, 0.90f));
			backgroundTex.Apply();
			scale.y = Screen.height / 1440f;
			scale.x = scale.y;
			scale.z = 1f;
			fontStyle = new GUIStyle
			{
				fontSize = 25,
				normal = { textColor = new Color(1f, 1f, 1f, 1f) },
				alignment = TextAnchor.MiddleLeft,
				fixedHeight = 50f,
				padding = new RectOffset(10, 10, 0, 0)
			};

			headerStyle = new GUIStyle
			{
				fontSize = 40,
				normal = { textColor = new Color(1f, 1f, 1f, 1f) },
				alignment = TextAnchor.MiddleCenter,
				padding = new RectOffset(10, 10, 10, 10)
			};

			menuStyle = new GUIStyle(GUI.skin.window) {
				padding = new RectOffset(10, 10, 10, 10),
				normal ={background = backgroundTex}
			};

			sectionStyle = new GUIStyle(GUI.skin.window) { 
				fixedWidth = Screen.width / 3f / scale.y, 
				fontSize = 30,
				normal = { textColor = new Color(1f, 1f, 1f, 1f), background = backgroundTex },
				alignment = TextAnchor.MiddleLeft,fixedHeight = 60f,
				padding = new RectOffset(30, 30, 30, 0)
			};

			controllerButtonBoxStyle = new GUIStyle(GUI.skin.label) { 
				fixedHeight = 50f, 
				fixedWidth = 50f 
			};

			isUISetup = true;
		}

		private void ShowGrindUI()
        {
			Matrix4x4 matrix = GUI.matrix;
			scale.y = Screen.height / 1440f;
			scale.x = scale.y;
			scale.z = 1f;
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
			float edgePadding = 25f;
			float bottomPadding = 3.25f;
			float leftPadding = 3.8f;
			float width = (Screen.width / leftPadding) - (edgePadding * 2);
			float height = (Screen.height - (Screen.height / bottomPadding)) - (edgePadding * 2);
			float x = Screen.width - width - edgePadding;
			float y = edgePadding;

			// Calculate the total number of labels and sections
			int totalLabels = 14; // Adjust this based on how many Labels
			int totalSections = 3; // Adjust this based on how many CreateLabels and GUILayout.Label

			// Calculate available space
			float availableHeight = height / scale.y;
			float labelHeight = availableHeight / (totalLabels + totalSections);

			// Modify styles based on calculated height 
			fontStyle.fixedHeight = labelHeight * 0.85f; // can adjust as needed
			sectionStyle.fixedHeight = labelHeight * 1.2f; // can adjust as needed

			GUILayout.BeginArea(new Rect(x / scale.x, y / scale.y, width / scale.x, availableHeight), backgroundTex, menuStyle);
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());

			CreateHeader("Grind Spline Tool");
			GUILayout.Label("General", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Switch Modes", new Texture2D[] { UIAssetLoader.Instance.xboxButtons.Y });
			CreateLabel("Increase/Decrease Speed", "/", new Texture2D[] { UIAssetLoader.Instance.xboxButtons.DpadUp, UIAssetLoader.Instance.xboxButtons.DpadDown });

			GUILayout.Label("Camera", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Camera Fov: " + Mathf.RoundToInt(Main.settings.CamFOV).ToString(""), "+", new Texture2D[] { UIAssetLoader.Instance.xboxButtons.RB, UIAssetLoader.Instance.xboxButtons.RightStick });
			CreateLabel("Left / Right / In / Out", new Texture2D[] { UIAssetLoader.Instance.xboxButtons.LeftStick });
			CreateLabel("Up / Down", "/", new Texture2D[] {UIAssetLoader.Instance.xboxButtons.LT, UIAssetLoader.Instance.xboxButtons.RT });
			CreateLabel("Rotate", new Texture2D[] { UIAssetLoader.Instance.xboxButtons.RightStick });

			GUILayout.Label("Splines", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Remove Active Spline Points", "or", new Texture2D[] { UIAssetLoader.Instance.xboxButtons.DpadLeft, UIAssetLoader.Instance.xboxButtons.DpadRight });
			CreateLabel("Scale Splines", "+", new Texture2D[] { UIAssetLoader.Instance.xboxButtons.RB,UIAssetLoader.Instance.xboxButtons.LeftStick });
			CreateLabel("Add Spline Points", new Texture2D[] { UIAssetLoader.Instance.xboxButtons.A });
			CreateLabel("Create New Spline", new Texture2D[] { UIAssetLoader.Instance.xboxButtons.X });

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
			float edgepadding = 25f;
			float bottomPadding = 2.5f;
			float leftPadding = 3.8f;
			float width = (Screen.width / leftPadding) - (edgepadding * 2);
			float height = (Screen.height - (Screen.height / bottomPadding)) - (edgepadding * 2);
			float x = Screen.width - width - edgepadding;
			float y = edgepadding;

			// Calculate the total number of labels and sections
			int totalLabels = 12;  // Adjust this based on how many Labels
			int totalSections = 3; // Adjust this based on how many sectionStyle

			// Calculate available space
			float availableHeight = height / scale.y;
			float labelHeight = availableHeight / (totalLabels + totalSections); // Dividing by the total number of elements (labels + sections)

			// Modify styles based on calculated height
			fontStyle.fixedHeight = labelHeight * 0.85f; // can adjust as needed
			sectionStyle.fixedHeight = labelHeight * 1.2f; // can adjust as needed

			GUILayout.BeginArea(new Rect(x / scale.x, y / scale.y, width / scale.x, availableHeight), backgroundTex, menuStyle);
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());

			CreateHeader("Wax Tool");
			GUILayout.Label("General", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Switch Modes", new Texture2D[] { UIAssetLoader.Instance.xboxButtons.Y });
			CreateLabel("Increase/Decrease Speed", "/", new Texture2D[] { UIAssetLoader.Instance.xboxButtons.DpadUp, UIAssetLoader.Instance.xboxButtons.DpadDown });

			GUILayout.Label("Camera", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Camera Fov: " + Mathf.RoundToInt(Main.settings.CamFOV).ToString(""), "+", new Texture2D[] { UIAssetLoader.Instance.xboxButtons.RB, UIAssetLoader.Instance.xboxButtons.RightStick });
			CreateLabel("Left / Right / In / Out", new Texture2D[] { UIAssetLoader.Instance.xboxButtons.LeftStick });
			CreateLabel("Up / Down", "/", new Texture2D[] { UIAssetLoader.Instance.xboxButtons.LT, UIAssetLoader.Instance.xboxButtons.RT });
			CreateLabel("Rotate", new Texture2D[] { UIAssetLoader.Instance.xboxButtons.RightStick });

			GUILayout.Label("Splines", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Change Friction", "+", new Texture2D[] { UIAssetLoader.Instance.xboxButtons.RB, UIAssetLoader.Instance.xboxButtons.LeftStick });
			CreateLabel("Toggle Coping", new Texture2D[] { UIAssetLoader.Instance.xboxButtons.X });

			GUILayout.EndVertical();
			GUILayout.EndArea();
			GUI.matrix = matrix;
		}
		private void CreateLabel(string text, string spacer, params Texture2D[] buttons)
		{
			string[] array = new string[buttons.Length];
			for (int i = 0; i < buttons.Length; i++)
			{
				array[i] = spacer;
			}
			CreateLabel(text, array, buttons);
		}

		private void CreateLabel(string text, params Texture2D[] buttons)
		{
			CreateLabel(text, (string)null, buttons);
		}

		private void CreateLabel(string text, string[] spacer, params Texture2D[] buttons)
		{
			GUILayout.Space(5);
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label(text, fontStyle, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			for (int i = 0; i < buttons.Length; i++)
			{
				GUILayout.Label(XboxButtons.SwapToPlaystationUI(buttons[i], isPS4), controllerButtonBoxStyle, Array.Empty<GUILayoutOption>());
				if (spacer != null && i != buttons.Length - 1)
				{
					GUILayout.Label(spacer[i], fontStyle, Array.Empty<GUILayoutOption>());
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(10);
		}
		private void CreateHeader(string text)
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.Label("<b>" + text + "</b>", headerStyle, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}
	}
}
