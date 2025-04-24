using UnityEngine;

public class Building : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Destroy if too close to play at the start of the game.
        if ((transform.position.x < 5 || transform.position.x > 5) && (transform.position.z < 5 || transform.position.z > 5))
        {
            Debug.Log("To close, destorying!");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Player Collision
    void OnCollisionEnter(Collision col)
    {
        //If this object collide with a player, building or projectile, enable this objects physics.
        if (col.gameObject.tag == "Player" )
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;

            //Add addiotnal force
            Vector3 globalPositionOfContact = col.contacts[0].point;

            GetComponent<Rigidbody>().AddExplosionForce(5, globalPositionOfContact, 1);
        }
        
        if (col.gameObject.tag == "Building")
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;

            //Add addiotnal force
            Vector3 globalPositionOfContact = col.contacts[0].point;

            GetComponent<Rigidbody>().AddExplosionForce(5, globalPositionOfContact, 1);
        }
        
    }

    //&& col.gameObject.GetComponent<Rigidbody>().isKinematic == true
}
