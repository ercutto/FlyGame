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
        public Camera displayCamera = null;

        
        [Serializable]
        public enum states
        {
            menu,
            game,
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
                Debug.Log("Checkpoint");
            }
            if (i == 1)
            {
                //packageDelivery

            
               Invoke(nameof(DeliveryGameLevels),1);
            }

            //Invoke(nameof(ResetLevels), 2);

           

        }

        public void ResetLevels()
        {
            MainStatManager.instance.CloseCanvas();

            DeliveryLevel =MenuManager.instance.DeliveryLevel;
            CheckPointLevel = MenuManager.instance.CheckPointLevel;
            UnlockLevel(DeliveryLevel, 0);
            UnlockLevel(CheckPointLevel, 0);
        }

        void DeliveryGameLevels()
        {
            DeliveryLevel = MenuManager.instance.DeliveryLevel;

            //böklumlerin acik olup olmadigini kopntrol eder
            if (levelBool[0] == true && levelBool[1] == true && levelBool[2] == true)
                complated = true;

            if (!complated)
                UnlockLevel(DeliveryLevel, SceneInt);
            else
            {

                UnlockLevel(DeliveryLevel, DeliveryLevel.Length);
            }
        }

        public void UnlockLevel(GameObject[] array, int unlockedLevel)
        {
           
            for (int i = 0; i < array.Length; i++)
            {
                GameObject go= array[i].gameObject;
                go.SetActive(false);
                Button _button = array[i].GetComponent<Button>();
                LevelState _levelState = array[i].GetComponent<LevelState>();

                if (i <= unlockedLevel)
                {                  
                    Color color = Color.white;
                    ApplyStatesToButtons(_button, true, _levelState, color, true, false, true,go);

                    levelBool[i]=true;
                }
                else
                {                 
                    Color color = Color.gray;
                    ApplyStatesToButtons(_button, false, _levelState, color, false, true, false,go);

                }
            }
 
        }

       
        //levelleri secerken guncel durumlarina göre:tus aktif jada pasif, rengi,acik mi,acik kilitmi yoksa kapali kilitmi gösteriliyor
        void ApplyStatesToButtons(Button button,bool enableButton, LevelState levelState, Color currentColor,bool isOpen ,bool keypadClose, bool keypadOpen,GameObject go)
        {
            button.enabled = enableButton;
            levelState.image.color = currentColor;
            levelState.isOpen = isOpen;
            levelState.keypadClose.gameObject.SetActive(keypadClose);
            levelState.keypadOpen.gameObject.SetActive(keypadOpen);
            go.SetActive(true);

            MainStatManager.instance.CloseCanvas();
        }
        //public void SetBoolsOfLevelStates(GameObject[] array,int ComplatedLevel)
        //{
        //    array[ComplatedLevel].GetComponent<LevelState>().isOpen = true;
        //    array[ComplatedLevel+1].GetComponent<LevelState>().isOpen = true;
        //}

    }
}
