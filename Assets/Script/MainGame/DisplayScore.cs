using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DisplayScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;

    public GameObject gameOverPanel;

    private void Start()
    {
        Fruits.OnScoreAdded.AddListener(AddScore);
        AddScore(0);
        Fruits.OnGameOver.AddListener(() => SceneManager.LoadScene("Score"));

        gameOverPanel.SetActive(false);
        Fruits.OnGameOver.AddListener(() => gameOverPanel.SetActive(true));
    }

    private void AddScore(int score)
    {
        this.score += score;
        scoreText.text = this.score.ToString();
    }
}