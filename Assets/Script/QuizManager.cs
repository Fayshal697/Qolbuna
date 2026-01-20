using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [Header("Soal (Panel)")]
    public List<QuestionPanel> questions;

    [Header("Button Jawaban")]
    public Button buttonA;
    public Button buttonB;
    public Button buttonC;
    public Button buttonD;

    private int currentIndex = 0;
    private bool answeredThisQuestion = false;

    private void Start()
    {
        ScoreManager.ResetScore();
        ShowQuestion(currentIndex);
    }

    private void Update()
    {
        // INPUT JAWABAN
        if (!answeredThisQuestion)
        {
            if (Input.GetKeyDown(KeyCode.A)) SelectAnswer(0);
            if (Input.GetKeyDown(KeyCode.S)) SelectAnswer(1);
            if (Input.GetKeyDown(KeyCode.D)) SelectAnswer(2);
            if (Input.GetKeyDown(KeyCode.F)) SelectAnswer(3);
        }

        // NEXT SOAL
        if (answeredThisQuestion && Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextQuestion();
        }

        // RESET & KELUAR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ScoreManager.ResetScore();
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ScoreManager.ResetScore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void SelectAnswer(int index)
    {
        if (answeredThisQuestion) return;

        bool correct = questions[currentIndex].IsCorrect(index);

        if (correct)
            ScoreManager.AddScore(1);

        answeredThisQuestion = true;
    }

    private void NextQuestion()
    {
        currentIndex++;

        if (currentIndex >= questions.Count)
        {
            // SEMUA SOAL SELESAI
            SceneManager.LoadScene(10);
            return;
        }

        answeredThisQuestion = false;
        ShowQuestion(currentIndex);
    }

    private void ShowQuestion(int index)
    {
        for (int i = 0; i < questions.Count; i++)
        {
            questions[i].gameObject.SetActive(i == index);
        }
    }
}
