using UnityEngine;

namespace ElectronicRoadSign
{
	public class DemoSceneGUI : MonoBehaviour
	{
		public enum DemoViews
		{
			RoadSign,
			ArrowSign
		}

		public ElectronicRoadSignScript RoadSign;
		public ElectronicArrowSignScript ArrowSign;

		public Transform CurrentViewPosition;
		public Transform OnDeckPosition;

		public Light[] SceneLights;

		public DemoViews CurrentView = DemoViews.RoadSign;

		public GUIStyle BoxStyle;

		bool _transitioning = false;
		float _transitionStart;
		float _transitionSpeed = 1f;

		bool _lightsOn = true;

		void OnGUI()
		{
			switch ( CurrentView )
			{
				case DemoViews.RoadSign:
					ShowRoadSignGUI();

					RoadSign.enabled = true;
					ArrowSign.enabled = false;

					break;
				case DemoViews.ArrowSign:
					ShowArrowSignGUI();

					RoadSign.enabled = false;
					ArrowSign.enabled = true;

					break;
				default:
					break;
			}

			if ( SceneLights != null && GUI.Button( new Rect( Screen.width - 130, 30, 100, 30 ), "Toggle Lights" ) )
			{
				_lightsOn = !_lightsOn;
				foreach ( var light in SceneLights )
				{
					light.enabled = _lightsOn;
				}
			}
		}

		void ShowRoadSignGUI()
		{
			if ( RoadSign != null && !_transitioning )
			{
				var yPos = 0;

				GUI.BeginGroup( new Rect( 30, 30, 400, 350 ) );

				GUI.Box( new Rect( 0, yPos, 400, 350 ), "Electronic Road Sign", BoxStyle );
				yPos += 30;

				GUI.Label( new Rect( 10, yPos, 380, 60 ), "Displayable Characters: " + ElectronicRoadSignScript.DISPLAYABLE_CHARACTERS );
				yPos += 70;

				GUI.Label( new Rect( 10, yPos, 380, 25 ), "Message Text:" );
				yPos += 25;
				RoadSign.MessageText = GUI.TextArea( new Rect( 10, yPos, 380, 50 ), RoadSign.MessageText );
				yPos += 60;

				GUI.Label( new Rect( 10, yPos, 380, 25 ), "Screen Display Time:" );
				yPos += 25;
				GUI.Label( new Rect( 10, yPos, 40, 25 ), string.Format( "{0:0.00}s", RoadSign.ScreenDisplayTime ) );
				RoadSign.ScreenDisplayTime = GUI.HorizontalSlider( new Rect( 55, yPos + 5, 335, 15 ), RoadSign.ScreenDisplayTime, 1.0f, 20.0f );
				yPos += 30;

				GUI.Label( new Rect( 10, yPos, 380, 25 ), "Delay Between Screens:" );
				yPos += 25;
				GUI.Label( new Rect( 10, yPos, 40, 25 ), string.Format( "{0:0.00}s", RoadSign.DelayBetweenScreens ) );
				RoadSign.DelayBetweenScreens = GUI.HorizontalSlider( new Rect( 55, yPos + 5, 335, 15 ), RoadSign.DelayBetweenScreens, 0.0f, 5.0f );
				yPos += 40;

				if ( GUI.Button( new Rect( 10, yPos, 185, 30 ), "Reset" ) )
				{
					RoadSign.MessageText = "WARNING! ZOMBIES AHEAD! USE CAUTION!";
					RoadSign.ScreenDisplayTime = 2.0f;
					RoadSign.DelayBetweenScreens = 0.5f;
				}

				if ( GUI.Button( new Rect( 205, yPos, 185, 30 ), "View Arrow Sign -->" ) )
				{
					_transitioning = true;
					_transitionStart = Time.time;
					CurrentView = DemoViews.ArrowSign;
				}

				GUI.EndGroup();
			}
		}

		void ShowArrowSignGUI()
		{
			if ( ArrowSign != null && !_transitioning )
			{
				var yPos = 0;

				GUI.BeginGroup( new Rect( 30, 30, 400, 400 ) );

				GUI.Box( new Rect( 0, yPos, 400, 400 ), "Electronic Arrow Sign", BoxStyle );
				yPos += 30;

				GUI.Label( new Rect( 10, yPos, 380, 25 ), "Display Mode:" );
				yPos += 20;

				if ( GUI.Toggle( new Rect( 10, yPos, 380, 25 ), ArrowSign.SignMode == ElectronicArrowSignScript.ElectronicArrowSignModes.Blank, "Blank" ) )
				{
					ArrowSign.SignMode = ElectronicArrowSignScript.ElectronicArrowSignModes.Blank;
				}
				yPos += 20;

				if ( GUI.Toggle( new Rect( 10, yPos, 380, 25 ), ArrowSign.SignMode == ElectronicArrowSignScript.ElectronicArrowSignModes.Diamonds, "Diamonds" ) )
				{
					ArrowSign.SignMode = ElectronicArrowSignScript.ElectronicArrowSignModes.Diamonds;
				}
				yPos += 20;

				if ( GUI.Toggle( new Rect( 10, yPos, 380, 25 ), ArrowSign.SignMode == ElectronicArrowSignScript.ElectronicArrowSignModes.FlashingArrowLeft, "Left Flashing Arrow" ) )
				{
					ArrowSign.SignMode = ElectronicArrowSignScript.ElectronicArrowSignModes.FlashingArrowLeft;
				}
				yPos += 20;

				if ( GUI.Toggle( new Rect( 10, yPos, 380, 25 ), ArrowSign.SignMode == ElectronicArrowSignScript.ElectronicArrowSignModes.FlashingArrowRight, "Right Flashing Arrow" ) )
				{
					ArrowSign.SignMode = ElectronicArrowSignScript.ElectronicArrowSignModes.FlashingArrowRight;
				}
				yPos += 20;

				if ( GUI.Toggle( new Rect( 10, yPos, 380, 25 ), ArrowSign.SignMode == ElectronicArrowSignScript.ElectronicArrowSignModes.SolidArrowLeft, "Left Solid Arrow" ) )
				{
					ArrowSign.SignMode = ElectronicArrowSignScript.ElectronicArrowSignModes.SolidArrowLeft;
				}
				yPos += 20;

				if ( GUI.Toggle( new Rect( 10, yPos, 380, 25 ), ArrowSign.SignMode == ElectronicArrowSignScript.ElectronicArrowSignModes.SolidArrowRight, "Right Solid Arrow" ) )
				{
					ArrowSign.SignMode = ElectronicArrowSignScript.ElectronicArrowSignModes.SolidArrowRight;
				}
				yPos += 20;

				if ( GUI.Toggle( new Rect( 10, yPos, 380, 25 ), ArrowSign.SignMode == ElectronicArrowSignScript.ElectronicArrowSignModes.FlashingDots, "Flashing Dots" ) )
				{
					ArrowSign.SignMode = ElectronicArrowSignScript.ElectronicArrowSignModes.FlashingDots;
				}
				yPos += 20;

				if ( GUI.Toggle( new Rect( 10, yPos, 380, 25 ), ArrowSign.SignMode == ElectronicArrowSignScript.ElectronicArrowSignModes.MergeLeft, "Merge Left" ) )
				{
					ArrowSign.SignMode = ElectronicArrowSignScript.ElectronicArrowSignModes.MergeLeft;
				}
				yPos += 20;

				if ( GUI.Toggle( new Rect( 10, yPos, 380, 25 ), ArrowSign.SignMode == ElectronicArrowSignScript.ElectronicArrowSignModes.MergeRight, "Merge Right" ) )
				{
					ArrowSign.SignMode = ElectronicArrowSignScript.ElectronicArrowSignModes.MergeRight;
				}
				yPos += 30;

				GUI.Label( new Rect( 10, yPos, 380, 25 ), "On Time:" );
				yPos += 20;
				GUI.Label( new Rect( 10, yPos, 40, 25 ), string.Format( "{0:0.00}s", ArrowSign.OnTime ) );
				ArrowSign.OnTime = GUI.HorizontalSlider( new Rect( 55, yPos + 5, 335, 15 ), ArrowSign.OnTime, 0.5f, 5.0f );
				yPos += 30;

				GUI.Label( new Rect( 10, yPos, 380, 25 ), "Off Time:" );
				yPos += 20;
				GUI.Label( new Rect( 10, yPos, 40, 25 ), string.Format( "{0:0.00}s", ArrowSign.OffTime ) );
				ArrowSign.OffTime = GUI.HorizontalSlider( new Rect( 55, yPos + 5, 335, 15 ), ArrowSign.OffTime, 0.0f, 5.0f );
				yPos += 40;

				if ( GUI.Button( new Rect( 205, yPos, 185, 30 ), "View Road Sign -->" ) )
				{
					_transitioning = true;
					_transitionStart = Time.time;
					CurrentView = DemoViews.RoadSign;
				}

				GUI.EndGroup();
			}
		}


		void Update()
		{
			if ( _transitioning )
			{
				var t = ( Time.time - _transitionStart ) * _transitionSpeed;
				var viewToPos = CurrentViewPosition.position;
				viewToPos.x = Mathf.Lerp( OnDeckPosition.position.x, CurrentViewPosition.position.x, t );
				var hideToPos = OnDeckPosition.position;
				hideToPos.x = Mathf.Lerp( CurrentViewPosition.position.x, OnDeckPosition.position.x, t );

				if ( CurrentView == DemoViews.RoadSign )
				{
					RoadSign.transform.position = Vector3.Lerp( RoadSign.transform.position, viewToPos, t );
					ArrowSign.transform.position = Vector3.Lerp( ArrowSign.transform.position, hideToPos, t );
				}
				else
				{
					ArrowSign.transform.position = Vector3.Lerp( ArrowSign.transform.position, viewToPos, t );
					RoadSign.transform.position = Vector3.Lerp( RoadSign.transform.position, hideToPos, t );
				}

				if ( t >= 1f )
				{
					_transitioning = false;
				}
			}
		}
	}
}