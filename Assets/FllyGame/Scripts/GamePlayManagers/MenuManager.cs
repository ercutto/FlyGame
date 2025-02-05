using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static RageRunGames.EasyFlyingSystem.ScenesManager;
using UnityEngine.SceneManagement;
namespace RageRunGames.EasyFlyingSystem
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager instance;

        [Header("Menus")]
        public GameObject StartMenu = null;
        public GameObject packageDeliveryScenemenu = null;
        public GameObject CheckPointgameSceneMenu = null;

        [Header("Buttons")]
        [Tooltip("This button is not active when StartMenu is active")]

        [Header("Level Buttons")]
        [Space(4)]
        [Header("DeliveryLevels")]
        public GameObject[] DeliveryLevel = new GameObject[2];
        [Header("CheckPointLevels")]
        public GameObject[] CheckPointLevel = new GameObject[2];

        [Space(5)]
        public GameObject returnButton = null;

        GameObject[] menus = new GameObject[3];
        public void Awake()
        {
            //
            if (instance)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;

            menus[0] = StartMenu;
            menus[1]=CheckPointgameSceneMenu;
            menus[2]=packageDeliveryScenemenu;

            SelectMenu(0);
        }


        public void SelectMenu(int menu)
        {
            
            EventSystem eventSystem = EventSystem.current;
            


            for (int i = 0; i < menus.Length; i++)
            {
                if (menu == i)
                {
                    

                    GameObject activeMenu= menus[i];
                    activeMenu.SetActive(true);
                    GameObject currentButton = activeMenu.transform.GetChild(0).gameObject;
                    eventSystem.SetSelectedGameObject(currentButton);
                }
                else
                {
                    menus[i].gameObject.SetActive(false);
                }

                //bu return tusunu aktif yapiyor.

                if (menu == 0) returnButton.SetActive(false);
                else returnButton.SetActive(true);
            }

        }

        public void SelectLevel(int Stage)
        {
            if (GameManager.instance)
            {
               ScenesManager.instance.SelectLevel(Stage);
            }
            else
            {
                SceneManager.LoadScene(Stage);
            }
        }

    }
}