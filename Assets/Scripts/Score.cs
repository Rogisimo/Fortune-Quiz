using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int correctAnswers = 0;
    public int questionsAnswered = 0;
    
    public int CalculateScore(){
        return Mathf.RoundToInt(correctAnswers / (float)questionsAnswered * 100);
    }
}
