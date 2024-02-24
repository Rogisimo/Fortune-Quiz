using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    public QuestionSO currentQuestion;
    public List<QuestionSO> questions = new List<QuestionSO>();
    public TextMeshProUGUI questionText;
    public GameObject[] answerButtons;
    int correctAnswerIndex;
    public Sprite defaultAnswerSprite;
    public Sprite correctAnswerSprite;
    public Sprite wrongAnswerSprite;
    public GameObject looseScreen;
    public GameObject winScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winScoreText;
    public Slider slider;
    public bool isComplete = false;
    public Score score;

    Timer timer;

    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        score = FindObjectOfType<Score>();
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();
        scoreText.text = "Score: 0 %"; 
        slider.maxValue = questions.Count;
        slider.value = 0;
        gameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        if (timer.timerFinished)
        {
            gameController.LooseGame();
        }
        if (isComplete)
        {
            gameController.WinGame();
            isComplete = false;
        }
    }


    public void OnAnswerSelected(int index)
    {
        DisplayAnswer(index);
        SetButtonState(false);
        scoreText.text = "Score: " + score.CalculateScore() + " %";
        StartCoroutine(WaitBeforeNextQuestion(2));
        slider.value++;
        if(slider.value == slider.maxValue){
            isComplete = true;
        }
    }
    IEnumerator WaitBeforeNextQuestion(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GetNextQuestion();
        yield break;
    }

    void DisplayAnswer(int index)
    {
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "That is correct! You are very smart";
            Image correctButtonImage = answerButtons[index].GetComponent<Image>();
            correctButtonImage.sprite = correctAnswerSprite;
            score.correctAnswers++;
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "That is incorect you egg!";
            Image correctButtonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            Image wrongButtonImage = answerButtons[index].GetComponent<Image>();
            correctButtonImage.sprite = correctAnswerSprite;
            wrongButtonImage.sprite = wrongAnswerSprite;
        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            score.questionsAnswered++;
        }
    }

    private void GetRandomQuestion()
    {
        int randomIndex = Random.Range(0, questions.Count);
        currentQuestion = questions[randomIndex];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
}
