using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;

    public static bool paused;

    
    // Start is called before the first frame update
    void Start(){
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        gameOverMenuUI.SetActive(false);
        paused = false;
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(paused == true){
                Resume();
            }else{
                Pause();
            }
        }
        if (Movement.player.hearts <= 0)
        {
            GameOver();
        }

        
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void PlayAgain()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameOver()
    {
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void Quit(){
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
