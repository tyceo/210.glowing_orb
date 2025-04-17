using System.Threading;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    //Delcare Variables
    [SerializeField] private float moveSpeed;
    public float damage;
    [SerializeField] private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Travel Forward
        //transform.position += transform.forward * moveSpeed;
        GetComponent<Rigidbody>().linearVelocity = (transform.forward * moveSpeed);

        
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 6)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("PlayerProjectile"))
        {
            Destroy(gameObject);
        }
    }
}
