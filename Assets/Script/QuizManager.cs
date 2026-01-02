using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour {
    public int level = 1; // 1..4
    public QuranData surah;
    public int questionsPerLevel = 5;
    public Text questionText;
    public Button[] choiceButtons;
    public AudioClip correctClip, wrongClip;
    int currentQuestion = 0;
    int score = 0;
    List<Question> questions;

    void Start() {
        GenerateQuestions();
        DisplayQuestion();
    }

    void GenerateQuestions() {
        questions = new List<Question>();
        int startAyat = (level - 1) * 10 + 1;
        // create questions based on ayat text or audio; here dummy sample
        for (int i = 0; i < questionsPerLevel; i++) {
            int ay = startAyat + i;
            Question q = new Question {
                prompt = $"Sambung: ayat {ay} dari surah {surah.surahName}",
                choices = new string[] {"A","B","C","D"},
                correctIndex = Random.Range(0,4)
            };
            questions.Add(q);
        }
    }

    void DisplayQuestion() {
        if (currentQuestion >= questions.Count) {
            FinishQuiz();
            return;
        }
        var q = questions[currentQuestion];
        questionText.text = q.prompt;
        // AudioManager.Instance.PlayVoice(q.prompt);
        for (int i=0;i<choiceButtons.Length;i++){
            var txt = choiceButtons[i].GetComponentInChildren<Text>();
            txt.text = q.choices[i];
            int idx = i;
            choiceButtons[i].onClick.RemoveAllListeners();
            choiceButtons[i].onClick.AddListener(()=>OnChoice(idx));
        }
    }

    void OnChoice(int idx) {
        var q = questions[currentQuestion];
        if (idx == q.correctIndex) {
            score++;
            AudioManager.Instance.PlaySFX(correctClip);
            // AudioManager.Instance.PlayVoice("Benar");
        } else {
            AudioManager.Instance.PlaySFX(wrongClip);
            // AudioManager.Instance.PlayVoice("Salah");
        }
        currentQuestion++;
        DisplayQuestion();
    }

    void FinishQuiz() {
        // AudioManager.Instance.PlayVoice($"Tes selesai. Skor Anda {score} dari {questions.Count}");
        // play final narrasi clip or show result screen
    }

    public class Question {
        public string prompt;
        public string[] choices;
        public int correctIndex;
    }
}
