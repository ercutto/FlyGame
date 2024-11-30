using UnityEngine;

namespace RageRunGames.EasyFlyingSystem
{
    public class MouseCursor : MonoBehaviour
    {
        [SerializeField] RectTransform cursor;

        private void LateUpdate()
        {
            cursor.position = Input.mousePosition;
        }
    }

}