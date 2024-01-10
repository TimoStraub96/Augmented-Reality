using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Snaptimer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cam;
    private float timer;
    //public float flash_timer = 0.5f;
    
    
    public RawImage flash;
    //private bool flashed = false;
    [SerializeField] private TextMeshProUGUI text = null;

    private PointsController pointsController;

    void Start()
    {
        
        //get camera with MainCamera tag
        GameObject maincam = GameObject.FindGameObjectWithTag("MainCamera");
        //get pointscontroller from camera
        pointsController = maincam.GetComponent<PointsController>();
        //subscribe to event
        pointsController.OnSnapped += flashon;
    }

   
    void Update()
    {
        timer = cam.GetComponent<PointsController>().timer;
        text.text = ""+ timer;

        // obsolete change with event
        /*if (timer <= 0){
            flashon();
        }
        */
        //obsolete changed with animation
        /* if (flashed)
        {
            flash.color = new Color(1, 1, 1, flash.color.a - Time.deltaTime * flash_timer);
        }
        if (flash.color.a <= 0)
        {
            flashed = false;
        }
        */
    }

    public void flashon(GameObject animal)
    {
        //flash.color = new Color(1, 1, 1, 1);
        //flashed = true;
        flash.GetComponent<AudioSource>().Play();
        flash.GetComponent<Animation>().Play();
    }
}
