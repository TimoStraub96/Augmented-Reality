using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderController : MonoBehaviour
{
    // [SerializeField] private TextMeshProUGUI slidertext = null;
    public Slider slider;
    public GameObject[] portals;  //array of portals
    public GameObject marker; //array of images on slider
    private GameObject ARSessionOrigin;
    void Start(){
        ARSessionOrigin = GameObject.Find("AR Session Origin");
        slider.value = 0;
       
        ARSessionOrigin.GetComponent<PortalRaycast>().objectPrefab = portals[0];
        slider.maxValue = portals.Length;
        float sliderwidth = slider.GetComponent<RectTransform>().rect.width;
        float sliderheight = slider.GetComponent<RectTransform>().rect.height;

        // initialize images as children on slider with correct positions
        for(int i = 0; i < portals.Length; i++){
            GameObject newMarker = Instantiate(marker, new Vector3(0,0,0), Quaternion.identity);
            newMarker.transform.SetParent(slider.transform);
            
            newMarker.transform.localPosition = new Vector3((sliderwidth/(portals.Length))*(i+1)-sliderwidth/2+(-5),0,0); 
            //change marker height to slider height

            
            newMarker.transform.localScale = new Vector3(1,1,1);
            //change width of marker and hight accordingly
            newMarker.GetComponent<RectTransform>().sizeDelta = new Vector2(10,sliderheight-sliderheight/2);
            newMarker.transform.localRotation = Quaternion.identity;
            newMarker.transform.SetSiblingIndex(i+1);
        }
        Debug.Log("test");
       
    }
    void Update()
    {
      
         
   
        if((int)slider.value<1){
                // slidertext.text = "Kein Portal ausgewählt";
                ARSessionOrigin.GetComponent<PortalRaycast>().objectPrefab = portals[0];
        }
        else{
            if((int)(slider.value) >=1 && (int)(slider.value) <= portals.Length){
                    // slidertext.text =portals[(int)slider.value-1].name;
                    ARSessionOrigin.GetComponent<PortalRaycast>().objectPrefab = portals[(int)slider.value -1];
            }
                
        }
        
    }
    public void SliderChange(){
        //destroy any existing portals
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Portal");
          for(var i = 0 ; i < gameObjects.Length ; i ++)
          {
              Destroy(gameObjects[i]);

          }

    }
}
