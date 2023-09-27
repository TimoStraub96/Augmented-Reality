using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class PortalManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject maincam;
    public GameObject ARWorld; // wird erst später eingesetzt

    public Material portalmat;

    // Update is called once per frame
    void Start(){
        maincam = GameObject.Find("AR Camera");
    }
    void OnTriggerStay(Collider collider)
    {
        Vector3 campositioninPortalspace = transform.InverseTransformPoint(maincam.transform.position);
        Debug.Log(campositioninPortalspace.y);
        if(campositioninPortalspace.y <2f){
            
                portalmat.SetInt("_StencilComp",(int)CompareFunction.Always);
            
        }else{
           
                portalmat.SetInt("_StencilComp",(int)CompareFunction.Equal);
            
        }
    }
}
