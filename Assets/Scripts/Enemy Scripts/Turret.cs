using Unity.VisualScripting;
using UnityEngine;

//Turret slowly floats towards player. When it at the right distance, it shoots fast moving, but low damage projectiles.
public class Turret : MonoBehaviour
{
    //Declare Variables
    public float health; //How much health this turret has.

    [SerializeField] private GameObject target; //The target of the helicopter
    [SerializeField] private GameObject projectile; //The projectile the turret shoots

    [SerializeField] private float shootSpeed; //How fast the turret shoots
    [SerializeField] private float moveSpeed; //How fast the turret moves.

    [SerializeField] private float hoverHeight; //How far of the ground the turret should be.
    [SerializeField] private float hoverDistance; //How far away the turret should try and stay.

    [SerializeField] private Rigidbody rb;

    float timeUntilShoot = 0;    //Declare Variables
    public bool hasBeenDestroyed = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get player object
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Only do stuff if alive
        if (hasBeenDestroyed == false)
        {
            //Movement =====================================
            //Rotate towards player
            transform.LookAt(target.transform.position);

            //Get the position where it should go to.
            Vector3 oldPosition = transform.position;
            Vector3 newPosition = transform.position;

            //If the postion is not to close to the player, move them forward.
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance > hoverDistance)
            {
                newPosition += transform.forward * moveSpeed;
            }
            else
            {
                newPosition = -transform.forward * moveSpeed;
            }

            //If hover height is to low, increase it.
            if (transform.position.y < hoverHeight)
            {
                newPosition = new Vector3(newPosition.x, newPosition.y + 2, newPosition.z);
            }

            //Move the rigidbody towards the new position
            Vector3 direction = newPosition - oldPosition;
            rb.linearVelocity = direction.normalized * moveSpeed;


            //Firing ============================================
            //Only fire if close enough
            distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < hoverDistance * 1.5)
            {
                timeUntilShoot += 1;
                if (timeUntilShoot >= shootSpeed)
                {
                    timeUntilShoot = 0;
                    var front = transform.position + transform.forward * 5f;
                    var proj = Instantiate(projectile, front, transform.rotation);
                }
            }
        } else {
            rb.useGravity = true;
        }
        
        //Detect if this object should be dead.
        if(health <= 0 && hasBeenDestroyed == false)
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
