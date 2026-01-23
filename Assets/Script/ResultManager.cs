using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ScoreResult
{
    public int correctAnswer;     // angka pasti (misal 3)
    public GameObject targetUI;   // Button / Panel yang di-trigger
}

public class ResultManager : MonoBehaviour
{
    [Header("Mapping Skor ke UI")]
    public ScoreResult[] results;

    void Start()
    {
        int finalScore = ScoreManager.Score;

        // Matikan semua UI dulu
        foreach (var r in results)
        {
            if (r.targetUI != null)
                r.targetUI.SetActive(false);
        }

        // Aktifkan UI yang sesuai skor
        foreach (var r in results)
        {
            if (r.correctAnswer == finalScore)
            {
                r.targetUI.SetActive(true);
                return;
            }
        }

        Debug.LogWarning("Tidak ada UI untuk skor: " + finalScore);
    }
}
