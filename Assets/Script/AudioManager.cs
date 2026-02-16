using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public enum AudioContext
    {
        Scene,
        Panel
    }

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource voiceSource;

    [HideInInspector] public AudioContext currentContext = AudioContext.Scene;

    private bool voiceEnabled = true;

    private void Awake()
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
        voiceSource.clip = clip;
        StartCoroutine(PlayNextFrame());
    }

    private IEnumerator PlayNextFrame()
    {
        yield return null;
        voiceSource.Play();
    }

    public void StopAllAudio()
    {
        if (sfxSource != null) sfxSource.Stop();
        if (voiceSource != null) voiceSource.Stop();
    }

    public void ToggleSound()
    {
        voiceEnabled = !voiceEnabled;
        if (!voiceEnabled)
            StopAllAudio();
    }

    public bool IsVoicePlaying()
    {
        return voiceSource != null && voiceSource.isPlaying;
    }
}
