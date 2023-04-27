using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace SimplePlaneController
{
    public class GamepadControll : MonoBehaviour
    {

        /* Variables */
        protected float pitch = 0f;
        protected float roll = 0f;
        protected float yaw = 0f;
        [HideInInspector]
        public float throttle = 0f;
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
        public float startingThrottle = 0f;
        public bool autoBrake = false;

        public string pitchAxes = "Vertical";
        public string rollAxes = "Horizontal";
        public string yawAxes = "Airplane Yaw";
        public string throttleAxes = "Airplane Throttle";
        public string flapsAxes = "Airplane Flaps Axes";
        public string brakeAxes = "Airplane Wheel Brake";
        public string cameraAxes = "Airplane Camera Toggle";
        public string engineCutoffAxes = "Airplane Engine Cutoff Toggle";
        public string lightToggleAxes = "Airplane Light Toggle";
        public string langingGearToggleAxes = "Airplane Gear Toggle";


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
            if (startingThrottle > 0.01f)
            {
                stickyThrottle = Mathf.Clamp01(startingThrottle);
            }
        }

        void Update()
        {
            GetInput();
        }

        public virtual void GetInput()
        {

            pitch = EvaluateAxes(pitchAxes);
            roll = EvaluateAxes(rollAxes);
            yaw = EvaluateAxes(yawAxes);

            throttle = EvaluateAxes(throttleAxes);
            UnityEngine.Debug.Log("Airplane throttle:" + throttle);
            ApplyStickyThrottle();

            brake = Mathf.Clamp01(EvaluateAxes(brakeAxes));

            if (EvaluateAxes(flapsAxes) > 0.1f)
            {
                flaps++;
            }
            else if (EvaluateAxes(flapsAxes) < -0.1f)
            {
                flaps++;
            }

            flaps = Mathf.Clamp(flaps, 0, maxFlaps);

            cameraSwitch = EvaluateAxes(cameraAxes) > 0.1f ? true : false;
            engineCutoff = EvaluateAxes(engineCutoffAxes) > 0.1f ? true : false;
            lightToggle = EvaluateAxes(lightToggleAxes) > 0.1f ? true : false;
            landingGearToggle = EvaluateAxes(langingGearToggleAxes) > 0.1f ? true : false;

            ApplyAutoBrake();

        }

        protected float ApplyAxisInput(float axisValue, KeyCode positiveKey, KeyCode negativeKey)
        {
            if (Input.GetKey(positiveKey))
            {
                axisValue += inputSensitivity;
            }
            else if (Input.GetKey(negativeKey))
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

        protected float ApplyAxisInputAI(float axisValue, bool positiveKey, bool negativeKey)
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
        private float EvaluateAxes(string name)
        {
            if (AxesExists(name))
            {
                return Input.GetAxis(name);
            }
            return 0f;
        }

        private bool AxesExists(string name)
        {
            try
            {
                float sample = Input.GetAxis(name);
                return true;
            }
            catch (System.ArgumentException ex)
            {
                //UnityEngine.Debug.Log("Airplane Controller:" + ex.Message);
                return false;
            }
        }
    }
}
