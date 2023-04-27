using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace SimplePlaneController {
    public class AirplaneInput : MonoBehaviour {

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

        public KeyCode pitchUpKey = KeyCode.W;
        public KeyCode pitchDownKey = KeyCode.S;
        public KeyCode rollLeftKey = KeyCode.A;
        public KeyCode rollRightKey = KeyCode.D;
        public KeyCode yawLeftKey = KeyCode.Z;
        public KeyCode yawRightKey = KeyCode.X;
        public KeyCode throttleUpKey = KeyCode.LeftShift;
        public KeyCode throttleDownKey = KeyCode.LeftControl;
        public KeyCode flapsDownKey = KeyCode.F;
        public KeyCode flapsUpKey = KeyCode.G;
        public KeyCode brakeKey = KeyCode.Space;
        public KeyCode cameraSwitchKey = KeyCode.C;
        public KeyCode engineCutoffKey = KeyCode.K;
        public KeyCode lightToggleKey = KeyCode.L;
        public KeyCode langingGearToggleKey = KeyCode.Q;


        DestroyAirplane destroyed;
        //AI
        [HideInInspector]
        public bool AI = true;
        GameObject objective;
        Vector3 vecdest, up, down, left, right;
        Vector3 upvec = new Vector3(0, 1, 0), downvec = new Vector3(0, -1, 0), leftvec = new Vector3(-1, 0, 0), rightvec = new Vector3(1, 0, 0);
        [HideInInspector]
        public bool bleft, bright, bup, bdown;


        /* Properties */
        public float Pitch {
            get {
                return pitch;
            }
        }

        public float Roll {
            get {
                return roll;
            }
        }

        public float Yaw {
            get {
                return yaw;
            }
        }

        public float Throttle {
            get {
                return throttle;
            }
        }

        public float Flaps {
            get {
                return flaps;
            }
        }

        public bool LandingGear { 
            get {
                return landingGearToggle;
            }
        }

        public float FlapsNormalized {
            get{
                return Mathf.InverseLerp(0, maxFlaps, flaps);
            }
        }

        public float Brake {
            get {
                return brake;
            }
        }

        public float StickyThrottle{
            get {
                return stickyThrottle;
            }
        }

        public bool CameraSwitch{
            get {
                return cameraSwitch;
            }
        }

        public bool EngineCutoff{
            get {
                return engineCutoff;
            }
        }

        public bool LightToggle{
            get {
                return lightToggle;
            }
        }

        /* Methods */
        void Start() {
            destroyed = this.GetComponent<DestroyAirplane>();
            objective = GameObject.FindGameObjectWithTag("Player");
            /*if (this.gameObject == objective)
                AI = false;*/
            AI = GetComponent<SetPlane>().AI;
            if (startingThrottle > 0.01f){
                stickyThrottle = Mathf.Clamp01(startingThrottle);
            }
        }

        void Update(){
            GetInput();
        }

        void calcVecs()
        {
            //front = this.transform.rotation * frontvec;
            up = this.transform.rotation * upvec;
            down = this.transform.rotation * downvec;

            left = this.transform.rotation * leftvec;
            right = this.transform.rotation * rightvec;
        }

        public virtual void GetInput(){
            if (AI)
            {
                if (!destroyed.destroyed)
                {
                    vecdest = objective.transform.position - this.transform.position;
                    calcVecs();
                    // vecdest = vecdest.normalized;
                    //
                    //  Vector3.Magnitude(vecdest - (this.transform.position + upvec)) <= Vector3.Magnitude(vecdest - (this.transform.position + downvec);
                    if (Vector3.Magnitude(vecdest - up) <= Vector3.Magnitude(vecdest - down))
                    {
                        //UP
                        bup = true;
                        bdown = false;
                    }
                    else
                    {
                        bup = false;
                        bdown = true;
                    }

                    if (Vector3.Magnitude(vecdest - right) <= Vector3.Magnitude(vecdest - left))
                    {
                        //RIGHT
                        bright = true;
                        bleft = false;

                    }
                    else
                    {
                        bright = false;
                        bleft = true;
                    }
                }

                //3500 3000 3000
                /*if (true)//jezeli odleglosc na x zbyt mala, to dopasuj rotacje
                {
                    if (Vector3.Magnitude(vecdest - right) <= Vector3.Magnitude(vecdest - left))
                    {
                        //RIGHT
                        bright = true;
                        bleft = false;
                        
                    }
                    else
                    {
                        bright = false;
                        bleft = true;
                    }
                }
                else
                {
                    if(objective.transform.rotation.z > this.transform.rotation.z)
                    {
                        bright = false;
                        bleft = true;
                    }
                    else
                    {
                        bright = true;
                        bleft = false;
                    }
                }*/


                //print("up:" + bup + " down:" + bdown);
                // print("up:" + up + " down:" + down);

                /*if (veccur.y > vecdest.y)
                {
                    down = true;
                    up = false;
                }
                else
                {
                    up = true;
                    down = false;
                }*/

                /*if (veccur.x > vecdest.x)
                {
                    left = true;
                }
                else
                {
                    right = true;
                }*/

                pitch = ApplyAxisInputAI(pitch, bdown, bup);
                roll = ApplyAxisInputAI(roll, bleft, bright);
                //  yaw = ApplyAxisInputAI(yaw, left, right);

                throttle = ApplyAxisInputAI(throttle, false, destroyed.destroyed);
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
            else
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

        protected float ApplyAxisInput(float axisValue, KeyCode positiveKey, KeyCode negativeKey){
            if(Input.GetKey(positiveKey)){
                axisValue += inputSensitivity;
            } else if(Input.GetKey(negativeKey)){
                axisValue -= inputSensitivity;
            } else {
                //Normalize to 0
                if(axisValue > inputSensitivity){
                    axisValue -= inputSensitivity;
                } else if(axisValue < -inputSensitivity) {
                    axisValue += inputSensitivity;
                } else {
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

        protected void ApplyStickyThrottle(){
            stickyThrottle = stickyThrottle + (throttle * throttleStepSize * Time.deltaTime);
            stickyThrottle = Mathf.Clamp01(stickyThrottle);

            if (engineCutoff){
                // The engine was recently cut, reset sticky
                stickyThrottle = 0f;
            }
        }

        protected void ApplyAutoBrake(){
            if (autoBrake){
                if(stickyThrottle < 0.01f){
                    brake = 1f;
                }
            }
        }

    }
}
