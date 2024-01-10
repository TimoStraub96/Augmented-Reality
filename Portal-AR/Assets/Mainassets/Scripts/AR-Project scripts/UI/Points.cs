using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text = null;
    private GameObject maincam;
    private String texts;
    private Dictionary<string, int> points;
    void Start()
    {
        maincam = GetComponent<Snaptimer>().cam;
        points = maincam.GetComponent<PointsController>().points;
    }

    // Update is called once per frame
    void Update()
    {
        texts = "";
        foreach (KeyValuePair<string, int> kvp in points)
        {
            texts += kvp.Key + ": " + kvp.Value + "\n";
        }
        text.text = texts;
       
        
    }
}
