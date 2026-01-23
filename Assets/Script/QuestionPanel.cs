using UnityEngine;

public class QuestionPanel : MonoBehaviour
{
    [Range(0, 3)]
    public int correctIndex; // 0=A, 1=B, 2=C, 3=D

    public bool IsCorrect(int selectedIndex)
    {
        return selectedIndex == correctIndex;
    }
}
