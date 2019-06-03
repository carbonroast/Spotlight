using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Update()
    {
        if (Luminosity.IO.InputManager.anyKeyDown)
        {
            Utils.Play();
        }
    }
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
