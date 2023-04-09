using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimplePlaneController
{
    public class AI : MonoBehaviour
    {
        AirplaneInput change;
        /* Variables */
        protected float pitch = 0f;
        protected float roll = 0f;
        protected float yaw = 0f;
        protected float throttle = 0f;
        protected int flaps = 0;
        protected float brake = 0f;
        protected float stickyThrottle;
        protected bool cameraSwitch = false;
        protected bool engineCutoff = false;
        protected bool lightToggle = false;
        protected bool landingGearToggle = false;

        protected float timeSinceLastTick = 0f;

        public int maxFlaps = 3;
        public float inputSensitivity = 0.1f;
        public float throttleStepSize = 0.1f;
        public float startingThrottle = 1f;
        public bool autoBrake = false;

        //AI
        GameObject objective;
        Vector3 vecdest, veccur,initdir = new Vector3(0,0,1);
        bool left, right, up, down;


        /* Properties */
        public float Pitch
        {
            get
            {
                return pitch;
            }
        }

        public float Roll
        {
            get
            {
                return roll;
            }
        }

        public float Yaw
        {
            get
            {
                return yaw;
            }
        }

        public float Throttle
        {
            get
            {
                return throttle;
            }
        }

        public float Flaps
        {
            get
            {
                return flaps;
            }
        }

        public bool LandingGear
        {
            get
            {
                return landingGearToggle;
            }
        }

        public float FlapsNormalized
        {
            get
            {
                return Mathf.InverseLerp(0, maxFlaps, flaps);
            }
        }

        public float Brake
        {
            get
            {
                return brake;
            }
        }

        public float StickyThrottle
        {
            get
            {
                return stickyThrottle;
            }
        }

        public bool CameraSwitch
        {
            get
            {
                return cameraSwitch;
            }
        }

        public bool EngineCutoff
        {
            get
            {
                return engineCutoff;
            }
        }

        public bool LightToggle
        {
            get
            {
                return lightToggle;
            }
        }

        /* Methods */
        void Start()
        {
            objective = GameObject.FindGameObjectWithTag("Player");
            if (startingThrottle > 0.01f)
            {
                stickyThrottle = Mathf.Clamp01(startingThrottle);
            }
        }

        void Update()
        {
            vecdest = objective.transform.position - this.transform.position;
            vecdest = vecdest.normalized;
            veccur = this.transform.rotation * initdir;

            print(veccur + " cur");
            print(vecdest + " dest");

            if (veccur.y>vecdest.y)
            {
                down = true;
                pitch += inputSensitivity;
            }
            else
            {
                pitch -= inputSensitivity;
                up = true;
            }

            if(veccur.x>vecdest.x)
            {
                left = true;
                yaw += inputSensitivity*10;
            }
            else
            {
                right = true;
                yaw -= inputSensitivity*10;
            }

            
            
            pitch = ApplyAxisInput(pitch, up, down);

           // roll = ApplyAxisInput(roll, rollLeftKey, rollRightKey);
            yaw = ApplyAxisInput(yaw, left, right);

           // throttle = ApplyAxisInput(throttle, throttleUpKey, throttleDownKey);
            ApplyStickyThrottle();

          //  brake = Input.GetKey(brakeKey) ? 1f : 0f;

           /* if (Input.GetKeyDown(flapsDownKey))
            {
                flaps++;
            }

            if (Input.GetKeyDown(flapsUpKey))
            {
                flaps--;
            }

            flaps = Mathf.Clamp(flaps, 0, maxFlaps);*/

            /*cameraSwitch = Input.GetKeyDown(cameraSwitchKey);
            engineCutoff = Input.GetKeyDown(engineCutoffKey);
            lightToggle = Input.GetKeyDown(lightToggleKey);

            if (Input.GetKeyDown(langingGearToggleKey))
            {
                landingGearToggle = !landingGearToggle;
            }*/

            ApplyAutoBrake();

        }

        protected float ApplyAxisInput(float axisValue, bool positiveKey, bool negativeKey)
        {
            if (positiveKey)
            {
                axisValue += inputSensitivity;
            }
            else if (negativeKey)
            {
                axisValue -= inputSensitivity;
            }
            else
            {
                //Normalize to 0
                if (axisValue > inputSensitivity)
                {
                    axisValue -= inputSensitivity;
                }
                else if (axisValue < -inputSensitivity)
                {
                    axisValue += inputSensitivity;
                }
                else
                {
                    axisValue = 0f;
                }
            }

            axisValue = Mathf.Clamp(axisValue, -1f, 1f);
            return axisValue;
        }

        protected void ApplyStickyThrottle()
        {
            stickyThrottle = stickyThrottle + (throttle * throttleStepSize * Time.deltaTime);
            stickyThrottle = Mathf.Clamp01(stickyThrottle);

            if (engineCutoff)
            {
                // The engine was recently cut, reset sticky
                stickyThrottle = 0f;
            }
        }

        protected void ApplyAutoBrake()
        {
            if (autoBrake)
            {
                if (stickyThrottle < 0.01f)
                {
                    brake = 1f;
                }
            }
        }

    }
}
