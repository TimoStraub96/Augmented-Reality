using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class MedalsController : MonoBehaviour
{
    public GameObject[] photomedals;
    public GameObject specialmedalPrefab;
    public GameObject firstinteractionmedalPrefab; //+ child for second interaction
    public GameObject allinteractionsmedalPrefab; //+ child for second interaction
    //get TextMeshProUGUI from medal
    public TextMeshProUGUI textanimal;

    //save the animals that are one of a kind
    public GameObject[] one_Of_A_kind_animal; 

    //the tags of the animals that are one of a kind
    private List<String> animaltags = new List<string>();
    [SerializeField] private List<GameObject> allanimalsinscene;
    public Dictionary<string, bool> discoveredAnimals = new Dictionary<string, bool>();

    //int for all interactions
    private int interactions_1 = 0;
    private int interactions_2 = 0;

    private PointsController pointsController; // Reference to the PointsController

    private void Start()
    {
        //get camera with MainCamera tag
        GameObject maincam = GameObject.FindGameObjectWithTag("MainCamera");
        //get pointscontroller from camera
            pointsController = maincam.GetComponent<PointsController>();
            InitializeDiscoveredAnimals();
            //hide all medals and child medals
            foreach (GameObject medal in photomedals)
            {
                    medal.SetActive(false);
                    medal.transform.GetChild(0).gameObject.SetActive(false);

            }
            
            //subscribe to event
            pointsController.OnSnapped += HandleSnappedEvent;

            //Subscribe to event clicked
            pointsController.OnInteraction += HandleInteractionEvent;
            /*




            //hide special medal
            specialmedalPrefab.SetActive(false);
            //hide first interaction medal and child
            firstinteractionmedalPrefab.SetActive(false);
            firstinteractionmedalPrefab.transform.GetChild(0).gameObject.SetActive(false);
            //hide all interaction medal
            allinteractionsmedalPrefab.SetActive(false);
            allinteractionsmedalPrefab.transform.GetChild(0).gameObject.SetActive(false);

            */
           
           
    }

    

    void Update()
    {
        //CheckPointsforeachanimal();
        
        
    }
    // Initializes the list of discovered animals
    private void InitializeDiscoveredAnimals()
    {
        foreach (GameObject animal in one_Of_A_kind_animal)
        {
            animaltags.Add(animal.tag);
            discoveredAnimals.Add(animal.tag, false);
        }
        //find every gameobject with tag from the list
        foreach (string tag in animaltags)
        {
            allanimalsinscene.AddRange(GameObject.FindGameObjectsWithTag(tag));
        }


    }
    //obsolete changed with event
/*
    public void CheckPointsforeachanimal(){

        foreach (GameObject animal in allanimalsinscene)
        {
            if (pointsController.points.ContainsKey(animal.tag) && pointsController.points[animal.tag] >= 1 && discoveredAnimals[animal.tag] == false)
            {
                
                //Show medal
                discoveredAnimals[animal.tag] = true;
                //check if medal has the same tag as animal but only show the parent
                foreach (GameObject medal in photomedals)
                {
                    if (medal.tag == animal.tag)
                    {
                        medal.SetActive(true);
                        
                    }
                }     
            }
            if (pointsController.points.ContainsKey(animal.tag) && pointsController.points[animal.tag] == 2 && discoveredAnimals[animal.tag] == true)
            {
                
                //check if medal has the same tag as animal but now show the child too if it has child
                foreach (GameObject medal in photomedals)
                {
                    if (medal.tag == animal.tag)
                    {
                        medal.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
                

            }
            //if all animals are discovered 
            if (pointsController.points.Count == animaltags.Count)
            {
                //show special medal
                specialmedalPrefab.SetActive(true);
            }
        }
    }
*/
    // Handles the event when an animal is snapped and checks if a medal should be shown
    private void HandleSnappedEvent(GameObject animal)
    {
        
        
            if (pointsController.points.ContainsKey(animal.tag) && pointsController.points[animal.tag] <= 1 && discoveredAnimals[animal.tag] == false)
            {
              
                 //Show medal
                discoveredAnimals[animal.tag] = true;
                //check if medal has the same tag as animal but only show the parent

                Debug.Log("animal has been discovered" + animal.tag);
                foreach (GameObject medal in photomedals)
                {
                    if (medal.tag == animal.tag)
                    {
                       // medal.SetActive(true);
                        
                    }
                }     
                
                
            }

             if (pointsController.points.ContainsKey(animal.tag) && pointsController.points[animal.tag] >= 2 && discoveredAnimals[animal.tag] == true)
            {

                  //check if medal has the same tag as animal but now show the child too if it has child
                  Debug.Log("animal has been discovered" + animal.tag);
                foreach (GameObject medal in photomedals)
                {
                    if (medal.tag == animal.tag)
                    {
                      //  medal.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }

            }
            Debug.Log("pointscontroller points count" + animaltags.Count);
            //if all animals are discovered 
            if (pointsController.totalpoints == animaltags.Count)
            {
               //show special medal
               Debug.Log("all animals have been discovered");
                //specialmedalPrefab.SetActive(true);
            }
       
    }

    //call this function when an animal is clicked and show medals
    private void HandleInteractionEvent(GameObject interactedAnimal)
    {
        
        //get animal from event
        GameObject animal = interactedAnimal;
        //get dictionary from pointscontroller
        Dictionary<string, int> interactionpoints = pointsController.interaction;
        //get the name from the animal
        string name = animal.name;
        //show medal for first interction with any animal
        if (interactionpoints.ContainsKey(name) && interactionpoints[name] == 1)
        {
            //show medal
            //firstinteractionmedalPrefab.SetActive(true);
            
        }
        //show medal for second interction with any animal
        if (interactionpoints.ContainsKey(name) && interactionpoints[name] >= 2)
        {
            
            //show medal
            //firstinteractionmedalPrefab.transform.GetChild(0).gameObject.SetActive(true);
            
        }
        //check all animals in scene if they have been interacted with
        
        if(interactionpoints.ContainsKey(name) && interactionpoints[name] == 1)
        {
            Debug.Log("animal has been interacted with" + name);
            interactions_1++;
            
        }
        if(interactionpoints.ContainsKey(name) && interactionpoints[name] == 2)
        {
            interactions_2++;
        }

        //if all animals have been interacted with at least once
        if (interactions_1 == allanimalsinscene.Count)
        {
            interactions_1=-1;
            Debug.Log("all animals have been interacted with at least once");
            //show medal
           // allinteractionsmedalPrefab.SetActive(true);   
        }
        //if all animals have been interacted with at least twice
        if (interactions_2 == allanimalsinscene.Count)
        { 
            interactions_2=-1;
            Debug.Log("all animals have been interacted with at least twice");
            //show medal
            //allinteractionsmedalPrefab.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}