using System;
using Unity.Mathematics;
using UnityEngine;

namespace RageRunGames.EasyFlyingSystem
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class DroneController : BaseFlyController
    {
        public bool Pc = false;
        [Header("Input Settings")] [HideInInspector]
        public InputType inputType;

        [Header("Controller Settings")] 
        [SerializeField] private bool maintainAltitude = true;
        [SerializeField] protected bool useGravityOnNoInput;
        
        [Header("Hover Settings")] 
        [SerializeField] protected bool enableHover;
        [Range(0, 10)]
        [SerializeField] protected float hoverAmplitude = 1.25f;
        [Range(0, 10)]
        [SerializeField] protected float hoverFrequency = 2f;

        [Header("Ground Settings")] 
        [SerializeField] protected float groundCheckDistance = 0.2f;
        [SerializeField] protected bool decelerateOnGround;
        [SerializeField] protected float decelSpeedOnGround = 4f;
        /// <summary>
        /// Bu graphic yönune haraketi engelemek icin.
        /// </summary>
        [SerializeField] protected bool onlyGraphichPitch = false;
        [SerializeField] protected GameObject graphics = null;

        public bool isUnderCower=false;
        public Vector3 windforce =Vector3.zero;
        public LayerMask groundLayer;
        
        public float windSpeed = 0;

        
        private float timer;

        private BaseInputHandler currentInputHandler;

        public bool IsGrounded { get; private set; } = true;

        protected override void Initialize()
        {
            base.Initialize();

            groundLayer = LayerMask.GetMask("Ground");
            if (Pc)
            {
                if (InputHandler == null)
                {
                    Debug.LogWarning(" No input is added or selected, adding keyboard input as default ");
                    InputHandler = gameObject.AddComponent<KeyboardInputHandler>();

                }
            }
        }

        protected override void Update()
        {
          

            IsGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance,groundLayer);
            Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.yellow);

            if (IsGrounded && rb.linearVelocity != Vector3.zero && decelerateOnGround)
            {
                rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, decelSpeedOnGround * Time.deltaTime);
            }

            base.Update();
        }

        protected override void HandleRotations()
        {
            base.HandleRotations();

            if (autoForwardMovement)
            {
                currentPitch = pitchAmount;
            }

            if (!onlyGraphichPitch) { 
                Quaternion currentRotation = Quaternion.Euler(currentPitch, currentYaw, currentRoll);
                rb.MoveRotation(currentRotation);
            }
            else
            {
                
                Quaternion currentGraphicRotation= Quaternion.Euler(currentGraphicPitch,currentYaw ,currentRoll);
                Quaternion currentRotation = Quaternion.Euler(currentPitch, currentYaw, currentRoll);
                graphics.gameObject.transform.rotation = currentGraphicRotation;
                rb.MoveRotation(currentRotation);
            }

        }

        protected override void UpdateMovement(IInputHandler inputHandler)
        {
            Vector3 upVector = Vector3.up;
            upVector.x = 0f;
            upVector.z = 0f;

            float upVectorMagnitude = 1 - upVector.magnitude;
            float gravityMagnitude = Physics.gravity.magnitude * upVectorMagnitude;

            float upwardForce = 0f;

            if (!useGravityOnNoInput)
            {
                upwardForce = rb.mass * Physics.gravity.magnitude + gravityMagnitude + inputHandler.Lift * maxLiftSpeed;
            }
            else
            {
                upwardForce = inputHandler.Lift * maxLiftSpeed;
            }

            Vector3 liftForce = Vector3.up * upwardForce;

            Vector3 forwardForce =
                disablePitch ? Vector3.zero : inputHandler.Pitch * maxSpeed * transform.forward;
            Vector3 sidewaysForce =
                disableRoll ? Vector3.zero : inputHandler.Roll * maxSpeed * transform.right;

            if (autoForwardMovement)
            {
                forwardForce = maxSpeed * transform.forward;
            }

            if (maintainAltitude)
            {
                forwardForce.y = 0f;
                sidewaysForce.y = 0f;
            }

            if (enableHover && inputHandler.checkInputs)
            {
                timer += Time.deltaTime;
                float hoverForce = Mathf.Sin(timer * hoverFrequency) * hoverAmplitude;
                liftForce += Vector3.up * hoverForce;
            }


            
            rb.AddForce(forwardForce + forwardForce + liftForce + sidewaysForce+(windforce*windSpeed), ForceMode.Force);
            
        }

        public InputType GetInputType()
        {
            return inputType;
        }

        #region Input Helpers

        public void AddKeyboardInputs()
        {
            inputType = InputType.Keyboard;

            currentInputHandler = GetComponent<BaseInputHandler>();

            if (currentInputHandler != null)
            {
                GameObject mobileControls = GameObject.Find("Mobile Controls UI Holder");

                if (mobileControls != null)
                {
#if UNITY_EDITOR
                    if(Pc)
                    DestroyImmediate(mobileControls);
#endif

                    if (Application.isPlaying)
                    {
                        Destroy(mobileControls);
                    }
                }

                if(Pc)
                DestroyImmediate(currentInputHandler);
            }

            InputHandler = gameObject.AddComponent<KeyboardInputHandler>();
            currentInputHandler = (BaseInputHandler)InputHandler;
        }

        public void AddMobileInputs()
        {
            inputType = InputType.Mobile;

            currentInputHandler = GetComponent<BaseInputHandler>();

            if (currentInputHandler != null)
            {
                DestroyImmediate(currentInputHandler);
            }

            GameObject mobileControls = GameObject.Find("Mobile Controls UI Holder");

            if (mobileControls == null)
            {
                mobileControls = Instantiate(Resources.Load<GameObject>("Mobile Controls UI Holder"), transform, true);
                mobileControls.name = "Mobile Controls UI Holder";
            }

            InputHandler = gameObject.AddComponent<MobileInputHandler>();
            currentInputHandler = (BaseInputHandler)InputHandler;

            MobileController[] controllers = GetComponentsInChildren<MobileController>();
            ((MobileInputHandler)InputHandler).SetMobileInputControllers(controllers);
        }

        public void AddMouseInputs()
        {
            inputType = InputType.Mouse;

            currentInputHandler = GetComponent<BaseInputHandler>();

            if (currentInputHandler != null)
            {
                GameObject mobileControls = GameObject.Find("Mobile Controls UI Holder");

                if (mobileControls != null)
                {
#if UNITY_EDITOR
                    DestroyImmediate(mobileControls);
#endif

                    if (Application.isPlaying)
                    {
                        Destroy(mobileControls);
                    }
                }

                DestroyImmediate(currentInputHandler);
            }

            InputHandler = gameObject.AddComponent<MouseInputHandler>();
            currentInputHandler = (BaseInputHandler)InputHandler;
        }

        #endregion
        

    }
}