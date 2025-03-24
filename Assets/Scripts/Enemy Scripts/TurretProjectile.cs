using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    //Delcare Variables
    [SerializeField] private float moveSpeed;
    public float damage;

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
}
