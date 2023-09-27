using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimensionrenderer : MonoBehaviour
{
    public GameObject maincam;
    

    // Update is called once per frame
   void OnTriggerStay(Collider collider)
    {
        Debug.Log(collider.gameObject.name);
        //check if collider is Big-Dimension
        
        
    }
    
}
