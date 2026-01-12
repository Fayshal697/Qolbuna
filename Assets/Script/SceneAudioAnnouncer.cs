using UnityEngine;

public class SceneAudioAnnouncer : MonoBehaviour
{
    [Header("Scene Narration")]
    public AudioClip sceneNarration;

    private void Start()
    {
        if (AudioManager.Instance == null) return;
        if (sceneNarration == null) return;

        AudioManager.Instance.StopAllAudio();
        AudioManager.Instance.PlaySceneNarration(sceneNarration);
    }
}
