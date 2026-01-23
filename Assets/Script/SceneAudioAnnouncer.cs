using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAudioAnnouncer : MonoBehaviour
{
    public AudioClip sceneIntroAudio; // audio untuk scene ini

    private bool firstTime = true;

    private void Start()
    {
        // Trigger audio saat game pertama kali dimuat
        if (firstTime)
        {
            firstTime = false;
            PlaySceneAudio();
        }
    }

    private void OnEnable()
    {
        // Trigger audio saat scene berikutnya diaktifkan / dipindah
        if (!firstTime)
        {
            PlaySceneAudio();
        }
    }

    public void PlaySceneAudio()
    {
        if (sceneIntroAudio != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.StopAllAudio(); // hentikan audio lain dulu
            AudioManager.Instance.PlayVoice(sceneIntroAudio);
        }
    }

    public void StopSceneAudio()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.StopAllAudio();
    }
}
