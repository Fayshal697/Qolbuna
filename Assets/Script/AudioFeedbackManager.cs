// AudioFeedbackManager.cs (Diletakkan pada GameObject Kosong di setiap Scene)

using UnityEngine;
using System.Collections.Generic;

public class AudioFeedbackManager : MonoBehaviour
{
    // Instance tunggal untuk akses mudah dari script lain
    public static AudioFeedbackManager Instance;
    
    // Komponen Audio Source untuk memutar klip
    private AudioSource audioSource;
    
    [Header("Audio Clips Mapping")]
    // Struktur data untuk menyimpan nama tombol dan klip audio yang sesuai
    public Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();

    [Tooltip("Daftar semua klip audio menu (diisi di Inspector)")]
    public AudioClip[] menuClips;

    void Awake()
    {
        // Pastikan hanya ada satu instance manager ini
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opsional, jika Anda ingin manager tetap ada
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 1. Inisialisasi Dictionary (Harus disesuaikan dengan nama file audio Anda)
        // PENTING: Nama Key harus sama persis dengan tag atau nama tombol
        // Anda harus mengisi array 'menuClips' di Inspector dengan semua klip suara menu.
        foreach (AudioClip clip in menuClips)
        {
            clips.Add(clip.name, clip);
        }
    }

    // Fungsi yang dipanggil oleh tombol untuk memberikan umpan balik
    public void PlayFeedback(string clipName)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Mencari klip berdasarkan nama tombol
        if (clips.ContainsKey(clipName))
        {
            audioSource.PlayOneShot(clips[clipName]);
        }
        else
        {
            Debug.LogWarning("Audio clip tidak ditemukan untuk: " + clipName);
        }
    }
}