using UnityEngine;

public class PanelAudioAnnouncer : MonoBehaviour
{
    [Header("Intro / Replay")]
    public AudioClip introNarration;
    public AudioClip replayNarration;

    [Header("Step Narration (SPACE)")]
    public AudioClip[] stepNarrations;

    [Header("Special Narration (RIGHT SHIFT)")]
    public AudioClip specialNarration;

    private int stepIndex = 0;
    private bool isInterrupted = false;

    private void OnEnable()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.currentContext = AudioManager.AudioContext.Panel;

        stepIndex = 0;
        isInterrupted = false;
        PlayIntroAudio();
    }

    private void OnDisable()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.currentContext = AudioManager.AudioContext.Scene;
    }

    private void Update()
    {
        if (isInterrupted) return;

        if (Input.GetKeyDown(KeyCode.Space))
            PlayNextStep();

        if (Input.GetKeyDown(KeyCode.K))
            ReplayIntro();

        if (Input.GetKeyDown(KeyCode.RightShift))
            PlaySpecial();

        if (Input.GetKeyDown(KeyCode.J))
            StopAudio();
    }

    public void PlayIntroAudio()
    {
        if (introNarration == null || AudioManager.Instance == null) return;

        AudioManager.Instance.StopAllAudio();
        AudioManager.Instance.PlayVoice(introNarration);
    }

    public void PlayNextStep()
    {
        if (stepNarrations == null || stepIndex >= stepNarrations.Length) return;

        AudioManager.Instance.StopAllAudio();
        AudioManager.Instance.PlayVoice(stepNarrations[stepIndex]);
        stepIndex++;
    }

    public void ReplayIntro()
    {
        stepIndex = 0;
        AudioClip clip = replayNarration != null ? replayNarration : introNarration;

        if (clip == null || AudioManager.Instance == null) return;

        AudioManager.Instance.StopAllAudio();
        AudioManager.Instance.PlayVoice(clip);
    }

    public void PlaySpecial()
    {
        if (specialNarration == null || AudioManager.Instance == null) return;

        AudioManager.Instance.StopAllAudio();
        AudioManager.Instance.PlayVoice(specialNarration);
    }

    public void StopAudio()
    {
        AudioManager.Instance?.StopAllAudio();
    }

    public void InterruptByUI()
    {
        isInterrupted = true;
        AudioManager.Instance?.StopAllAudio();
    }
}
