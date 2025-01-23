using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace RageRunGames.EasyFlyingSystem
{
    public class StatsManager : MonoBehaviour
    {
        public static StatsManager instance;
        [Header("Score")]
        public Text scoreText = null;
        float score = 0;

        [Space]
        [Header("HEALTH")]
        public Text HealthText = null;
        public float currentHealth = 100;
        public float Maxhealth = 100;
        private float minhealth = 0;

        [Space]
        [Header("Battery")]
        public Image batterImage = null;
        public Text BatteryText = null;
        public float currentCharge = 100;
        public float MaxCharge = 100;
        private float minCharge = 0;
        [Space]
        [Header("Wind")]
        public GameObject windDirectionUi=null;

        [Header("Mesages")]
        public Text MesageText= null;
        public Text messageText2= null;
        public string mesage=null;
        [Range(0f, 1f)] public float textingSpeed=0.5f;

        [Header("TimeCount")]
        public Text TimeText = null;
        public string timeMessage = null;
        public float currentTime = 100f;
        public float maxTime = 100f;
        public float minTime = 0f;
        public bool isPackageDelivered=false;


        public enum GameMode
        {
            None,
            Mission,
            Post,
        }
        public bool player;
        [Header("Select Game mode")]
        [SerializeField]
        public GameMode gameMode;
        private byte mode = 1;

        public void Start()
        {
            instance = this;

            if (MainStatManager.instance != null) 
            {
                MainStatManager.instance.stats=(MainStatManager.Stats)gameMode;
               
                //MainStatManager.instance.stats = (MainStatManager.Stats)mode;
            }
            
           
            
            ResetStats();
        }


        public void AddScore(float add)
        {
            score += add;
            TypeTexts(scoreText, score);
            mode = (byte)(MainStatManager.Stats)gameMode;
            if (MainStatManager.instance != null)
            {
                MainStatManager.instance.Score(score, mode);
            }
           

        }
        public void TakeDamage(float add, bool isPlayer)
        {


            if (currentHealth <= minhealth)
            {
                HealthText.text = "Dead!";
                return;
            }
            else
            {
                currentHealth -= add;
                TypeTexts(HealthText, currentHealth);
            }

        }

        public void BatteryUsage(float add)
        {


            if (currentCharge <= minCharge)
            {
                BatteryText.text = "BatteryFinished";
                return;
            }
            else
            {
                currentCharge -= add;



                batterImage.fillAmount = currentCharge / MaxCharge;

                TypeTexts(BatteryText, currentCharge);
            }

        }
        void TypeTexts(Text text, float WhatToWrite)
        {
            text.text = WhatToWrite.ToString();
        }


        private void ResetStats()
        {
            score = 0;
            currentHealth = Maxhealth;
            currentCharge = MaxCharge;
            batterImage.color = new Color(0.216f, 1, 0, 1);
            TypeTexts(scoreText, score);
            TypeTexts(HealthText, currentHealth);
            TypeTexts(BatteryText, currentCharge);
        }

        public void WriteMessage(string message,int type)
        {
            Text _type;
            if (type == 0)//address
            {
                _type = MesageText;
                Invoke(nameof(ClearText), 10f);
            }
            else
                _type = messageText2;

                StartCoroutine(TypingToUI(message,_type));
           
        }

        IEnumerator TypingToUI(string _message, Text type)
        {

            for (int i = 0; i < _message.Length; i++)
            {
                yield return new WaitForSeconds(textingSpeed);
                string delayedText = _message.Substring(0, i + 1);
                type.text = delayedText;

            }

        }

        void ClearText()
        {
            MesageText.text = "";
        }

        public void TimeCount(float _time )
        {
            maxTime=_time;
            currentTime=maxTime;
            StartCoroutine(TimeCounting(currentTime));
        }

        IEnumerator TimeCounting(float time)
        {
            

            currentTime=time;
            while(!isPackageDelivered && currentTime > minTime) {

                yield return new WaitForSeconds(1);
                currentTime--;
                TimeText.text = currentTime.ToString();

            }
            yield return null;
            StartCoroutine(AddScoreCount());
        }

        IEnumerator AddScoreCount()
        {   while (currentTime > minTime)
            {
                yield return new WaitForSeconds(0.01f);
                currentTime--;
                TimeText.text = currentTime.ToString();
                AddScore(100);
            }
            yield return null ;
        }

     
    }
}
