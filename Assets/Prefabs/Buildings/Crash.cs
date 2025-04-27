using UnityEngine;

public class Crash : MonoBehaviour
{

    private AudioSource CrashSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CrashSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
 public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Building"))
        {
            CrashSound.Play();
        }

    }
}
