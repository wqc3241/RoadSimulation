using System;
using System.Collections;
using UnityEngine;

namespace ElectronicRoadSign
{
	public class ElectronicArrowSignScript : MonoBehaviour
	{

		#region Enums

		public enum ElectronicArrowSignModes
		{
			Blank,
			MergeRight,
			MergeLeft,
			SolidArrowRight,
			SolidArrowLeft,
			FlashingArrowLeft,
			FlashingArrowRight,
			Diamonds,
			FlashingDots
		}

		#endregion


		#region Constants

		// number of characters per row on the texture
		private const int _TEXTURE_COLUMNS = 4;

		// number of rows of characters on the texture
		private const int _TEXTURE_ROWS = 2;

		// the character height ( 300 / 2048 )
		private const float _TEXTURE_PANEL_HEIGHT = 0.146484375f;

		// the character width ( 512 / 2048 )
		private const float _TEXTURE_PANEL_WIDTH = 0.25f;

		#endregion


		#region Inspector Variables

		// 
		public ElectronicArrowSignModes SignMode = ElectronicArrowSignModes.MergeRight;

		// duration lights are on
		public float OnTime = 2.0f;

		// duration lights are off
		public float OffTime = 1.0f;

		#endregion


		#region Private Variables

		// index of current screen of text being displayed
		private int _frameIndex = 0;

		// array of the UV indexes for the verts in the display panel
		private int[] _panelUVs;

		// the model mesh
		private Mesh _mesh;

		// flag for display initialized
		private bool _initialized = false;

		// stores the current mode so we can watch for changes
		private ElectronicArrowSignModes _currentMode;

		// flag for inverting UVs
		private bool _invertUVs = false;

		// screen animation frames
		private int[] _frames;

		// temporary array for finding the panel UVs
		private Vector2[] _roundedUVs;

		#endregion


		#region Unity Methods

		void Start()
		{
			// initialize the display
			InitializeDisplay();
		}

		private void Update()
		{
			// watch the text for changes and re-parse if needed
			if ( _initialized && _currentMode != SignMode )
			{
				StopCoroutine( "SwitchScreen" );
				PrepareAnimation();
			}
		}

		#endregion


		#region Methods

		// coroutine that initializes the display
		private void InitializeDisplay()
		{
			_initialized = false;

			// initialize array
			_panelUVs = new int[4];
			_mesh = GetComponent<MeshFilter>().mesh;

			// expected UV coords for the panel
			var ul = new Vector2( 0f, 1f );
			var ur = new Vector2( 0.25f, 1f );
			var bl = new Vector2( 0, 1f - _TEXTURE_PANEL_HEIGHT );
			var br = new Vector2( 0.25f, 1f - _TEXTURE_PANEL_HEIGHT );

			// find the panel UVs and store their indexes
			_panelUVs[0] = FindUV( _mesh, ul ); // upper left
			_panelUVs[1] = FindUV( _mesh, ur ); // upper right
			_panelUVs[2] = FindUV( _mesh, bl ); // lower left
			_panelUVs[3] = FindUV( _mesh, br ); // lower right

			_initialized = true;

			// clean up temp variable
			_roundedUVs = null;

			// start displaying the text
			PrepareAnimation();
		}

		// coroutine that switches the display to the next screen of text
		private IEnumerator SwitchScreen()
		{
			if ( _frames[_frameIndex] == 0 && OffTime > 0f )
			{
				// display the screen animation
				DisplayScreen( _frames[_frameIndex] );

				// lights are off
				yield return new WaitForSeconds( OffTime );
			}
			else if ( _frames[_frameIndex] > 0 )
			{
				// display the screen animation
				DisplayScreen( _frames[_frameIndex] );

				// lights are on
				yield return new WaitForSeconds( OnTime );
			}

			// next frame
			_frameIndex++;
			if ( _frameIndex >= _frames.Length )
			{
				// loop back to first frame
				_frameIndex = 0;
			}

			// kick off coroutine again
			StartCoroutine( "SwitchScreen" );
		}

		// splits the message text up into lines for the display
		private void PrepareAnimation()
		{
			// reset to first frame
			_frameIndex = 0;

			_currentMode = SignMode;

			switch ( _currentMode )
			{
				case ElectronicArrowSignModes.MergeRight:
					_frames = new int[4] { 0, 1, 2, 3 };
					_invertUVs = false;
					break;
				case ElectronicArrowSignModes.MergeLeft:
					_frames = new int[4] { 0, 1, 2, 3 };
					_invertUVs = true;
					break;
				case ElectronicArrowSignModes.SolidArrowLeft:
					_frames = new int[1] { 5 };
					_invertUVs = false;
					break;
				case ElectronicArrowSignModes.SolidArrowRight:
					_frames = new int[1] { 5 };
					_invertUVs = true;
					break;
				case ElectronicArrowSignModes.FlashingArrowLeft:
					_frames = new int[2] { 0, 5 };
					_invertUVs = false;
					break;
				case ElectronicArrowSignModes.FlashingArrowRight:
					_frames = new int[2] { 0, 5 };
					_invertUVs = true;
					break;
				case ElectronicArrowSignModes.Diamonds:
					_frames = new int[2] { 6, 7 };
					_invertUVs = false;
					break;
				case ElectronicArrowSignModes.FlashingDots:
					_frames = new int[2] { 0, 4 };
					_invertUVs = false;
					break;
				case ElectronicArrowSignModes.Blank:
				default:
					_frames = new int[1] { 0 };
					_invertUVs = false;
					break;
			}

			// kick off coroutine
			StartCoroutine( "SwitchScreen" );
		}

		// blanks out the display
		private void ClearDisplay()
		{
			DisplayScreen( 0 );
		}

		// displays a single character of text
		private void DisplayScreen( int index )
		{
			var newUVs = _mesh.uv;

			// make sure index is not out of range
			index = Mathf.Clamp( index, 0, ( _TEXTURE_COLUMNS * _TEXTURE_ROWS ) - 1 );

			// calculate frame UV position
			var frameX = ( ( index % _TEXTURE_COLUMNS ) * _TEXTURE_PANEL_WIDTH );
			var frameY = 1f - ( Mathf.Floor( index / _TEXTURE_COLUMNS ) * _TEXTURE_PANEL_HEIGHT );

			// calculate new uv coords
			if ( !_invertUVs )
			{
				newUVs[_panelUVs[0]] = new Vector2( frameX, frameY );
				newUVs[_panelUVs[1]] = new Vector2( frameX + _TEXTURE_PANEL_WIDTH, frameY );
				newUVs[_panelUVs[2]] = new Vector2( frameX, frameY - _TEXTURE_PANEL_HEIGHT );
				newUVs[_panelUVs[3]] = new Vector2( frameX + _TEXTURE_PANEL_WIDTH, frameY - _TEXTURE_PANEL_HEIGHT );
			}
			else
			{
				newUVs[_panelUVs[0]] = new Vector2( frameX + _TEXTURE_PANEL_WIDTH, frameY );
				newUVs[_panelUVs[1]] = new Vector2( frameX, frameY );
				newUVs[_panelUVs[2]] = new Vector2( frameX + _TEXTURE_PANEL_WIDTH, frameY - _TEXTURE_PANEL_HEIGHT );
				newUVs[_panelUVs[3]] = new Vector2( frameX, frameY - _TEXTURE_PANEL_HEIGHT );
			}

			// swap uv coords
			_mesh.uv = newUVs;
		}

		// find the UV at a specific location
		private int FindUV( Mesh mesh, Vector2 uv )
		{
			// due to floating point errors somewhere when importing the model, 
			// we have to loop through and compare rounded coords

			if ( _roundedUVs == null )
			{
				// temporarily store the rounded UV coords
				_roundedUVs = mesh.uv;
				for ( int i = 0; i < _roundedUVs.Length; i++ )
				{
					_roundedUVs[i].x = Mathf.RoundToInt( mesh.uv[i].x * 1000f );
					_roundedUVs[i].y = Mathf.RoundToInt( mesh.uv[i].y * 1000f );
				}
			}

			// find the UV index
			var index = Array.IndexOf( _roundedUVs, new Vector2( Mathf.RoundToInt( uv.x * 1000f ), Mathf.RoundToInt( uv.y * 1000f ) ) );
			if ( index >= 0 )
				return index;

			// UV not found
			return -1;
		}

		#endregion

	}
}