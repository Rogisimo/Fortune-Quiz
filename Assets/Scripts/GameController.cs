using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    Quiz quiz;
    public GameObject winScreen;
    public TextMeshProUGUI winScoreText;
    public GameObject looseScreen;

    private void Start()
    {
        quiz = FindObjectOfType<Quiz>();
    }

    public void WinGame()
    {
        winScreen.SetActive(true);
        winScoreText.text = "Score: " + quiz.score.CalculateScore() + " %";
        Time.timeScale = 0f;
    }

    public void LooseGame()
    {
        Time.timeScale = 0f;
        looseScreen.SetActive(true);

    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
