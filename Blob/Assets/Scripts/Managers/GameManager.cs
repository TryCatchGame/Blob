using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private GameObject gameOverCanvas;

    [SerializeField]
    private Text[] pointTexts;

    private float timer;

    public bool GameOver { get; private set; }

    public int Point { get; private set; }

    // Start is called before the first frame update
    void Start() {
        timer = 60f;
        GameOver = true;
        Point = 0;

        UpdatePointTexts();
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

    public void StartGame() {
        GameOver = false;
    }


    public void AddScore() {
        ++Point;
        UpdatePointTexts();
    }

    public void MakeGameOver() {
        GameOver = true;
        gameOverCanvas.SetActive(true);
    }

    private void UpdatePointTexts() {
        foreach (var pointText in pointTexts) {
            pointText.text = Point.ToString();
        }
    }
}
