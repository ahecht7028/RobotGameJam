using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedrunTimer : MonoBehaviour
{
    public static float totalTime = 0;
    
    Text text;
    public bool isActiveScene;

    void Start()
    {
        text = GetComponent<Text>();
        UpdateText();
    }

    void Update()
    {
        if (isActiveScene)
        {
            totalTime += Time.deltaTime;
            UpdateText();
        }
    }

    public void StartNewGame()
    {
        totalTime = 0;
    }
    
    void UpdateText()
    {
        int mil = (int)(totalTime * 100) % 100;
        int sec = (int)(totalTime % 60);
        int min = (int)(totalTime / 60);

        string milZero = "0";
        string secZero = "0";
        string minZero = "0";
        if(mil > 9)
        {
            milZero = "";
        }
        if(sec > 9)
        {
            secZero = "";
        }
        if(min > 9)
        {
            minZero = "";
        }

        text.text = minZero + min + ":" + secZero + sec + "." + milZero + mil;
    }
}
