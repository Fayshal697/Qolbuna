using System.Collections;
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

    private void OnEnable()
    {
        stepIndex = 0;
        PlayIntroAudio();
    }

    private void Update()
    {
        // Input tombol universal
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
        AudioClip clip = introNarration;
        if (clip != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.StopAllAudio();
            AudioManager.Instance.PlayVoice(clip);
        }
    }

    public void PlayNextStep()
    {
        if (stepNarrations == null || stepIndex >= stepNarrations.Length) return;

        AudioClip clip = stepNarrations[stepIndex];
        stepIndex++;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopAllAudio();
            AudioManager.Instance.PlayVoice(clip);
        }
    }

    public void ReplayIntro()
    {
        stepIndex = 0;
        AudioClip clip = replayNarration != null ? replayNarration : introNarration;

        if (clip != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.StopAllAudio();
            AudioManager.Instance.PlayVoice(clip);
        }
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
}
