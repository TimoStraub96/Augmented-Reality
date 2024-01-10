using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MedalsController : MonoBehaviour
{
    private PointsController pointsController; // Reference to the PointsController
    //save the animals that are one of a kind
    public GameObject[] animalinscene; 

    //the tags of the animals that are one of a kind
    private List<String> animaltags = new List<string>();
    [SerializeField] private List<GameObject> allanimalsinscene;
    public Dictionary<string, bool> discoveredAnimals = new Dictionary<string, bool>();

    //int for all interactions
    private int interactions_1 = 0;
    private int interactions_2 = 0;


//UI elements
    private VisualElement rowsnapped;
    private List<Button> buttonssnapped; // Declare 'buttons' as a List<Button>

    private VisualElement rowinteracted;
    private List<Button> buttonsinteracted; // Declare 'buttons' as a List<Button>

    

    private void Start()
    {   
        //get container Photo
        rowsnapped = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Photo");
        var buttons = rowsnapped.Query<Button>().ToList();
        buttonssnapped = buttons;


        //get container firstinteraction
        rowinteracted = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Interaction");
        var buttonsinter = rowinteracted.Query<Button>().ToList();
        buttonsinteracted = buttonsinter;


        //get camera with MainCamera tag
        GameObject maincam = GameObject.FindGameObjectWithTag("MainCamera");
        //get pointscontroller from camera
        pointsController = maincam.GetComponent<PointsController>();
        InitializeDiscoveredAnimals();
            
        //subscribe to event
        pointsController.OnSnapped += HandleSnappedEvent;

        //Subscribe to event clicked
        pointsController.OnInteraction += HandleInteractionEvent;

           
           
    }

    // Initializes the list of discovered animals
    private void InitializeDiscoveredAnimals()
    {
        foreach (GameObject animal in animalinscene)
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
    // Handles the event when an animal is snapped and checks if a medal should be shown
    private void HandleSnappedEvent(GameObject animal)
    {
        
        //first time snapp animal
            if (pointsController.points.ContainsKey(animal.tag) && pointsController.points[animal.tag] <= 1 && discoveredAnimals[animal.tag] == false)
            {
                Button convertedButton;

                discoveredAnimals[animal.tag] = true;
                //Debug.Log("animal has been discovered" + animal.tag);
                //go through the first buttons and show the button with the same class as the animal
                for(int i = 0; i < buttonssnapped.Count/2; i++)
                {
                    convertedButton = buttonssnapped[i].ConvertTo<Button>();
                    
                    if(convertedButton.ClassListContains(animal.tag))
                    {
                        convertedButton.style.display = DisplayStyle.Flex;
                        //Debug.Log("button has been shown");
                    }
                }
                
                
                
            }
            //second time snapp animal
             if (pointsController.points.ContainsKey(animal.tag) && pointsController.points[animal.tag] >= 2 && discoveredAnimals[animal.tag] == true)
            {

                Button convertedButton;
                //go through the second buttons and show the button with the same class as the animal
                for(int i = buttonssnapped.Count/2; i < buttonssnapped.Count; i++)
                {
                    convertedButton = buttonssnapped[i].ConvertTo<Button>();
                    //log the class of the button
                    
                    if(convertedButton.ClassListContains(animal.tag))
                    {
                        //Debug.Log("button has the same class as animal");
                        convertedButton.style.display = DisplayStyle.Flex;
                        //Debug.Log("button has been shown");
                    }
                }
                

            }
            
            //if all animals are discovered 
            if (pointsController.points.Count == animaltags.Count)
            {
                var firstinteractionmedalPrefab = rowinteracted.Q<VisualElement>("AllAnimals");
                //get the button from the firstinteractionmedalPrefab
                Button button = firstinteractionmedalPrefab.Q<Button>();
                //button.style.display = DisplayStyle.Flex; //There is no button in UI
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
            Debug.Log("first interaction with animal: " + name );
            //get from rowinteracted the the firstAnimalInteractionmedal
            var firstinteractionmedalPrefab = rowinteracted.Q<VisualElement>("FirstAnimal");
            //get the button from the firstinteractionmedalPrefab
            Button button = firstinteractionmedalPrefab.Q<Button>();
            //show medal
            //button.style.display = DisplayStyle.Flex; //There is no button in UI
            
        }
        //show medal for second interction with any animal
        if (interactionpoints.ContainsKey(name) && interactionpoints[name] >= 2)
        {
            
            var firstinteractionmedalPrefab = rowinteracted.Q<VisualElement>("FirstMultipleAnimal");
            var allinter = rowinteracted.Q<VisualElement>("AllInteractions");
            Button buttoninter = allinter.Q<Button>();
            //get the button from the firstinteractionmedalPrefab
            Button button = firstinteractionmedalPrefab.Q<Button>();
            //button.style.display = DisplayStyle.Flex; //There is no button in UI
            /*
            if(buttoninter.style.display == DisplayStyle.None)
            {
               buttoninter.style.display = DisplayStyle.Flex; //no button in UI
            }
            */
        }
        //check all animals in scene if they have been interacted with
        
        if(interactionpoints.ContainsKey(name) && interactionpoints[name] == 1)
        {
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
            var firstinteractionmedalPrefab = rowinteracted.Q<VisualElement>("AllAnimalsFirst");
            //get the button from the firstinteractionmedalPrefab
            Button button = firstinteractionmedalPrefab.Q<Button>();
            //button.style.display = DisplayStyle.Flex; //There is no button in UI
        }
        //if all animals have been interacted with at least twice
        if (interactions_2 == allanimalsinscene.Count)
        { 
            interactions_2=-1;
            var firstinteractionmedalPrefab = rowinteracted.Q<VisualElement>("AllAnimalsSecond");
            //get the button from the firstinteractionmedalPrefab
            Button button = firstinteractionmedalPrefab.Q<Button>();
            //button.style.display = DisplayStyle.Flex; //There is no button in UI
        }
    }
}