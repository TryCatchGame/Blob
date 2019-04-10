using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private GameObject gameOverCanvas;

    private float timer;

    public bool GameOver { get; private set; }

    // Start is called before the first frame update
    void Start() {
        timer = 60f;
        GameOver = false;
    }

    // Update is called once per frame
    void Update() {
        if (!GameOver) {
            HandleTimerUpdate();
        }
    }

    private void HandleTimerUpdate() {
        timer -= Time.deltaTime;

        timerText.text = Mathf.RoundToInt(timer).ToString();

        if (timer <= 0) {
            timerText.text = "0";
            MakeGameOver();
        }
    }

    public void MakeGameOver() {
        GameOver = true;
        gameOverCanvas.SetActive(true);
    }
}
