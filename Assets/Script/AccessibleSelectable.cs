using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class AccessibleSelectable : MonoBehaviour,
    IPointerEnterHandler,
    ISelectHandler
{
    [Header("Audio")]
    [Tooltip("Narasi saat tombol di-highlight (keyboard / mouse)")]
    public AudioClip narrationClip;

    [Tooltip("SFX hover (opsional)")]
    public AudioClip hoverSFX;

    private bool hasPlayedThisFrame = false;

    private void LateUpdate()
    {
        // reset flag tiap frame (anti double trigger)
        hasPlayedThisFrame = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayAudio();
    }

    public void OnSelect(BaseEventData eventData)
    {
        PlayAudio();
    }

    private void PlayAudio()
    {
        if (hasPlayedThisFrame) return;
        hasPlayedThisFrame = true;

        if (AudioManager.Instance == null) return;

        // Narasi setelahnya
        if (narrationClip != null)
            AudioManager.Instance.PlayVoice(narrationClip);
    }
}
