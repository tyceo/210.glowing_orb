using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//The helicopter rotates around the player and shoots them. Projectiles are slow and destroyable, but do alot of damage.
public class Helicopter : MonoBehaviour
{
    //Declare Variables
    public float health; //How much health this helicopter has.

    [SerializeField] private GameObject target; //The target of the helicopter
    [SerializeField] private GameObject projectile; //The projectile the helicopter shoots

    [SerializeField] private float rotateSpeed; //How fast the helipcopter rotates around
    [SerializeField] private float shootSpeed; //How fast the helicopter shoots
    [SerializeField] private float moveSpeed; //How fast the helocopter moves.

    [SerializeField] private float hoverHeight; //How far of the ground the helipcoter should be.
    [SerializeField] private float hoverDistance; //How far away the helipcter should try and stay.

    [SerializeField] private Rigidbody rb;
    public bool hasBeenDestroyed = false;

    float timeUntilShoot = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get player object
        target = GameObject.Find("Player");
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (hasBeenDestroyed == false)
        {
            //Movement =========================================================================

            //Get the position where it should go to.
            Vector3 oldPosition = transform.position;
            transform.RotateAround(target.transform.position,Vector3.up, rotateSpeed);
            Vector3 newPosition = transform.position;
            transform.position = oldPosition;

            //Move the rigidbody towards the position
            Vector3 direction = newPosition - oldPosition;
            rb.linearVelocity = direction.normalized * moveSpeed;

            //Rotate towards player
            transform.LookAt(target.transform.position);



            //Firing =============================================================================
            timeUntilShoot += 1;
            if (timeUntilShoot >= shootSpeed)
            {
                timeUntilShoot = 0;
                var front = transform.position + transform.forward * 5f;
                var proj = Instantiate(projectile, front, transform.rotation);
            }
        }
        else
        {
            rb.useGravity = true;
        }

        //Detect if this object should be dead.
        if (health <= 0 && hasBeenDestroyed == false)
        {
            hasBeenDestroyed = true;
            var waveSpawner = GameObject.Find("Manager");
            waveSpawner.gameObject.GetComponent<WaveSpawner>().score += 10;
        }
    }

    //Collision with player projectiles
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            if ((collision.gameObject.GetComponent("PlayerLaserProjectile") as PlayerLaserProjectile) != null)
            {
                health -= collision.gameObject.GetComponent<PlayerLaserProjectile>().damage;
                Destroy(collision.gameObject);
            }

            if ((collision.gameObject.GetComponent("PlayerBeamProjectile") as PlayerBeamProjectile) != null)
            {
                health -= collision.gameObject.GetComponent<PlayerBeamProjectile>().damage;
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.tag == "Player")

        {
            health = 0;
        }
    }
}

