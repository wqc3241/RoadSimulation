using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


namespace UnityStandardAssets.Vehicles.Car
{

    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

        Vector3 newpos;
        Vector3 fwd;
        Vector3 prevpos;
        Vector3 movement;

        bool moveBack = false;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }

        void Update()
        {
            newpos = transform.position;
            fwd = transform.forward;
            movement = newpos - prevpos;
            if (Vector3.Dot(fwd, movement) < 0)
            {
                moveBack = true;
                print("Moving backwards");
            }
            else
            {
                moveBack = false;
                print("Moving forwards");
            }
        }

        void LateUpdate()
        {
            prevpos = transform.position;
            fwd = transform.forward;
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


            if (!moveBack)
                m_Car.Move(h, v, b, handbrake);
            else
            {
                m_Car.Move(h, v, 0, 0);
                
            }

#else
            m_Car.Move(h, v, v, 0f);
#endif
            //Debug.Log("rb.velocity: " + GetComponent<Rigidbody>().velocity.ToString() +  " h: " + h.ToString() + " vrt: " +  v.ToString() + " brake: " + b.ToString() + " handbrake: " +  handbrake.ToString() + " " + Input.GetAxis("Horizontal"));
        }
    }
}
