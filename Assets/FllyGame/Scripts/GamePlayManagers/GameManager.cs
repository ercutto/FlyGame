using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace RageRunGames.EasyFlyingSystem
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        public static GameManager instance;

        [HideInInspector]
        public GameObject[] DeliveryLevel = null;
        [HideInInspector]
        public GameObject[] CheckPointLevel = null;
        [HideInInspector]
        public int SceneInt = 0;
        private bool[] levelBool= {false,false,false};
        bool complated=false;
        private bool[] CPlevelBool = { false, false, false };
        bool CPcomplated = false;
        public Camera displayCamera = null;
        public int _gameMode = 0;
        
        [Serializable]
        public enum states
        {
            menu,
            game,
            none,
        }
        public states state;
        #endregion
        public void Awake()
        {
            //Baska game Manager varsa o game manageri yok eder
            if (instance)
                Destroy(gameObject);
            else
                instance = this;

            DontDestroyOnLoad(gameObject);

            Invoke(nameof(ResetLevels), 2);

        }
           
        public void NextStage(int ToStage)
        {
            ScenesManager.instance.SelectLevel(ToStage);
            
            if (state==states.menu) {NatureManager.instance.windZonePrefab.SetActive(false);}

            if (state == states.game) { NatureManager.instance.windZonePrefab.SetActive(true); }
        }
        /// <summary>
        /// HER Bölum bittiginde oyuncu bu ana menuye döner ve GameManager bölumkerin acik olup olamadigini sahnelerde target managerlerden alir targetmanagerlerde sceneInt GameManagerin sceneIntine baglanir.Game Manager her Ana menuye geldiginde yedinden menu managerin level dugmelerinde yeni array olusturur ve game managerdeki guncellenmis verileri bu araydeki tus lara atar
        /// </summary>
        public void BackToMainMenu()
        {
            
            state=states.menu;
            NatureManager.instance.windZonePrefab.SetActive(false);
            int i = (int)ScenesManager.instance.gameType;
            SceneManager.LoadScene(4);
            if (i == 0)
            {
                //checkpoint
               
                Invoke(nameof(CheckPointGameLevels), 1);
            }
            if (i == 1)
            {
                //packageDelivery

            
               Invoke(nameof(DeliveryGameLevels),1);
            }

            ScenesManager.instance.gameType = (ScenesManager._gameType)states.none;

            Invoke(nameof(CheckLevelsComplatedOrNot),1);
        }


       
        void DeliveryGameLevels()
        {
            DeliveryLevel = MenuManager.instance.DeliveryLevel;

            //böklumlerin acik olup olmadigini kopntrol eder
            if (levelBool[0] == true && levelBool[1] == true && levelBool[2] == true)
                complated = true;

            if (!complated)
                UnlockLevel(DeliveryLevel, SceneInt,levelBool, true);
            else
            {

                UnlockLevel(DeliveryLevel, DeliveryLevel.Length,levelBool,true);
            }

            
        }

        void CheckPointGameLevels()
        {
            CheckPointLevel = MenuManager.instance.CheckPointLevel;
           
            //böklumlerin acik olup olmadigini kopntrol eder
            if (CPlevelBool[0] == true && CPlevelBool[1] == true && CPlevelBool[2] == true)
                CPcomplated = true;

            if (!CPcomplated)
                UnlockLevel(CheckPointLevel, SceneInt,CPlevelBool,true);
            else
            {

                UnlockLevel(CheckPointLevel, CheckPointLevel.Length,CPlevelBool, true);
            }


            
        }

        public void CheckLevelsComplatedOrNot()
        {
            DeliveryLevel = MenuManager.instance.DeliveryLevel;
            CheckPointLevel = MenuManager.instance.CheckPointLevel;

            Invoke(nameof(SetLevelsCurrentStates), 1);
        }


        void SetLevelsCurrentStates()
        {
            ShowLevelStates(CPlevelBool, CheckPointLevel);
            ShowLevelStates(levelBool, DeliveryLevel);
        }

        public void UnlockLevel(GameObject[] array, int unlockedLevel, bool[] bools, bool unlocked)
        {
           
            for (int i = 0; i < array.Length; i++)
            {
                

                if (i <= unlockedLevel)
                {
                    LevelState _levelState = array[i].GetComponent<LevelState>();
                  
                    _levelState.Unlock();
                     bools[i]=unlocked;
                }
                else
                {       
                    LevelState _levelState = array[i].GetComponent<LevelState>();
                    _levelState.Lock();
                    bools[i] = false;
                }
                
            }
 
        }

        void ShowLevelStates(bool[] levels, GameObject[] arrays)
        {

            for (int i = 0; i < levels.Length; i++)
            {
                if (levels[i] == true)
                {
                    arrays[i].GetComponent<LevelState>().Unlock();
                }
                else
                {
                    arrays[i].GetComponent<LevelState>().Lock();
                }
            }
        }

        public void ResetLevels()
        {
            MainStatManager.instance.CloseCanvas();

            DeliveryLevel = MenuManager.instance.DeliveryLevel;
            CheckPointLevel = MenuManager.instance.CheckPointLevel;
            UnlockLevel(DeliveryLevel, 0, levelBool, true);
            UnlockLevel(CheckPointLevel, 0, CPlevelBool, true);
        }
    }
}
