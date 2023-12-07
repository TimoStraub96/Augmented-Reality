using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Camerasnap : MonoBehaviour
{
  
    public GameObject animal;

   

    [SerializeField]public Dictionary<string, int> points = new();
    private  String temptag;
   
    public float timer = 2f;
    private float temp_timer;
    

    private void Start()
    {
       temp_timer = timer;
    }

    private void OnTriggerStay(Collider collider)
    {
        
    
            if ( collider.gameObject.GetComponent<Animal_Behavior>() != null)
            {
                if (collider.gameObject.GetComponent<Animal_Behavior>().snapped == false && collider.gameObject.tag != null)
                {
                    animal = collider.gameObject;
                    

                    if(timer > 0 ){
                        timer -= Time.deltaTime;
                    }
                    else if(timer <= 0 && animal.GetComponent<Animal_Behavior>().snapped == false){
                        timer = 2f;
                        animal.GetComponent<Animal_Behavior>().snapped = true;
                        temptag= animal.tag;
                        if(points.ContainsKey(temptag) ){
                            points[temptag] += 1;
                            
                        }else{
                            points.Add(temptag,1);
                        }
                    }
                }
                
            }
          
        
    }
    private void OnTriggerExit(Collider collider)
    {
        
            timer = temp_timer;
           
        
    }
  
}
