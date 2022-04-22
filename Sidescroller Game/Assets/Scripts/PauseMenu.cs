using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    public GameObject pauseMenuUI;

    public static bool paused;

    
    // Start is called before the first frame update
    void Start(){
        pauseMenuUI.SetActive(false);
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

        
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
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
