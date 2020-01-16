using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private Scene scene;

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void levelsGame()
    {
        SceneManager.LoadScene(2);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void openFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void openSecondLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void openThirdLevel()
    {
        SceneManager.LoadScene(3);
    }

    public void goMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void reloadLevel()
    {
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
