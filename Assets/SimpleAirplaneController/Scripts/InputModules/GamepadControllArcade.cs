using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SimplePlaneController
{
    public class GamepadControllArcade : AirplaneInput
    {
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
        public bool forceKeybord = false;
        int controlls;
        [HideInInspector]
        public bool lost=false;//usun

        void start()
        {

        }

        public override void GetInput()
        {
            if (lost)//zrob cos po smierci - usun
            {
                //   roll = 0;
                //   yaw = 0;
                   pitch = 0.1f;
                throttle = ApplyAxisInput(throttle, false, true);
                ApplyStickyThrottle();
            }
            else
            {
                controlls = PlayerPrefs.GetInt("Controlls");
                if (controlls == 1 && forceKeybord == false)
                {
                    pitch = EvaluateAxes(pitchAxes);
                    roll = EvaluateAxes(rollAxes);
                    yaw = EvaluateAxes(yawAxes);

                    throttle = EvaluateAxes(throttleAxes);
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
                else if (controlls == 0 || forceKeybord == true)
                {
                    pitch = ApplyAxisInput(pitch, pitchUpKey, pitchDownKey);
                    roll = ApplyAxisInput(roll, rollLeftKey, rollRightKey);
                    yaw = ApplyAxisInput(yaw, yawLeftKey, yawRightKey);

                    throttle = ApplyAxisInput(throttle, throttleUpKey, throttleDownKey);
                    ApplyStickyThrottle();

                    brake = Input.GetKey(brakeKey) ? 1f : 0f;

                    if (Input.GetKeyDown(flapsDownKey))
                    {
                        flaps++;
                    }

                    if (Input.GetKeyDown(flapsUpKey))
                    {
                        flaps--;
                    }

                    flaps = Mathf.Clamp(flaps, 0, maxFlaps);

                    /* Due to sample differences in Fixed Updates, compared to Updates, these may revert too quickly for the accompanying processes to track */
                    /* As such the sample timer was introduced to ensure this is maintained, meaning they are left active for longer */
                    cameraSwitch = Input.GetKeyDown(cameraSwitchKey);
                    engineCutoff = Input.GetKeyDown(engineCutoffKey);
                    lightToggle = Input.GetKeyDown(lightToggleKey);

                    if (Input.GetKeyDown(langingGearToggleKey))
                    {
                        landingGearToggle = !landingGearToggle;
                    }

                    ApplyAutoBrake();
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
    }
}