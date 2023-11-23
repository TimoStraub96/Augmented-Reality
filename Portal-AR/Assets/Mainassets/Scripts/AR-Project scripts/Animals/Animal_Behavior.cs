using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Animal_Behavior : MonoBehaviour
{
    public float rotationSpeed = 360.0f; // Animal's rotation speed (degrees per second)
    public float minRotationTime = 1.0f; // Minimum rotation time in seconds
    public float maxRotationTime = 5.0f; // Maximum rotation time in seconds
    public float MaxmoveSpeed = 3.0f; // Animal's speed
    public float minMoveTime = 1.0f; // Minimum movement time in seconds
    public float maxMoveTime = 5.0f; // Maximum movement time in seconds
    private float currentActionTime;
    public float waitbetweenmove = 1.0f; // Minimum wait time in seconds
    public GameObject field;
    private bool isRotating = false;
    private Vector3 moveDirection;
    private Vector3 newDirection;
    private bool isLeaving = false;
    private float isLeavingPause = 2f;
    [SerializeField]private float moveSpeed;
    void Start()
    {
        // Start the Animal's action (rotation or movement)
        StartCoroutine(PerformAction());
    }

    IEnumerator PerformAction()
    {
        
        while (true)
        {
            // Generate a random number between 0 and 1
            float randomValue = Random.value;
            
            float timer = 0f;
           
            yield return new WaitForSeconds(waitbetweenmove);
            if (randomValue < 0.3f) //  chance for rotation
            {
                isRotating = true;
                currentActionTime = Random.Range(minRotationTime, maxRotationTime);
                

                // Rotate the Animal randomly
                Quaternion randomRotate = Quaternion.Euler(0, Random.Range(0, 360), 0);
                Quaternion startRotation = transform.rotation;
                Quaternion endRotation = startRotation * randomRotate;
                float startTime = Time.time;

                while (Time.time < startTime + currentActionTime)
                {
                    float t = (Time.time - startTime) / currentActionTime;
                    transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
                    yield return null;
                }

                isRotating = false;
            }
            else //  chance for movement
            {
                // Choose a random movement direction
                moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

                // Choose a random movement time
                currentActionTime = Random.Range(minMoveTime, maxMoveTime);
                moveSpeed = Random.Range(1f, MaxmoveSpeed);

                // Rotate the Animal to the movement direction along the Y-axis
                RotateToDirection(moveDirection);

                yield return new WaitForSeconds(1f);
                while (timer < currentActionTime)
                {
                    if (isLeaving)
                    {
                        
                        yield return new WaitForSeconds(isLeavingPause);
                        moveDirection = newDirection;
                        yield return new WaitForSeconds(2f);
                        isLeaving = false;
                    }
                    
                    // Move the Animal in the current direction
                    transform.position += moveDirection * moveSpeed * Time.deltaTime;
                    timer += Time.deltaTime;
                    yield return null;
                }

                // Stop the movement
                moveDirection = Vector3.zero;
                float randomWaitTime = Random.Range(1.0f, 5.0f);
                yield return new WaitForSeconds(randomWaitTime);
            }
        }
    }

    

    void RotateToDirection(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MoveArea"))
        {
            // move back to the field
            newDirection = RandomDirectionInField();
            RotateToDirection(newDirection);
            isLeaving = true;
        
        }
        
        
    }
    Vector3 RandomDirectionInField(){
        Vector3 fieldCenter = field.GetComponent<Collider>().bounds.center ;
        Vector3 fieldExtents =   field.GetComponent<Collider>().bounds.extents;

    // generate a random position inside the field
        Vector3 randomPositionInsideField = new Vector3(
        Random.Range(fieldCenter.x - fieldExtents.x, fieldCenter.x + fieldExtents.x),
        transform.position.y,
        Random.Range(fieldCenter.z - fieldExtents.z, fieldCenter.z + fieldExtents.z)
        );

    // calculate the direction from the Animal to the random position
    Vector3 randomDirectionInsideField = (randomPositionInsideField - transform.position).normalized;

    return randomDirectionInsideField;
    }
    
    void Update()
    {
        if (isRotating)
        {
            // Perform the rotation
            // Rotation is already handled in PerformAction, so nothing is done here.
        }
        else
        {
            RotateToDirection(moveDirection);
        }
    }

}

