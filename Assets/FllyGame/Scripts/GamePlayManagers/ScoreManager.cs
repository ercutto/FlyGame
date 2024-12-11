using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText=null;
    public Text HealthText=null;

    int score = 0;
    int health = 100;

    public void Start()
    {
        instance = this;
    }
    public void AddScore(int add)
    {
        score += add;
        TypeTexts(scoreText,score);
        
    }
    public void TakeDamage(int add)
    {
        health += add;
        TypeTexts(HealthText,health);
        
    }

    void TypeTexts(Text text,int WhatToWrite)
    {
        text.text=WhatToWrite.ToString();
    }
}
