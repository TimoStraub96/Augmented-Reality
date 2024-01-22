using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PointsController : MonoBehaviour
{
  
    

   

    [SerializeField]public Dictionary<string, int> points = new();
    [SerializeField]public Dictionary<string, int> interaction = new();
    [SerializeField]public Dictionary<string, int> interactiontag = new();

    public event Action<GameObject> OnSnapped;
    public event Action<GameObject> OnInteraction;
    

    public int totalpoints = 0;
    private  String temptag;
   
    public float timer = 2f;
    private float temp_timer;
    private GameObject animal;
    private GameObject portal;


    private void Start()
    {
       temp_timer = timer;
       portal = GameObject.FindGameObjectWithTag("Portal");
    }

    
    


    //if animal snapp function pointscontroller
    private void OnTriggerStay(Collider collider)
    {

    
        if (collider.gameObject.GetComponent<Animal_Behavior>() != null)
        {
            if (collider.gameObject.GetComponent<Animal_Behavior>().snapped == false && collider.gameObject.tag != null)
            {
                animal = collider.gameObject;
                

                if(timer > 0 ){
                    timer -= Time.deltaTime;
                }
                else if(timer < 0 && animal.GetComponent<Animal_Behavior>().snapped == false){
                    timer = 2f;
                    animal.GetComponent<Animal_Behavior>().snapped = true;

                    totalpoints += 1;

                    
                    temptag= animal.tag;
                    if(points.ContainsKey(temptag) ){
                        points[temptag] += 1;

                        
                        
                    }else{
                        points.Add(temptag,1);
                    }
                    // call the event
                    OnSnapped?.Invoke(animal);
                }
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {  
            timer = temp_timer;
    }

    public bool GetSnappedStatus(GameObject animal)
    {
        if (animal != null && animal.GetComponent<Animal_Behavior>() != null)
        {
            return animal.GetComponent<Animal_Behavior>().snapped;
        }

        return false;
    }


    //animal interaction Points for each animal function
    public void AnimalInteraction(Ray ray){
        RaycastHit hit;
        GameObject interactedAnimal;
        if (Physics.Raycast(ray, out hit))
        {
            //check if animal has animal behavior script
            if (hit.transform.gameObject.GetComponent<Animal_Behavior>() != null)
            {
                //get animal
                interactedAnimal = hit.transform.gameObject;

                Debug.Log("animal has been interacted with" + interactedAnimal.name);
                //if animal first interaction call InteractionAnimation
                if (interaction.ContainsKey(interactedAnimal.name) && interaction[interactedAnimal.name] < 3)
                {
                    //add 1 to interaction
                    interaction[interactedAnimal.name] += 1;
                    //call InteractionAnimation and put as parameter the interaction number
                    interactedAnimal.GetComponent<Animal_Behavior>().InteractionAnimation(interaction[interactedAnimal.name]);
                }else{
                    if(interaction.ContainsKey(interactedAnimal.name) == false)
                        //add animal to dictionary
                        interaction.Add(interactedAnimal.name,1);

                    //call InteractionAnimation and put as parameter the interaction number
                    interactedAnimal.GetComponent<Animal_Behavior>().InteractionAnimation(interaction[interactedAnimal.name]);
                }
                // add tag to dictionary for every animal that is interacted with and give one if all animals are interacted with at least once but max 3
                if (interactiontag.ContainsKey(interactedAnimal.tag) && interactiontag[interactedAnimal.tag] < 2)
                {
                    interactiontag[interactedAnimal.tag] += 1;
                }else{
                    if(interactiontag.ContainsKey(interactedAnimal.tag) == false)
                        interactiontag.Add(interactedAnimal.tag,1);
                }
                
                // call the event
                OnInteraction?.Invoke(interactedAnimal);                   
            }

        }
       
    }

    public void clicked(){
        //raycast from camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //call AnimalInteraction function
            AnimalInteraction(ray);
        }
        
        
    }
    void Update()
    {   
        

        // check if portal exists else find it
        if (portal != null)
        {
            //call clicked function
            clicked();
                
        }else{
            portal = GameObject.FindGameObjectWithTag("Portal");
        }
    }
}
