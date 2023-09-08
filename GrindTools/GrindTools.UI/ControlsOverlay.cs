using System;
using Photon.Pun;
using UnityEngine;

namespace GrindTools.UI
{
	public class ControlsOverlay
	{
		private static ControlsOverlay _instance;
		private GUIStyle fontStyle;
		private GUIStyle fontStyleBlue;
		private GUIStyle menuStyle;
		private GUIStyle sectionStyle;
		private GUIStyle controllerButtonBoxStyle;
		private GUIStyle inputLabelStyle;
		private Vector3 scale = Vector3.one;
		private int guiPadding;
		private Texture2D whiteTex;
		private bool isXbox;
		public static ControlsOverlay Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new ControlsOverlay();
				}
				return _instance;
			}
		}
		public bool GUIReady { get; private set; }

		public void Show()
		{
			if (!GUIReady)
			{
				initGui();
				return;
			}
			Matrix4x4 matrix = GUI.matrix;
			scale.y = Screen.height / 1440f;
			scale.x = scale.y;
			scale.z = 1f;
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
			float num = Screen.width / 3f;
			GUILayout.BeginArea(new Rect(0f, 0f, num / scale.y, (Screen.height - guiPadding * 2) / scale.y)
			{
				center = new Vector2(((Screen.width - guiPadding) - num / 2f) / scale.y, Screen.height / 2f / scale.y)
			}, whiteTex, menuStyle);
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			GUILayout.Label("General", sectionStyle, Array.Empty<GUILayoutOption>());
			//singleInfoEntry("Speed Factor", Main.customObjectDropperState.getSpeedFactor().ToString("F1") + "x Speed");
			singleControlEntry("Increase/Decrease Speed", "/", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.DpadUp,
				UIAssetLoader.Instance.psButtons.DpadDown
			});
			//singleInfoEntry("Current Axis", Main.customObjectDropperState.editorAxis.ToString() + " Axis");
			singleControlEntry("Next/Previous Axis", "/", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.DpadRight,
				UIAssetLoader.Instance.psButtons.DpadLeft
			});
			GUILayout.Label("Movement", sectionStyle, Array.Empty<GUILayoutOption>());
			singleControlEntry("Select Objects", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.Cross
			});
			singleControlEntry("Move Objects", "+", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.L1,
				UIAssetLoader.Instance.psButtons.LeftStick
			});
			singleControlEntry("Rotate Objects", "+", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.L1,
				UIAssetLoader.Instance.psButtons.RightStick
			});
			singleControlEntry("Delete Objects", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.Square
			});
			GUILayout.Label("Manipulation", sectionStyle, Array.Empty<GUILayoutOption>());
			singleControlEntry("Scale Object", new string[]
			{
				"+",
				"/"
			}, new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.R1,
				UIAssetLoader.Instance.psButtons.L2,
				UIAssetLoader.Instance.psButtons.R2
			});
			singleControlEntry("Snap to ground", "+", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.R1,
				UIAssetLoader.Instance.psButtons.Triangle
			});
			singleControlEntry("Match rotation \nwith highlighted object", "+", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.R1,
				UIAssetLoader.Instance.psButtons.Cross
			});
			singleControlEntry("Clone objects", "+", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.R1,
				UIAssetLoader.Instance.psButtons.Square
			});
			GUILayout.Label("Navigation", sectionStyle, Array.Empty<GUILayoutOption>());
			singleControlEntry("Save / Load Presets", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.Options
			});
			if (PhotonNetwork.InRoom && !PhotonNetwork.CurrentRoom.IsVisible)
			{
				if (PhotonNetwork.IsMasterClient)
				{
					singleControlEntry("Enable / Disable scaling\nfor current room", new Texture2D[]
					{
						UIAssetLoader.Instance.psButtons.Share
					});
				}
				//singleInfoEntry("Scaling Enabled", Main.customObjectDropperState.unlimitedScalingAllowed ? "ON" : ("OFF (" + Main.customObjectDropperState.roomScalingFactor.ToString() + "x)"));
			}
			GUILayout.FlexibleSpace();
			singleControlEntry("Clear Selection / Exit", new Texture2D[]
			{
				UIAssetLoader.Instance.psButtons.Circle
			});
			GUILayout.EndVertical();
			GUILayout.EndArea();
			GUI.matrix = matrix;
		}

		private void initGui()
		{
			string[] joystickNames = Input.GetJoystickNames();
			for (int i = 0; i < joystickNames.Length; i++)
			{
				if (joystickNames[i].ToLower().Contains("xbox"))
				{
					isXbox = true;
				}
			}
			whiteTex = new Texture2D(1, 1);
			whiteTex.wrapMode = TextureWrapMode.Repeat;
			whiteTex.SetPixel(0, 0, new Color(1f, 1f, 1f, 0.8f));
			whiteTex.Apply();
			scale.y = Screen.height / 1440f;
			scale.x = scale.y;
			scale.z = 1f;
			fontStyle = new GUIStyle
			{
				fontSize = 25,
				normal =
				{
					textColor = new Color(0.32f, 0.32f, 0.35f, 1f)
				},
				alignment = TextAnchor.MiddleLeft,
				fixedHeight = 50f,
				padding = new RectOffset(30, 30, 0, 0)
			};
			fontStyleBlue = new GUIStyle(fontStyle)
			{
				normal =
				{
					textColor = new Color(0.26f, 0.53f, 0.96f, 1f)
				}
			};
			menuStyle = new GUIStyle(GUI.skin.window)
			{
				padding = new RectOffset(0, 0, 50, 50),
				normal =
				{
					background = whiteTex
				}
			};
			sectionStyle = new GUIStyle(GUI.skin.window)
			{
				fixedWidth = Screen.width / 3f / scale.y,
				fontSize = 30,
				normal =
				{
					textColor = new Color(0.32f, 0.32f, 0.35f, 1f),
					background = whiteTex
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
			inputLabelStyle = new GUIStyle(GUI.skin.label)
			{
				fontSize = 16,
				normal =
				{
					textColor = new Color(0.75f, 0.75f, 0.75f, 1f)
				}
			};
			GUIReady = true;
		}

		private void singleControlEntry(string text, string delimiter, params Texture2D[] buttons)
		{
			string[] array = new string[buttons.Length];
			for (int i = 0; i < buttons.Length; i++)
			{
				array[i] = delimiter;
			}
			singleControlEntry(text, array, buttons);
		}

		private void singleControlEntry(string text, params Texture2D[] buttons)
		{
            singleControlEntry(text, (string)null, buttons);
		}

		private void singleControlEntry(string text, string[] delimiter, params Texture2D[] buttons)
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
		private void singleToggleEntry(string text, bool isOn, string onText = "ON", string offText = "OFF")
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label(text, fontStyle, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.Label(isOn ? onText : offText, fontStyleBlue, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.Space(20f);
		}
		private void singleInfoEntry(string textL, string textR)
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label(textL, fontStyle, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.Label("<b>" + textR + "</b>", fontStyleBlue, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.Space(20f);
		}
	}
}
