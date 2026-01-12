// AccessibleButton.cs (Diletakkan pada komponen Button/Selectable UI)

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AccessibleButton : MonoBehaviour, ISelectHandler
{
    [Tooltip("Nama klip audio yang harus diputar (harus sama dengan nama file audio)")]
    public string audioClipName;

    // Dipanggil saat tombol mendapat fokus (saat user menekan TAB atau Panah)
    public void OnSelect(BaseEventData eventData)
    {
        // 1. Berikan umpan balik audio
        if (AudioFeedbackManager.Instance != null)
        {
            AudioFeedbackManager.Instance.PlayFeedback(audioClipName);
        }

        // 2. Berikan umpan balik visual (opsional, tapi baik untuk yang masih bisa melihat)
      
    }
    
    // Fungsi OnClick bawaan Unity akan menangani Enter/Spasi.
}