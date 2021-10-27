using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float startTime = 60f;
    
    public Text timer;

    private void Update()
    {
        timer.color = new Color(0.9f, 1f, 0.1f);
        startTime -= 1 * Time.deltaTime;
        if(startTime<=0)
        {
            startTime=0;
            
        }
        timer.text = startTime.ToString("00");
    }

}
