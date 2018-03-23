using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElectronicRoadSign
{
	public class ElectronicRoadSignScript : MonoBehaviour
	{

		#region Constants

		// the characters that are displayable in order of how they appear on the texture
		public const string DISPLAYABLE_CHARACTERS = " ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!$^()-+=<>?,./:'*_\"#;[]{}%&";

		// number of characters per row on the texture
		private const int _TEXTURE_COLUMNS = 16;

		// number of rows of characters on the texture
		private const int _TEXTURE_ROWS = 4;

		// the character height ( 180 / 2048 )
		private const float _TEXTURE_PANEL_HEIGHT = 0.087890625f;

		// the character width ( 128 / 2048 )
		private const float _TEXTURE_PANEL_WIDTH = 0.0625f;

		// texture margin around the display characters
		private const float _TEXTURE_PANEL_MARGIN = 0.001f;

		#endregion


		#region Inspector Variables

		// the text message to be displayed (hint: the display has 3 lines with 8 characters per line)
		public string MessageText = "WARNING ZOMBIES AHEAD!";

		// duration each screen of a message is displayed (if there is more than 3 lines of text)
		public float ScreenDisplayTime = 5.0f;

		// duration to pause between multi-screen messages (if there is more than 3 lines of text)
		public float DelayBetweenScreens = 0.5f;

		#endregion


		#region Private Variables

		// parsed lines of text to be displayed
		private string[] _lines;

		// stores a copy of the message text used to check for changes to the message text so we can re-parse if needed
		private string _text;

		// index of current screen of text being displayed
		private int _screen = 0;

		// marks the last line as closed to force a new line to be added for the next word 
		// starts as true since we don't have any existing lines
		private bool _lastLineClosed = true;

		// array of the UV indexes for the verts in the display panels
		private int[,] _panelUVs;

		// the model mesh
		private Mesh _mesh;

		// flag for display initialized
		private bool _initialized = false;

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
			if ( _initialized && _text != MessageText )
			{
				StopCoroutine( "SwitchScreen" );
				ParseText();
			}
		}

		#endregion


		#region Methods

		// initializes the display
		private void InitializeDisplay()
		{
			_initialized = false;

			// initialize array
			_panelUVs = new int[24, 4];
			_mesh = GetComponent<MeshFilter>().mesh;

			//Vector2 ul, ur, bl, br;
			int panelNumber;

			for ( int i = 0; i < 3; i++ )
			{
				for ( int j = 0; j < 8; j++ )
				{
					panelNumber = ( i * 8 ) + j;

					// find the panel UVs and store their indexes
					_panelUVs[panelNumber, 0] = FindUV( _mesh, new Vector2( panelNumber * 0.02f, 1f ) ); // upper left
					_panelUVs[panelNumber, 1] = FindUV( _mesh, new Vector2( ( panelNumber * 0.02f ) + 0.01f, 1f ) ); // upper right
					_panelUVs[panelNumber, 2] = FindUV( _mesh, new Vector2( panelNumber * 0.02f, 1f - 0.005f ) ); // lower left
					_panelUVs[panelNumber, 3] = FindUV( _mesh, new Vector2( ( panelNumber * 0.02f ) + 0.01f, 1f - 0.005f ) ); // lower right
				}
			}

			_initialized = true;

			// free up memory
			_roundedUVs = null;

			// start displaying the text
			ParseText();
		}

		// coroutine that switches the display to the next screen of text
		private IEnumerator SwitchScreen()
		{
			// show message for desired length of time
			yield return new WaitForSeconds( ScreenDisplayTime );

			// blank out display
			ClearDisplay();

			// leave blank for delay time
			yield return new WaitForSeconds( DelayBetweenScreens );

			// move to next screen
			_screen++;
			if ( _screen >= Mathf.CeilToInt( _lines.Length / 3f ) )
			{
				// reset to first screen
				_screen = 0;
			}

			// display the message text
			DisplayText();

			// kick off coroutine again
			StartCoroutine( "SwitchScreen" );
		}

		// splits the message text up into lines for the display
		private void ParseText()
		{
			// no lines created yet, prevent trying to add to a non-existant line
			_lastLineClosed = true;

			// replace new lines with the pipe character
			MessageText = MessageText.Replace( '\n', '|' ).ToUpper();

			// store text for monitoring changes
			_text = MessageText;

			// split text up by spaces
			var words = MessageText.Split( ' ' );

			// list for creating our line array
			var lineList = new List<string>();

			for ( int i = 0; i < words.Length; i++ )
			{
				// capitalize word
				var word = words[i].Trim().ToUpper();

				// new line
				if ( word.Contains( "|" ) )
				{
					// split the word up by the pipe | character
					var lineBreakWords = word.Split( '|' );

					for ( int j = 0; j < lineBreakWords.Length; j++ )
					{
						// append the word to the existing lines
						i = AddWordToLines( words, lineList, i, lineBreakWords[j].Trim() );

						if ( j < lineBreakWords.Length - 1 )
						{
							// if we are not at the last line break work
							// close the last line
							_lastLineClosed = true;
						}
					}
				}
				else
				{
					// just append the word, no other processing needed
					i = AddWordToLines( words, lineList, i, word );
				}
			}

			// center the lines
			for ( int i = 0; i < lineList.Count; i++ )
			{
				lineList[i] = lineList[i].Trim();
				var spacesToPrepend = Mathf.FloorToInt( ( 8 - lineList[i].Length ) / 2 );
				for ( int j = 0; j < spacesToPrepend; j++ )
				{
					lineList[i] = " " + lineList[i];
				}
			}

			// store the lines in a regular array
			_lines = lineList.ToArray();

			// reset the screen index
			_screen = 0;

			// show the first screen of the message
			DisplayText();

			// if we have more than one screen of text, enable screen cycling
			if ( _lines.Length > 3 )
			{
				// start the timer to switch the screen
				StartCoroutine( "SwitchScreen" );
			}
		}

		// updates the display with a screen of text
		private void DisplayText()
		{
			// display 3 lines of text
			for ( int i = 0; i < 3; i++ )
			{
				// calculate the line index
				var lineIndex = ( _screen * 3 ) + i;

				// if the line index exists
				if ( lineIndex < _lines.Length )
				{
					// display line text
					for ( int j = 0; j < 8; j++ )
					{
						// display character
						if ( j < _lines[lineIndex].Length )
							DisplayCharacter( i, j, _lines[lineIndex][j] );
						else
							DisplayCharacter( i, j, ' ' );
					}
				}
				else
				{
					// no line of text to display, so clear the remaining lines
					for ( int j = 0; j < 8; j++ )
					{
						DisplayCharacter( i, j, ' ' );
					}
				}
			}
		}

		// blanks out the display
		private void ClearDisplay()
		{
			for ( int row = 0; row < 3; row++ )
			{
				for ( int col = 0; col < 8; col++ )
				{
					DisplayCharacter( row, col, ' ' );
				}
			}
		}

		// appends a word to the lines of text
		private int AddWordToLines( string[] words, List<string> lineList, int i, string word )
		{
			if ( word.Length > 8 )
			{
				// add the first 8 characters of the word to the line list
				lineList.Add( word.Substring( 0, 8 ) );
				// remove the first 8 characters from the word
				words[i] = word.Substring( 8, word.Length - 8 );
				// make sure we go back and process the rest of this word
				i--;
			}
			else if ( _lastLineClosed || ( word.Length > 0 && word.Length + 1 + lineList[lineList.Count - 1].Length > 8 ) )
			{
				// can't add the word to the last line
				// so add the full word to a new line
				lineList.Add( word );

				// new line is not closed (yet)
				_lastLineClosed = false;
			}
			else
			{
				// add the word to the last line
				lineList[lineList.Count - 1] += " " + word;
			}
			return i;
		}

		// displays a single character of text
		private void DisplayCharacter( int row, int col, char p )
		{
			var panelNumber = ( row * 8 ) + col;
			var newUVs = _mesh.uv;
			var charIndex = DISPLAYABLE_CHARACTERS.IndexOf( p );

			// if not a displayable character
			if ( charIndex < 0 )
			{
				charIndex = 0;
			}

			// calculate character position
			var charX = ( ( charIndex % _TEXTURE_COLUMNS ) * _TEXTURE_PANEL_WIDTH ) + _TEXTURE_PANEL_MARGIN;
			var charY = 1f - ( Mathf.Floor( charIndex / _TEXTURE_COLUMNS ) * _TEXTURE_PANEL_HEIGHT ) + _TEXTURE_PANEL_MARGIN;

			// calculate new uv coords
			newUVs[_panelUVs[panelNumber, 0]] = new Vector2( charX, charY );
			newUVs[_panelUVs[panelNumber, 1]] = new Vector2( charX + _TEXTURE_PANEL_WIDTH, charY );
			newUVs[_panelUVs[panelNumber, 2]] = new Vector2( charX, charY - _TEXTURE_PANEL_HEIGHT );
			newUVs[_panelUVs[panelNumber, 3]] = new Vector2( charX + _TEXTURE_PANEL_WIDTH, charY - _TEXTURE_PANEL_HEIGHT );

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