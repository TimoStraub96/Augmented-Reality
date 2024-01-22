using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Rendering;
using System;
public class PortalManager : MonoBehaviour
{
    public GameObject maincam;
    public GameObject ARWorld; 

    public event Action<bool> inside;

    [SerializeField] private bool isInside = false;
    private GameObject UI;
    [SerializeField]private Renderer[] childRenderer;
    [SerializeField]private List<Material> childmaterial =new();

  
    void Start(){
        //get UI through layer
        UI = GameObject.Find("UI");
        //aktivate the UI medal script
        UI.GetComponent<MedalsController>().enabled = true;
        maincam = GameObject.FindGameObjectWithTag("MainCamera");
        ARWorld = GameObject.FindGameObjectWithTag("Room");

   
        childRenderer = ARWorld.GetComponentsInChildren<Renderer>();
        foreach(Renderer r in childRenderer){
                

            foreach(Material m in r.materials){
                    childmaterial.Add(m);
                    m.SetInt("_StencilComp",(int)CompareFunction.Equal);

            }

          
            
                
                
        }
        maincam.transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
    }
    void OnTriggerStay(Collider collider)
    {
        Vector3 campositioninPortalspace = transform.InverseTransformPoint(maincam.transform.position);
        if(campositioninPortalspace.y <2f){
           
        
           
            foreach(Material m in childmaterial){
                m.SetInt("_StencilComp",(int)CompareFunction.Always);
            }
            
            activatePortal();

            
            
            if(isInside == false){
                isInside = true;
                inside?.Invoke(isInside);
            }
        }else{
            
           foreach(Material m in childmaterial){
                m.SetInt("_StencilComp",(int)CompareFunction.Equal);
            }
            if(isInside == true){
                isInside = false;
                inside?.Invoke(isInside);
            }
            deactivatePortal();
            
            
              
            
        }  
    }

    public void deactivatePortal(){
            //activate the portalRaycast script from maincam parent
            maincam.transform.parent.GetComponent<PortalRaycast>().enabled = true;
            //deactivate the points script from maincam
            maincam.GetComponent<PointsController>().enabled = false;
            //deactivate the capsulecolider from maincam child
            maincam.transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
    }
    public void activatePortal(){
        //deactivate the portalRaycast script from maincam parent
            maincam.transform.parent.GetComponent<PortalRaycast>().enabled = false;

            //activate the points script from maincam
            maincam.GetComponent<PointsController>().enabled = true;

            //activate the capsulecolider from maincam child
            maincam.transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
    }
    //getter for isInside
    public bool getIsInside(){
        return isInside;
    }
    //setter for isInside
    public void setIsInside(bool isInside){
        this.isInside = isInside;
    }

    //getter for childmaterial
    public List<Material> getChildMaterial(){
        return childmaterial;
    }
}