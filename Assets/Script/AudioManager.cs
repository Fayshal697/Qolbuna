using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource voiceSource;

    private bool voiceEnabled = true;

    // === SCENE NARRATION SYSTEM ===
    private AudioClip sceneIntroClip;   // sound pertama
    private AudioClip sceneReplayClip;  // sound ke-2 (opsional)

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
    // SFX
    // =====================
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;

        sfxSource.Stop();
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    // =====================
    // VOICE UMUM
    // =====================
    public void PlayVoice(AudioClip clip)
    {
        if (!voiceEnabled || clip == null || voiceSource == null) return;

        voiceSource.Stop();
        voiceSource.clip = clip;
        voiceSource.Play();
    }

    // =====================
    // SCENE / PANEL ANNOUNCER
    // =====================
    public void PlaySceneNarration(AudioClip introClip, AudioClip replayClip = null)
    {
        if (!voiceEnabled || introClip == null || voiceSource == null) return;

        sceneIntroClip = introClip;
        sceneReplayClip = replayClip;

        voiceSource.Stop();
        voiceSource.clip = sceneIntroClip;
        voiceSource.Play();
    }

    public void ReplaySceneNarration()
    {
        if (!voiceEnabled || voiceSource == null) return;

        AudioClip clipToPlay = sceneReplayClip != null
            ? sceneReplayClip
            : sceneIntroClip;

        if (clipToPlay == null) return;

        voiceSource.Stop();
        voiceSource.clip = clipToPlay;
        voiceSource.Play();
    }

    // =====================
    // STOP & TOGGLE
    // =====================
    public void StopAllAudio()
    {
        if (sfxSource != null) sfxSource.Stop();
        if (voiceSource != null) voiceSource.Stop();
    }

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
