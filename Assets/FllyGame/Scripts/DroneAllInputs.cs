using UnityEngine;
using UnityEngine.EventSystems;

public class DroneAllInputs : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
   
        public float Vertical
        {
            get { return (snapY) ? SnapInputs(inputVector.y, AxisTypes.Vertical) : inputVector.y; }
        }

        public float Horizontal
        {
            get { return (snapX) ? SnapInputs(inputVector.x, AxisTypes.Horizontal) : inputVector.x; }
        }


        public float RangeValue
        {
            get { return rangeValue; }
            set { rangeValue = Mathf.Abs(value); }
        }

        public float AllowedZone
        {
            get { return allowedZone; }
            set { allowedZone = Mathf.Abs(value); }
        }

        float Pitch;
        float Roll;
        float Yaw;
        float Lift;

        [SerializeField] private float rangeValue = 1;
        [SerializeField] private float allowedZone = 0;
        [SerializeField] private AxisTypes axisTypes = AxisTypes.Both;
        [SerializeField] private bool snapX = false;
        [SerializeField] private bool snapY = false;

        [SerializeField] protected RectTransform holder = null;
        [SerializeField] private RectTransform knob = null;

        private Canvas canvasHolder;
        private Camera mainCam;

        private Vector2 inputVector = Vector2.zero;

        protected virtual void Start()
        {
            RangeValue = rangeValue;
            AllowedZone = allowedZone;

            canvasHolder = GetComponentInParent<Canvas>();
            if (canvasHolder == null)
                Debug.LogError("The Joystick is not placed inside a canvas");


            holder = GetComponent<RectTransform>();
            knob = transform.GetChild(0).GetComponent<RectTransform>();

            Vector2 center = new Vector2(0.5f, 0.5f);
            holder.pivot = center;
            knob.anchorMin = center;
            knob.anchorMax = center;
            knob.pivot = center;
            knob.anchoredPosition = Vector2.zero;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            mainCam = null;
            if (canvasHolder.renderMode == RenderMode.ScreenSpaceCamera)
                mainCam = canvasHolder.worldCamera;

            Vector2 position = RectTransformUtility.WorldToScreenPoint(mainCam, holder.position);
            Vector2 radius = holder.sizeDelta / 2;
            inputVector = (eventData.position - position) / (radius * canvasHolder.scaleFactor);
            InputsBasedOnAxisTypes();
            UpdateInputs(inputVector.magnitude, inputVector.normalized);
            knob.anchoredPosition = inputVector * radius * rangeValue;
        }


        public virtual void OnPointerUp(PointerEventData eventData)
        {
            inputVector = Vector2.zero;
            knob.anchoredPosition = Vector2.zero;
        }

        protected virtual void UpdateInputs(float magnitude, Vector2 normalised)
        {
            if (magnitude > allowedZone)
            {
                if (magnitude > 1)
                    inputVector = normalised;
            }
            else
                inputVector = Vector2.zero;
        }

        private void InputsBasedOnAxisTypes()
        {
            if (axisTypes == AxisTypes.Horizontal)
                inputVector = new Vector2(inputVector.x, 0f);
            else if (axisTypes == AxisTypes.Vertical)
                inputVector = new Vector2(0f, inputVector.y);
        }

        private float SnapInputs(float value, AxisTypes snapAxis)
        {
            if (value == 0)
                return value;

            if (axisTypes == AxisTypes.Both)
            {
                float angle = Vector2.Angle(inputVector, Vector2.up);
                if (snapAxis == AxisTypes.Horizontal)
                {
                    if (angle < 22.5f || angle > 157.5f)
                        return 0;
                    else
                        return (value > 0) ? 1 : -1;
                }
                else if (snapAxis == AxisTypes.Vertical)
                {
                    if (angle > 67.5f && angle < 112.5f)
                        return 0;
                    else
                        return (value > 0) ? 1 : -1;
                }

                return value;
            }
            else
            {
                if (value > 0)
                    return 1;
                if (value < 0)
                    return -1;
            }

            return 0;
        }
    

    public enum AxisTypes
    {
        Both,
        Horizontal,
        Vertical
    }
    public void GamePad()
    {
        //Input.GetAxisRaw("Vertical");
       Pitch = Input.GetAxis("Vertical");

        //Input.GetAxisRaw("Horizontal");
       Roll = Input.GetAxis("Horizontal");

        // -Input.GetAxisRaw("Yaw");
       Yaw = -Input.GetAxis("Yaw");


        // Input.GetAxisRaw("Throttle");
       Lift = Input.GetAxis("Throttle");
    }
    public class KeyInputs
    {
        public KeyCode rollLeft = KeyCode.A;
        public KeyCode rollRight = KeyCode.D;
        public KeyCode pitchForward = KeyCode.W;
        public KeyCode pitchBackward = KeyCode.S;

        public KeyCode yawLeft = KeyCode.LeftArrow;
        public KeyCode yawRight = KeyCode.RightArrow;

        public KeyCode liftUp = KeyCode.UpArrow;
        public KeyCode liftDown = KeyCode.DownArrow;
    }
}
