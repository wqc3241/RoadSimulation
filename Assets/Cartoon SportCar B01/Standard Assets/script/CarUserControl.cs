using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


namespace UnityStandardAssets.Vehicles.Car
{

    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use


        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {


            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);

            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = -(rec.lY - 32767);
            float b = rec.lRz - 32767;

#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, b, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
            //Debug.Log(h.ToString() + " vrt: " +  v.ToString() + " brake: " + b.ToString() + " handbrake: " +  handbrake.ToString() + " " + Input.GetAxis("Horizontal"));
        }
    }
}
