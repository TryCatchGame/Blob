using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler> {

    [SerializeField]
    private int playingFieldBuildIndex;

    [SerializeField]
    private int mainMenuBuildIndex;

    public void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GotoPlayingFieldScene() {
        SceneManager.LoadScene(playingFieldBuildIndex);
    }

    public void GotoMainMenuScene() {
        SceneManager.LoadScene(mainMenuBuildIndex);
    }
}
