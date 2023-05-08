using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource click;
    /*public void PlayGameMusic()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }*/

    public void PlayGame()
    {
        click.Play();
        StartCoroutine(LoadNextSceneWithDelay(0.1f));
    }

    private IEnumerator LoadNextSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public async void Level1()
    {
        await Task.Delay(100); // Delay for 100 milliseconds (0.1 seconds)
        SceneManager.LoadScene("Directional Aesteroids 1");
    }

    public async void Level2()
    {
        await Task.Delay(100); // Delay for 100 milliseconds (0.1 seconds)
        SceneManager.LoadScene("Level 2 Aesteroids 2");
    }

    public async void Level3()
    {
        await Task.Delay(100); // Delay for 100 milliseconds (0.1 seconds)
        SceneManager.LoadScene("Level 3 Aesteroids 3");
    }

    public async void Level4()
    {
        await Task.Delay(100); // Delay for 100 milliseconds (0.1 seconds)
        SceneManager.LoadScene("Level 4 Aesteroids 4");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
