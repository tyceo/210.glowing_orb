using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlescreenManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressStart()
    {
        SceneManager.LoadScene("Main");
    }

    public void PressExit()
    {
        Application.Quit();
    }
}
