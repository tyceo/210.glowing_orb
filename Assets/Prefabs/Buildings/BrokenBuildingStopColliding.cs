using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrokenBuildingStopColliding : MonoBehaviour
{
    [Tooltip("Time in seconds before the collider is disabled and position is frozen")]
    public float disableDelay = 5f;

    IEnumerator Start()
    {
        gameObject.tag = "Broken";
        // Get components
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogWarning("No Rigidbody component found on " + gameObject.name);
            yield break;
        }
        yield return new WaitForSeconds(2);
        gameObject.tag = "Broken1";
        // Wait for the specified delay
        yield return new WaitForSeconds(disableDelay);

        
        // Disable MeshCollider if it exists
        if (meshCollider != null)
        {
            //meshCollider.enabled = false;
        }

        /*
        // Freeze all position constraints
        rb.constraints = RigidbodyConstraints.FreezePositionX |
                         RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezePositionZ;
        */
    }
    /*
    private void Update()
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Eater"))
            {
            Debug.Log("testold");
            }
        }
    }
    */
}
