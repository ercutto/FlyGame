using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class LineRendererController : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        public Transform startPositon = null;
        public Transform endPositon = null;
        private BoxCollider coll;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            lineRenderer.SetPosition(0, startPositon.position);
            lineRenderer.SetPosition(1, endPositon.position);
         

        }

    }
}
