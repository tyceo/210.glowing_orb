using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //Declare Variables

    //UI Controller References
    [SerializeField] private GameObject up1;
    [SerializeField] private GameObject up2;
    [SerializeField] private GameObject down1;
    [SerializeField] private GameObject down2;
    [SerializeField] private GameObject left1;
    [SerializeField] private GameObject left2;
    [SerializeField] private GameObject right1;
    [SerializeField] private GameObject right2;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private float hoverHeight;
    [SerializeField] private float moveSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Apply hover height
        Vector3 spawnPosition = new Vector3(0, hoverHeight, 0);
        transform.position = spawnPosition;
    }

    //Normal update for standard button presses/input.
    void Update()
    {
        //Go back to main menu if player presses escape.
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
    }


    // Fixed update for movement 
    void FixedUpdate()
    {
        //WASD MOVEMENT =============================================================================================
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(gameObject.transform.forward * moveSpeed);  
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(gameObject.transform.forward * -moveSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(gameObject.transform.right * -moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(gameObject.transform.right * moveSpeed);
        }

        //CAMERA MOVEMENT ===========================================================================================
        //Get raycast of mouse pointer, find all overlapping ui movement objects.
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        //Debug.Log(results.Count);

        //Repeat through the list, if the index is one of the control objects, move accordingly.
        Vector3 newRotation = transform.eulerAngles;
        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject == up1)
            {
                newRotation.x += -0.25f;
            }
            if (results[i].gameObject == up2)
            {
                newRotation.x += -0.4f;
            }

            if (results[i].gameObject == down1)
            {
                newRotation.x += 0.25f;
            }
            if (results[i].gameObject == down2)
            {
                newRotation.x += 0.4f;
            }

            if (results[i].gameObject == left1)
            {
                newRotation.y += -0.25f;
            }
            if (results[i].gameObject == left2)
            {
                newRotation.y += -0.4f;
            }

            if (results[i].gameObject == right1)
            {
                newRotation.y += 0.25f;
            }
            if (results[i].gameObject == right2)
            {
                newRotation.y += 0.4f;
            }
        }
        //Apply Rotation
        newRotation.z = 0;
        transform.eulerAngles = newRotation;

    }

}
