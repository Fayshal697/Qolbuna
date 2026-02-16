using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHypno : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip introClip;
    public List<AudioClip> stepClips;

    private int stepIndex = -1;          // -1 = intro
    private AudioClip currentClip;

    private NavigationManager nav;
    private bool hypnoCompleted = false;

    void Start()
    {
        nav = FindObjectOfType<NavigationManager>();

        LockNavigation();

        // Scene load → intro langsung play
        stepIndex = -1;
        PlayClip(introClip);
    }

    void Update()
    {
        HandleInput();
        CheckAudioEnd();
    }

    // ======================
    // INPUT
    // ======================
    void HandleInput()
    {
        bool isPlaying = AudioManager.Instance.IsVoicePlaying();

        // J = STOP / RESET
        if (Input.GetKeyDown(KeyCode.J))
        {
            LockNavigation();

            if (isPlaying)
            {
                AudioManager.Instance.StopAllAudio();
            }
            else
            {
                // reset ke intro (belum play)
                stepIndex = -1;
                currentClip = introClip;
            }

            hypnoCompleted = false;
        }

        // K = PLAY / REPLAY
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (currentClip == null) return;

            LockNavigation();
            hypnoCompleted = false;

            StartCoroutine(ReplayClip());
        }

        // SPACE = NEXT STEP (AudioHypno)
        if (Input.GetKeyDown(KeyCode.Space) && !hypnoCompleted)
        {
            AudioManager.Instance.StopAllAudio();
            PlayNextStep();
        }
    }

    // ======================
    // AUDIO
    // ======================
    void PlayClip(AudioClip clip)
    {
        if (clip == null) return;

        currentClip = clip;

        AudioManager.Instance.StopAllAudio();
        StartCoroutine(PlayNextFrame(clip));
    }

    IEnumerator PlayNextFrame(AudioClip clip)
    {
        yield return null;
        AudioManager.Instance.PlayVoice(clip);
    }

    IEnumerator ReplayClip()
    {
        AudioManager.Instance.StopAllAudio();
        yield return null;
        AudioManager.Instance.PlayVoice(currentClip);
    }

    void CheckAudioEnd()
    {
        if (hypnoCompleted) return;

        if (currentClip != null && !AudioManager.Instance.IsVoicePlaying())
        {
            // audio terakhir selesai & tidak ada step lagi
            if (stepIndex >= stepClips.Count - 1)
            {
                CompleteHypno();
            }
        }
    }

    // ======================
    // FLOW
    // ======================
    void PlayNextStep()
    {
        stepIndex++;

        if (stepIndex >= stepClips.Count)
        {
            CompleteHypno();
            return;
        }

        PlayClip(stepClips[stepIndex]);
    }

    void CompleteHypno()
    {
        hypnoCompleted = true;
        UnlockNavigation();
    }

    // ======================
    // NAVIGATION CONTROL
    // ======================
    void LockNavigation()
    {
        if (nav != null)
            nav.inputEnabled = false;
    }

    void UnlockNavigation()
    {
        if (nav != null)
            nav.inputEnabled = true;
    }
}
