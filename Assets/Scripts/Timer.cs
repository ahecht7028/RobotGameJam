using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text text;
    public GameObject obj;
    public static float currentTime=0;
    public static int min=0;
    public static float sec=0;
    public int x = 0;
    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("TimerText");
        min= PlayerPrefs.GetInt("min");
        sec= PlayerPrefs.GetFloat("sec");
       currentTime=sec;
          if (sec<10){
        obj.GetComponent<UnityEngine.UI.Text>().text= min.ToString() + ":" +x.ToString()+ sec.ToString();//(currentTime).ToString();
    } else {
        obj.GetComponent<UnityEngine.UI.Text>().text= min.ToString() + ":" + sec.ToString();//(currentTime).ToString();
    }

        if (SceneManager.GetActiveScene().name.ToString() == "End Screen"){
        this.enabled = false;
        } else {
        this.enabled = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        sec=Mathf.Round(currentTime * 100f) / 100f;
        if(sec>60){
            sec-=60;
            currentTime-=60;
            min++;
        }

    if (sec<10){
        obj.GetComponent<UnityEngine.UI.Text>().text= min.ToString() + ":" +x.ToString()+ sec.ToString();//(currentTime).ToString();
    } else {
        obj.GetComponent<UnityEngine.UI.Text>().text= min.ToString() + ":" + sec.ToString();//(currentTime).ToString();
    }
    }
    
    void OnDisable()
    {
    //PlayerPrefs.SetInt("time", (int)currentTime);
    PlayerPrefs.SetInt("min", (int)min);
    PlayerPrefs.SetFloat("sec", sec);
    }

void OnEnable()
    {
    //currentTime  =  PlayerPrefs.GetInt("time");
    min= PlayerPrefs.GetInt("min");
    sec= PlayerPrefs.GetFloat("sec");
    }
 

}

 
