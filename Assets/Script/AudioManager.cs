using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;    // hover, click, dll
    public AudioSource voiceSource;  // narasi (scene / button)

    private bool voiceEnabled = true;

    // ⬇️ SIMPAN AUDIO SCENE TERAKHIR
    private AudioClip lastSceneNarration;

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

    // =====================
    // SFX (button hover, click)
    // =====================
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;

        sfxSource.Stop();
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    // =====================
    // VOICE UMUM (button narration)
    // =====================
    public void PlayVoice(AudioClip clip)
    {
        if (!voiceEnabled || clip == null || voiceSource == null) return;

        voiceSource.Stop();
        voiceSource.clip = clip;
        voiceSource.Play();
    }

    // =====================
    // VOICE KHUSUS SCENE
    // =====================
    public void PlaySceneNarration(AudioClip clip)
    {
        if (!voiceEnabled || clip == null || voiceSource == null) return;

        lastSceneNarration = clip;

        voiceSource.Stop();
        voiceSource.clip = clip;
        voiceSource.Play();
    }

    public void ReplaySceneNarration()
    {
        if (!voiceEnabled || lastSceneNarration == null || voiceSource == null) return;

        voiceSource.Stop();
        voiceSource.clip = lastSceneNarration;
        voiceSource.Play();
    }

    // =====================
    // STOP SEMUA AUDIO (TOMBOL J)
    // =====================
    public void StopAllAudio()
    {
        if (sfxSource != null) sfxSource.Stop();
        if (voiceSource != null) voiceSource.Stop();
    }

    // =====================
    // TOGGLE NARASI
    // =====================
    public void ToggleSound()
    {
        voiceEnabled = !voiceEnabled;

        if (!voiceEnabled && voiceSource != null)
            voiceSource.Stop();

        Debug.Log("Voice Narration Enabled: " + voiceEnabled);
    }

    public bool IsVoicePlaying()
    {
        return voiceSource != null && voiceSource.isPlaying;
    }
}
