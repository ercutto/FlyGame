using UnityEngine;

namespace RageRunGames.EasyFlyingSystem
{
    public class FullMouseLookInputHandler : BaseInputHandler, IInputHandler
    {
        public enum YawMode
        {
            Mouse,
            Keyboard
        }

        [SerializeField] YawMode yawMode = YawMode.Mouse;

        [SerializeField] float lerpSpeed = 10f;
        [SerializeField] float _deadZoneRadius = 0.1f;

        Vector2 ScreenCenter => new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        [SerializeField] private KeyInputs keyInputs;

        public void HandleInputs()
        {
            Vector2 mousePosition = Input.mousePosition;
            float yaw = (mousePosition.x - ScreenCenter.x) / ScreenCenter.x;
            float pitch = (mousePosition.y - ScreenCenter.y) / ScreenCenter.y;

            Pitch = Mathf.Abs(pitch) > _deadZoneRadius ? pitch : 0f;


            if (yawMode == YawMode.Mouse)
            {
                Yaw = Mathf.Abs(yaw) > _deadZoneRadius ? yaw : 0f;
                Roll = Yaw;
            }
            else
            {
                if (Input.GetKey(keyInputs.rollLeft))
                {
                    Roll = Mathf.Lerp(Roll, -1f, Time.deltaTime * lerpSpeed);
                    Yaw = Mathf.Lerp(Yaw, -1f, Time.deltaTime * lerpSpeed);
                }
                else if (Input.GetKey(keyInputs.rollRight))
                {
                    Roll = Mathf.Lerp(Roll, 1f, Time.deltaTime * lerpSpeed);
                    Yaw = Mathf.Lerp(Yaw, 1f, Time.deltaTime * lerpSpeed);
                }
            }

            if (Input.GetKey(keyInputs.liftUp))
            {
                Lift = Mathf.Lerp(Lift, 1f, Time.deltaTime * lerpSpeed);
            }
            else
            {
                Lift = Mathf.Lerp(Lift, 0f, Time.deltaTime * lerpSpeed);
            }


            EvaluateAnyKeyDown();
        }
    }
}