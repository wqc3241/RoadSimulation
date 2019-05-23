using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


namespace UnityStandardAssets.Vehicles.Car
{

    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        float h, v, b;
        float LIC = 32767;
        Vector3 newpos;
        Vector3 fwd;
        Vector3 prevpos;
        Vector3 movement;

        bool moveBack = false;
    

        void Start()
        {
            m_Car = GetComponent<CarController>();
            LogitechGSDK.LogiPlayConstantForce(0,100);
            m_Car.Move(0, 50, 0, 0,false);
        }

        //private void Awake()
        //{
        //    // get the car controller
        //    m_Car = GetComponent<CarController>();
        //}

        void Update()
        {
            newpos = transform.position;
            fwd = transform.forward;
            movement = newpos - prevpos;
            //if (Vector3.Dot(fwd, movement) < 0)
            //{
            //    moveBack = true;
            //    //print("Moving backwards");
            //}
            //else
            //{
            //    moveBack = false;
            //    //print("Moving forwards");
            //}

            //Vehicle Control

            float handbrake = CrossPlatformInputManager.GetAxis("Jump");

            //Debug.Log(LogitechGSDK.LogiIsConnected(0));

            if (LogitechGSDK.LogiIsConnected(0) == false)
            {
                h = CrossPlatformInputManager.GetAxis("Horizontal");
                v = CrossPlatformInputManager.GetAxis("Vertical");
                b = CrossPlatformInputManager.GetAxis("Vertical");
                Debug.Log(v);

                if (Input.GetKeyDown("r"))
                {
                    m_Car.transform.Rotate(Vector3.up * 90);
                }
                if (!moveBack)
                    m_Car.Move(h, v, b, handbrake, moveBack);
                else
                {
                    m_Car.Move(h, v, 0, 0, moveBack);
                }

            }
            else
            {

                LogitechGSDK.LogiPlayDamperForce(0, 50);
                LogitechGSDK.LogiPlayConstantForce(0,-50);
                LogitechGSDK.LogiPlaySpringForce(0, 0, 50, 50);
                LogitechGSDK.DIJOYSTATE2ENGINES rec;
                rec = LogitechGSDK.LogiGetStateUnity(0);


                h = (float)rec.lX / (float)LIC;
                v = Mathf.Abs(rec.lY - 32767);
                b = -Mathf.Abs(rec.lRz - 32767);

                if (h < .02f && h > -.02f)
                {
                    h = 0;
                }

                if (v < 20)
                {
                    v = 0;
                }

                if (rec.rgbButtons[5] == 128)
                {
                    moveBack = true;

                    Debug.Log(moveBack);
                }

                if (rec.rgbButtons[4] == 128)
                {
                    moveBack = false;

                    Debug.Log(moveBack);
                }

                UpdateSteeringWheelRotation(h);

                //if (rec.rgbButtons[7] == 128)
                //{
                //    m_Car.transform.Rotate(Vector3.up * 90);
                //}

                //Debug.Log(h + " " + v + " " + b);
 

                if (!moveBack)
                    m_Car.Move(h * 32767f, v, b, handbrake,moveBack);
                else
                {
                    m_Car.Move(h * 32767f, -v, b, handbrake, moveBack);
                }

                //Debug.Log(b);
                //Debug.Log(v);

            }


#if !MOBILE_INPUT

#else
                  m_Car.Move(h, v, v, 0f);
#endif

        }

        void LateUpdate()
        {
            prevpos = transform.position;
            fwd = transform.forward;
        }

        //Apply Constant Force
        void UpdateSteeringWheelRotation(float steer)
        {
            //steeringWheel.transform.localRotation = Quaternion.Euler (0, 0, -Input.GetAxis ("Horizontal")*90);
            //if (Input.GetKeyDown (KeyCode.F12))
            //debugGUI = !debugGUI;
            float steer_copy = steer;
            int force = (int)Mathf.Round(Mathf.Abs(steer_copy) * Mathf.Sign(steer_copy) * m_Car.GetComponent<Rigidbody>().velocity.magnitude * 7);

            //Debug.Log(force);
            //Debug.Log(force);


            if (force > 0)
            {
                LogitechGSDK.LogiPlayConstantForce(0, force);
            }
            else if (force < 0)
            {
                LogitechGSDK.LogiPlayConstantForce(0, force);
            }
            else
            {
                if (LogitechGSDK.LogiIsPlaying(0, LogitechGSDK.LOGI_FORCE_CONSTANT))
                {
                    LogitechGSDK.LogiStopConstantForce(0);
                }
            }
        }
    }
}
