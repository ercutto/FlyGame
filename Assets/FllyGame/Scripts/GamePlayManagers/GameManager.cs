using System;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            //Baska game Manager varsa o game manageri yok eder
            if(instance)
                Destroy(gameObject);
            else
                instance = this;

            DontDestroyOnLoad(gameObject);

        }
        public void NextStage(int ToStage)
        {
            ScenesManager.instance.SelectLevel(ToStage);
            
            if (state==states.menu) {NatureManager.instance.windZonePrefab.SetActive(false);}

            if (state == states.game) { NatureManager.instance.windZonePrefab.SetActive(true); }
        }

        public void BackToMainMenu()
        {
            state=states.menu;
            NatureManager.instance.windZonePrefab.SetActive(false);
            SceneManager.LoadScene(4);

        }

    }
}
