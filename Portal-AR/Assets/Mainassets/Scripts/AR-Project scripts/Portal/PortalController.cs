using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Rendering;
using System;

public class PortalController : MonoBehaviour
{
    //reference to the portalmanager
    private PortalManager portalManager;
    [SerializeField]private GameObject maincam;

    private bool isInside = false;
    //start function
    void Start(){
        //find the portalmanager
        portalManager = GameObject.Find("Maskplane").GetComponent<PortalManager>();
        //find the maincam
        maincam = GameObject.FindGameObjectWithTag("MainCamera");
        //subscribe to event
        portalManager.inside += HandleInsideEvent;

        
    }
    //function to handle the event
    private void HandleInsideEvent(bool isInside){

        this.isInside = isInside;
    }
    //ontrigger stay function
    void OnTriggerExit(Collider collider){
        //if the player is inside the portal and is getting out of it without using the door then set stencilcomp to equal
        if(isInside == true && collider.gameObject.tag == "MainCamera"){
            //deaktivate the portal
            portalManager.deactivatePortal();

            //get childmaterial from portalmanager
            List<Material> childmaterial = portalManager.getChildMaterial();
            //set the stencilcomp to equal
            foreach(Material m in childmaterial){
                m.SetInt("_StencilComp",(int)CompareFunction.Equal);
            }
            
        
            //set the inside bool to false
            isInside = false;
            portalManager.setIsInside(false);
        }
    }
    	

   
}
