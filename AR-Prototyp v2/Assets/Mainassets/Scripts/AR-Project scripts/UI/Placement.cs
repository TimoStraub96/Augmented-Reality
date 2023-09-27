using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Placement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text = null;
  

    // Update is called once per frame
    void Update()
    {
     
        //smooth fade in and out text every 2sec
        float lerp = Mathf.PingPong(Time.time, 2) / 2;
        
        //check if portal is spawned 
        if (GameObject.FindWithTag("Portal") != null)
        {
            text.color = new Color(1, 1, 1, 0);
            //stop script
            this.enabled = false;
        }
         else{
            text.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 0.8f), lerp);
        }


    
        
    }
}
