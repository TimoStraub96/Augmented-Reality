using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Animal_On_Tap : MonoBehaviour
{
    private Animator animator;
    public Camera mainCamera;
    public TMP_Text infoText;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        infoText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Began)
        {
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out _))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                animator.SetTrigger("OnTap");
            }
            infoText.gameObject.SetActive(true);
        }
    }
}
