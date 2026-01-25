using UnityEngine;

public class QuestionPanel : MonoBehaviour
{
    [Range(0, 3)]
    public int correctIndex;

    [Header("Audio Feedback")]
    public AudioClip correctAudio;
    public AudioClip wrongAudio;

    public bool IsCorrect(int selectedIndex)
    {
        return selectedIndex == correctIndex;
    }

    public void PlayFeedbackAudio(bool isCorrect)
    {
        if (AudioManager.Instance == null) return;

        // 🔴 MATIKAN SEMUA AUDIO YANG SEDANG JALAN
        AudioManager.Instance.StopAllAudio();

        AudioClip clip = isCorrect ? correctAudio : wrongAudio;
        if (clip == null) return;
        if (AudioManager.Instance.sfxSource == null) return;

        AudioManager.Instance.sfxSource.PlayOneShot(clip);
    }

}
