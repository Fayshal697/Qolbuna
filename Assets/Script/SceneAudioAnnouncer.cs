using UnityEngine;

public class SceneAudioAnnouncer : MonoBehaviour
{
    [Header("Scene / Panel Narration")]
    [Tooltip("Narasi pertama saat masuk scene / panel")]
    public AudioClip introNarration;

    [Tooltip("Narasi saat tombol K ditekan (opsional)")]
    public AudioClip replayNarration;

    private void Start()
    {
        if (AudioManager.Instance == null) return;
        if (introNarration == null) return;

        AudioManager.Instance.StopAllAudio();
        AudioManager.Instance.PlaySceneNarration(introNarration, replayNarration);
    }
}
