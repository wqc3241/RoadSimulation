using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace SpeedSign
{
    public class SpeedSignHelper : MonoBehaviour
    {
        public TextMesh speedText;


        /// <summary>
        /// Sets the speed.
        /// </summary>
        /// <param name="speed">Speed.</param>
        public void setSpeed(string speed)
        {
            string justInt = Regex.Replace(speed, "[^0-9]", "");
            if (justInt.Length > 0)
            {
                int intConvert = int.Parse(justInt);
                if (intConvert < 0)
                {
                    speedText.text = "0";
                }
                else if (intConvert < 10)
                {
                    speedText.text = "" + justInt[justInt.Length - 1];
                }
                else if (intConvert <= 99)
                {
                    speedText.text = justInt[(justInt.Length - 2)] + "" + justInt[justInt.Length - 1];
                }
                else
                {
                    speedText.text = "99";
                }
            }
        }

    }
}