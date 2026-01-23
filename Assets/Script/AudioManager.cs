using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource voiceSource;

    private bool voiceEnabled = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayVoice(AudioClip clip)
    {
        if (!voiceEnabled || clip == null || voiceSource == null) return;

        voiceSource.Stop();
        voiceSource.clip = null;
        voiceSource.clip = clip;

        StartCoroutine(PlayNextFrame());
    }

    private IEnumerator PlayNextFrame()
    {
        yield return null;
        voiceSource.Play();
        Debug.Log("Playing voice clip: " + voiceSource.clip.name);
    }

    public void StopAllAudio()
    {
        if (sfxSource != null) sfxSource.Stop();
        if (voiceSource != null) voiceSource.Stop();
    }

    public bool IsVoicePlaying()
    {
        return voiceSource != null && voiceSource.isPlaying;
    }

    public void ToggleSound()
    {
        voiceEnabled = !voiceEnabled;
        if (!voiceEnabled && voiceSource != null)
            voiceSource.Stop();
    }
}
