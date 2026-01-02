using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MediaPlayerController : MonoBehaviour {
    public AudioSource playerSource;
    public List<AudioClip> clips; // per ayat atau per kisah
    public int currentIndex = 0;

    public Button playBtn, stopBtn, replayBtn, nextBtn, prevBtn;
    public Text labelText; // show "Surah X : Ayat Y" (screen readers can ignore)

    void Start() {
        UpdateUI();
    }

    void Update() {
        // Fallback to built-in KeyCode values if a KeyBindings class is not defined
        if (Input.GetKeyDown(KeyCode.Space)) TogglePlay();       // Space = play/pause
        if (Input.GetKeyDown(KeyCode.S)) Stop();                // S = stop
        if (Input.GetKeyDown(KeyCode.R)) Replay();              // R = replay
        if (Input.GetKeyDown(KeyCode.RightArrow)) Next();       // RightArrow = next
        if (Input.GetKeyDown(KeyCode.LeftArrow)) Prev();        // LeftArrow = previous
    }

    public void TogglePlay() {
        if (playerSource.isPlaying) {
            playerSource.Pause();
            // AudioManager.Instance.PlayVoice("Dihentikan sementara");
        } else {
            PlayCurrent();
        }
    }

    public void PlayCurrent() {
        if (clips == null || clips.Count == 0) return;
        playerSource.clip = clips[currentIndex];
        playerSource.Play();
        // AudioManager.Instance.PlayVoice($"Memainkan {labelText.text}");
    }

    public void Stop() {
        playerSource.Stop();
        // AudioManager.Instance.PlayVoice("Stop");
    }

    public void Replay() {
        playerSource.Stop();
        playerSource.time = 0;
        playerSource.Play();
        // AudioManager.Instance.PlayVoice("Memutar ulang");
    }

    public void Next() {
        if (clips == null || clips.Count == 0) return;
        currentIndex = (currentIndex + 1) % clips.Count;
        UpdateUI();
        PlayCurrent();
    }

    public void Prev() {
        if (clips == null || clips.Count == 0) return;
        currentIndex = (currentIndex - 1 + clips.Count) % clips.Count;
        UpdateUI();
        PlayCurrent();
    }

    void UpdateUI() {
        if (labelText != null) labelText.text = $"Ayat {currentIndex+1}";
    }
}