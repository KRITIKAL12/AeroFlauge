using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Level1()
    {
        SceneManager.LoadScene("Directional Aesteroids 1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level 2 Aesteroids 2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Level 3 Aesteroids 3");
    }

    public void Level4()
    {
        SceneManager.LoadScene("Level 4 Aesteroids 4");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
