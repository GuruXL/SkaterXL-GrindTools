﻿using System;
using Photon.Pun;
using UnityEngine;
using Rewired;
using System.Collections.Generic;

namespace GrindTools.UI
{
	public class ControlsUI
	{
		private GUIStyle fontStyle;
		private GUIStyle menuStyle;
		private GUIStyle sectionStyle;
		private GUIStyle controllerButtonBoxStyle;
		private Vector3 scale = Vector3.one;
		//private int guiPadding = 2;
		private Texture2D backgroundTex;
		private bool isJoystickXbox;

		public static ControlsUI __instance { get; private set; }
		public static ControlsUI Instance => __instance ?? (__instance = new ControlsUI());
		public bool isUISetup { get; private set; }

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
			//IList<Joystick> joySticks = PlayerController.Instance.inputController.player.controllers.Joysticks;
			string[] joySticks = Input.GetJoystickNames();
			for (int i = 0; i < joySticks.Length; i++)
			{
				if (joySticks[i].ToLower().Contains("xbox"))
				{
					isJoystickXbox = true;
				}
				/*
				if (joySticks[i].name.ToLower().Contains("xbox") || joySticks[i].hardwareName.ToLower().Contains("xbox"))
				{
					isJoystickXbox = true;
				}
				*/
			}
			backgroundTex = new Texture2D(1, 1);
			backgroundTex.wrapMode = TextureWrapMode.Repeat;
			backgroundTex.SetPixel(0, 0, new Color(0.24f, 0.24f, 0.24f, 0.85f));
			backgroundTex.Apply();
			scale.y = Screen.height / 1440f;
			scale.x = scale.y;
			scale.z = 1f;
			fontStyle = new GUIStyle {
				fontSize = 25, 
				normal = {textColor = new Color(1f, 1f, 1f, 1f)}, 
				alignment = TextAnchor.MiddleLeft, 
				fixedHeight = 50f, padding = new RectOffset(10, 10, 0, 0)
			};

			menuStyle = new GUIStyle(GUI.skin.window) {
				padding = new RectOffset(10, 10, 50, 50),
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
			float bottomPadding = 3f;
			float leftPadding = 3.8f;
			float width = (Screen.width / leftPadding) - (edgePadding * 2);
			float height = (Screen.height - (Screen.height / bottomPadding)) - (edgePadding * 2);
			float x = Screen.width - width - edgePadding;
			float y = edgePadding;

			GUILayout.BeginArea(new Rect(x / scale.x, y / scale.y, width / scale.x, height / scale.y), backgroundTex, menuStyle);
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());

			GUILayout.Label("General", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Switch Modes", new Texture2D[] { UIAssetLoader.Instance.psButtons.Triangle });
			CreateLabel("Increase/Decrease Speed", "/", new Texture2D[] { UIAssetLoader.Instance.psButtons.DpadUp, UIAssetLoader.Instance.psButtons.DpadDown });

			GUILayout.Label("Camera", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Left / Right / In / Out", new Texture2D[] { UIAssetLoader.Instance.psButtons.LeftStick });
			CreateLabel("Up / Down", "/", new Texture2D[] {UIAssetLoader.Instance.psButtons.L2, UIAssetLoader.Instance.psButtons.R2 });
			CreateLabel("Rotate", new Texture2D[] { UIAssetLoader.Instance.psButtons.RightStick });

			GUILayout.Label("Splines", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Remove Active Spline Points", "or", new Texture2D[] { UIAssetLoader.Instance.psButtons.DpadLeft, UIAssetLoader.Instance.psButtons.DpadRight });
			CreateLabel("Scale Splines", "+", new Texture2D[] { UIAssetLoader.Instance.psButtons.R1,UIAssetLoader.Instance.psButtons.LeftStick });
			CreateLabel("Add Spline Points", new Texture2D[] { UIAssetLoader.Instance.psButtons.Cross });
			CreateLabel("Create New Spline", new Texture2D[] { UIAssetLoader.Instance.psButtons.Square });

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
			float bottomPadding = 2.25f;
			float leftPadding = 3.8f;
			float width = (Screen.width / leftPadding) - (edgepadding * 2);
			float height = (Screen.height - (Screen.height / bottomPadding)) - (edgepadding * 2);
			float x = Screen.width - width - edgepadding;
			float y = edgepadding;

			GUILayout.BeginArea(new Rect(x / scale.x, y / scale.y, width / scale.x, height / scale.y), backgroundTex, menuStyle);
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());

			GUILayout.Label("General", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Switch Modes", new Texture2D[] { UIAssetLoader.Instance.psButtons.Triangle });
			CreateLabel("Increase/Decrease Speed", "/", new Texture2D[] { UIAssetLoader.Instance.psButtons.DpadUp, UIAssetLoader.Instance.psButtons.DpadDown });

			GUILayout.Label("Camera", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Left / Right / In / Out", new Texture2D[] { UIAssetLoader.Instance.psButtons.LeftStick });
			CreateLabel("Up / Down", "/", new Texture2D[] { UIAssetLoader.Instance.psButtons.L2,UIAssetLoader.Instance.psButtons.R2 });
			CreateLabel("Rotate", new Texture2D[] { UIAssetLoader.Instance.psButtons.RightStick });

			GUILayout.Label("Splines", sectionStyle, Array.Empty<GUILayoutOption>());
			CreateLabel("Change Friction", "+", new Texture2D[] { UIAssetLoader.Instance.psButtons.R1,UIAssetLoader.Instance.psButtons.LeftStick });
			CreateLabel("Toggle Coping", new Texture2D[] { UIAssetLoader.Instance.psButtons.Square });

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
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label(text, fontStyle, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			for (int i = 0; i < buttons.Length; i++)
			{
				GUILayout.Label(PlaystationButtons.SwapToPlaystationUI(buttons[i], isJoystickXbox), controllerButtonBoxStyle, Array.Empty<GUILayoutOption>());
				if (spacer != null && i != buttons.Length - 1)
				{
					GUILayout.Label(spacer[i], fontStyle, Array.Empty<GUILayoutOption>());
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(20f);
		}
	}
}