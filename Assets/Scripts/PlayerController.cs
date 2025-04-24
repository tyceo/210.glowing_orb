using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //Declare Variables

    //UI References
    [SerializeField] private GameObject up1;
    [SerializeField] private GameObject up2;
    [SerializeField] private GameObject down1;
    [SerializeField] private GameObject down2;
    [SerializeField] private GameObject left1;
    [SerializeField] private GameObject left2;
    [SerializeField] private GameObject right1;
    [SerializeField] private GameObject right2;
    [SerializeField] private GameObject centerCrosshair;
    [SerializeField] private GameObject pointerCrosshair;
    [SerializeField] private TextMeshProUGUI healthUI;
    [SerializeField] private TextMeshProUGUI laserUI;
    [SerializeField] private TextMeshProUGUI beamUI;

    //Lasers
    [SerializeField] private GameObject laserProjectile;
    [SerializeField] private GameObject beamProjectile;

    //Stats
    [SerializeField] private float hoverHeight;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float health;

    //Ammo
    [SerializeField] private float laserAmmoMaximum;
    [SerializeField] private float laserAmmoCurrent;
    [SerializeField] private float laserAmmoRegeneration;
    private float laserAmmoRecharge = 0;

    [SerializeField] private float beamAmmoMaximum;
    [SerializeField] private float beamAmmoCurrent;
    [SerializeField] private float beamAmmoRegeneration;
    private float beamAmmoRecharge = 0;
    private float timeSinceBeam = 0;

    //Self References
    [SerializeField] private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Apply hover height
        Vector3 spawnPosition = new Vector3(0, hoverHeight, 0);
        transform.position = spawnPosition;

        //Apply ammo
        laserAmmoCurrent = laserAmmoMaximum;
        
    }

    //Normal update for standard button presses/input.
    void Update()
    {
        //Move the pointer crosshair to mouse position.
        pointerCrosshair.transform.position = Input.mousePosition;

        //FIRING =======================================================================================================
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

        if (Input.GetMouseButtonDown(0))
        {
            //If player has ammo
            if (laserAmmoCurrent >= 1)
            {
                //Fire laser at mouse
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 dir;
                if (Physics.Raycast(ray, out RaycastHit hit, 10000f))
                {
                    dir = hit.point - transform.position;
                    var frontOfPlayer = transform.position + transform.forward * 8f;
                    var laserShot = Instantiate(laserProjectile, frontOfPlayer, transform.rotation);
                    laserShot.transform.LookAt(dir);
                }

                laserAmmoCurrent -= 1;
            }
        }

        if (Input.GetMouseButton(1))
        {
            //If player has ammo
            if (beamAmmoCurrent >= 1)
            {
                timeSinceBeam = 0;
                //Fire beam at center
                var frontOfPlayer = transform.position + transform.forward * 10f;
                var beamShot = Instantiate(beamProjectile, frontOfPlayer, transform.rotation);

                //Apply backwards momentum
                rb.AddForce(gameObject.transform.forward * -10);

                beamAmmoCurrent -= 1f;
            }
        }
        else
        {
            timeSinceBeam += 1;
        }

        //AMMO RECHARGE =================================================================================================
        if (laserAmmoCurrent < laserAmmoMaximum)
        {
            laserAmmoRecharge += 1;
            if(laserAmmoRecharge >= laserAmmoRegeneration)
            {
                laserAmmoCurrent += 1;
                laserAmmoRecharge = 0;
            }
        }

        if(laserAmmoCurrent > laserAmmoMaximum)
        {
            laserAmmoCurrent = laserAmmoMaximum;
        }

        if ((beamAmmoCurrent < beamAmmoMaximum) && timeSinceBeam >= 500)
        {
            beamAmmoRecharge += 1;
            if (beamAmmoRecharge >= beamAmmoRegeneration)
            {
                beamAmmoCurrent += 1;
                beamAmmoRecharge = 0;
            }
        }

        if (beamAmmoCurrent > beamAmmoMaximum)
        {
            beamAmmoCurrent = beamAmmoMaximum;
        }


        //Update Ui
        laserUI.text = "Laser: " + laserAmmoCurrent;
        beamUI.text = "Beam: " + Mathf.FloorToInt(beamAmmoCurrent);
        healthUI.text = "Health: " + health;

        //If the player has no health, return to main menu.
        if (health <= 0)
        {
            SceneManager.LoadScene("Title");
        }

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

    //Collision with enemy projectiles
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile") 
        {
            if ((collision.gameObject.GetComponent("HelicopterProjectile") as HelicopterProjectile) != null)
            {
                health -= collision.gameObject.GetComponent<HelicopterProjectile>().damage;
                Destroy(collision.gameObject);
            }

            if ((collision.gameObject.GetComponent("TurretProjectile") as TurretProjectile) != null)
            {
                health -= collision.gameObject.GetComponent<TurretProjectile>().damage;
                Destroy(collision.gameObject);
            }
        }

        
    }

}
