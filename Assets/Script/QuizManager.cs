using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [Header("Soal (Panel)")]
    public List<QuestionPanel> questions;

    private int currentIndex = 0;
    private bool answered = false;

    private void Start()
    {
        ScoreManager.ResetScore();
        ShowQuestion();
    }

    private void Update()
    {
        if (!answered)
        {
            if (Input.GetKeyDown(KeyCode.A)) Answer(0);
            if (Input.GetKeyDown(KeyCode.S)) Answer(1);
            if (Input.GetKeyDown(KeyCode.D)) Answer(2);
            if (Input.GetKeyDown(KeyCode.F)) Answer(3);
        }

        if (answered && Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextQuestion();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitToMenu();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void Answer(int index)
    {
        if (answered) return;

        if (questions[currentIndex].IsCorrect(index))
        {
            ScoreManager.AddScore(1);
        }

        answered = true;
    }

    private void NextQuestion()
    {
        currentIndex++;

        if (currentIndex >= questions.Count)
        {
            SceneManager.LoadScene(10); // ResultScene
            return;
        }

        answered = false;
        ShowQuestion();
    }

    private void ShowQuestion()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            questions[i].gameObject.SetActive(i == currentIndex);
        }
    }

    private void ExitToMenu()
    {
        ScoreManager.ResetScore();
        SceneManager.LoadScene("MainMenu");
    }
}
