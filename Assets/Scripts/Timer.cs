using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeToCompleteQuestions = 30f;
    float timeRemaining;

    public bool timerFinished = false;

    public Image timerImage;

private void Start() {
    timeRemaining = timeToCompleteQuestions;
}

    void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if(timeRemaining > 0){
            timeRemaining -= Time.deltaTime;

            UpdateFillFraction();
        }
        else{
            timeRemaining = 0;
            timerFinished = true;
        }
    }

    private void UpdateFillFraction()
    {
        float fillFraction = timeRemaining / timeToCompleteQuestions;
        timerImage.fillAmount = fillFraction;
    }
}
