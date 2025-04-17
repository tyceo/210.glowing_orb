using UnityEngine;

public class HelicopterProjectile : MonoBehaviour
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
 
        transform.position += transform.forward * moveSpeed;

    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10)
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
