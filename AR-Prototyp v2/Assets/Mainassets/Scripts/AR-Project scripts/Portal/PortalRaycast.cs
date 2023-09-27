using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class PortalRaycast : MonoBehaviour
{
    public GameObject objectPrefab; // Das zu platzierende Objekt

    private ARRaycastManager raycastManager;
    public Camera ARCamera;
    private GameObject placedobj;
    private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // check if the screen is touched but not ui element       
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject())
        {
                
            // Erzeuge einen Raycast vom Bildschirm aus
            Ray ray = ARCamera.ScreenPointToRay(Input.GetTouch(0).position);

            // Führe den AR Raycast durch
            if (raycastManager.Raycast(ray, raycastHits, TrackableType.PlaneWithinPolygon))
            {
                
                // Finde die Position und Rotation des getroffenen Punkts
                Pose pose = raycastHits[0].pose;
                // Berechne die Höhe des Objekts
                float objectHeight = objectPrefab.transform.localScale.y;

                // Passe die Platzierungshöhe an
                Vector3 placementPosition = new Vector3(pose.position.x,(-objectHeight),pose.position.z);
                if(placedobj != null){
                    Destroy(placedobj);
                }
                Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
                // Erzeuge das Objekt an der Position des getroffenen Punkts
                placedobj=Instantiate(objectPrefab, placementPosition, pose.rotation * rotation);
            }
        }
        
    }
}
