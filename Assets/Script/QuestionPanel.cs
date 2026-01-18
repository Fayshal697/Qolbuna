using UnityEngine;
using UnityEngine.UI;

public class QuestionPanel : MonoBehaviour
{
    [Header("Jawaban Benar")]
    [Tooltip("0=A, 1=B, 2=C, 3=D")]
    public int correctAnswerIndex;

    [HideInInspector]
    public bool isAnswered = false;

    public bool IsCorrect(int selectedIndex)
    {
        isAnswered = true;
        return selectedIndex == correctAnswerIndex;
    }
}
