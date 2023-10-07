using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Rendering;
public class PortalManager : MonoBehaviour
{
    public GameObject maincam;
    public GameObject ARWorld; 
    [SerializeField]private Renderer[] childRenderer;
    [SerializeField]private List<Material> childmaterial =new();

  
    void Start(){
        maincam = GameObject.Find("AR Camera");
   
        childRenderer = ARWorld.GetComponentsInChildren<Renderer>();
        foreach(Renderer r in childRenderer){
                

            foreach(Material m in r.materials){
                    childmaterial.Add(m);
            }

          
            
                
                
        }
    }
    void OnTriggerStay(Collider collider)
    {
        Vector3 campositioninPortalspace = transform.InverseTransformPoint(maincam.transform.position);
        Debug.Log(campositioninPortalspace.y);
        if(campositioninPortalspace.y <2f){
           
        
           
            foreach(Material m in childmaterial){
                m.SetInt("_StencilComp",(int)CompareFunction.Always);
            }
                  
                
            
        }else{
            
           foreach(Material m in childmaterial){
                m.SetInt("_StencilComp",(int)CompareFunction.Equal);
            }
                
            
        }
    }
}
