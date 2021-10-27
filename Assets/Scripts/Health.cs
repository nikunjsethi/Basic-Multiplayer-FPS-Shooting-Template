using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Health : MonoBehaviourPunCallbacks
{
    public static float  hp = 100;
    private float health;
    public Image healthbar;
    public Text heal;
    public GameObject ulost;
    public GameObject uwin;
   

    void Start()
    {
        health = hp;
        // heal.text = health.ToString();
        healthbar.fillAmount = health/100;
    }

   
   /* void Update()
    {
        heal.text = hp + "";
        if(hp <= 0)
        {
            hp = 0;
            ulost.SetActive(true);
        }
    } */

    [PunRPC]
    public void TakeDamageFromBullet(float damage)
    {
        if(health<50)
        {
            //healthbar.GetComponent<Image>().color = new Color(0.8, 0, 0);
            healthbar.color = new Color(0.8f, 0, 0);
            
        }
        if (health<= 10)
        {
            health = 10;
            if (photonView.IsMine)
            {
                ulost.SetActive(true);
                Time.timeScale = 0;
                
            }
            else
            {
                uwin.SetActive(true);
                Time.timeScale = 0;
                
            } 
        }
        
        
            health -= damage;
        //heal.text = health.ToString();
        healthbar.fillAmount=health/100;
        
        Debug.Log(hp);

       
    }
}
