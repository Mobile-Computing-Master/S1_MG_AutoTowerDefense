using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartMap(int mapNumber)
    {
        // TODO: Add other maps
        SceneManager.LoadScene("Scenes/main");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
