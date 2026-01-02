using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;   // hover, click, dll
    public AudioSource voiceSource; // narasi khusus tunanetra (tts/audio file)
    private bool voiceEnabled = true;  // status narasi


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

    // -------------------------
    // SFX
    // -------------------------
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        sfxSource.Stop();
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    // -------------------------
    // Narasi (TTS / Rekaman)
    // -------------------------
    public void PlayVoice(AudioClip clip)
    {
        if (!voiceEnabled || clip == null) return;

        voiceSource.Stop();
        voiceSource.clip = clip;
        voiceSource.Play();
    }

    public void StopVoice()
    {
        voiceSource.Stop();
    }

    public void ToggleSound()
{
    voiceEnabled = !voiceEnabled;

    if (!voiceEnabled)
    {
        // Matikan narasi yang sedang berjalan
        voiceSource.Stop();
    }
    else
    {
        // Optional: Umumkan bahwa narasi aktif kembali
        // voiceSource.PlayOneShot(someClip);
    }

    Debug.Log("Voice Narration Toggled: " + voiceEnabled);
}
}
