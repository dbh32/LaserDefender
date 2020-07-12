using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 1f;
    [SerializeField] float slowMotionEffectScale = 0.5f;

    public void LoadGameOver() { StartCoroutine(WaitAndLoad()); }
    
    IEnumerator WaitAndLoad()
    {
        Time.timeScale = slowMotionEffectScale;
        yield return new WaitForSeconds(delayInSeconds);
        Time.timeScale = 1f;
        Cursor.visible = true;
        SceneManager.LoadScene("Game Over");
    }

    public void LoadGameScene() 
    {
        Cursor.visible = false;
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadStartScreen() 
    {
        Cursor.visible = true;
        SceneManager.LoadScene("Start Menu");
    }

    public void QuitGame() { Application.Quit(); }
}
