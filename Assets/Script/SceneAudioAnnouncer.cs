using UnityEngine;

public class SceneAudioAnnouncer : MonoBehaviour
{
    public AudioClip sceneIntroAudio;
    private bool hasPlayed = false;

    private void Start()
    {
        PlaySceneAudio();
    }

    private void Update()
    {
        if (AudioManager.Instance == null) return;
        if (AudioManager.Instance.currentContext != AudioManager.AudioContext.Scene) return;

        if (Input.GetKeyDown(KeyCode.J))
            AudioManager.Instance.StopAllAudio();

        if (Input.GetKeyDown(KeyCode.K))
            PlaySceneAudio();
    }

    public void PlaySceneAudio()
    {
        if (sceneIntroAudio == null || AudioManager.Instance == null) return;

        hasPlayed = true;
        AudioManager.Instance.StopAllAudio();
        AudioManager.Instance.PlayVoice(sceneIntroAudio);
    }
}
