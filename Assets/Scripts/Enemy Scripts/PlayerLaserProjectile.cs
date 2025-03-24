using UnityEngine;

public class PlayerLaserProjectile : MonoBehaviour
{
    //Delcare Variables
    [SerializeField] private float moveSpeed;
    public float damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().excludeLayers = 6;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Travel Forward
        GetComponent<Rigidbody>().linearVelocity = (transform.forward * moveSpeed);
    }

    //If colliding with enemy projectile, destroy this object.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Building")
        {
            Destroy(this.gameObject);
        }
    }


}
