using System;
using UnityEngine;
namespace RageRunGames.EasyFlyingSystem
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
      
        public Camera displayCamera = null;
        [Serializable]
        public enum states
        {
            menu,
            game,
        }
        public states state;

        public void Awake()
        {
            if (instance)
            {
                Destroy(instance.gameObject);
                return;
            }
            instance = this;

            DontDestroyOnLoad(instance.gameObject);

            
            
        }
        public void NextStage()
        {
            ScenesManager.instance.SelectLevel(2);
            
            if (state==states.menu) {NatureManager.instance.windZonePrefab.SetActive(false);}

            if (state == states.game) { NatureManager.instance.windZonePrefab.SetActive(true); }
        }

    }
}
