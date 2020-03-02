using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    public void LoadMainMeun() {
        SceneManager.LoadScene(0);
    }

    public void LoadLevelSelect() {
        SceneManager.LoadScene("levelSelect");
    }

    public void LoadLevel(int level) {
        SceneManager.LoadScene(level +1);
    }

    public void CloseGame() {
        Application.Quit();
    }
}
