using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class AccessibleSelectable : MonoBehaviour,
    IPointerEnterHandler,
    ISelectHandler
{
    [Header("Audio")]
    public AudioClip narrationClip;

    private bool hasPlayedThisFrame = false;

    private void LateUpdate()
    {
        hasPlayedThisFrame = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HandleHighlight();
    }

    public void OnSelect(BaseEventData eventData)
    {
        HandleHighlight();
    }

    private void HandleHighlight()
    {
        if (hasPlayedThisFrame) return;
        hasPlayedThisFrame = true;

        if (AudioManager.Instance == null) return;

        // 🔥 HENTIKAN PANEL AUDIO JIKA ADA
        PanelAudioAnnouncer panel =
            FindObjectOfType<PanelAudioAnnouncer>();

        if (panel != null && panel.isActiveAndEnabled)
        {
            panel.InterruptByUI();
        }

        // 🔊 Mainkan audio tombol
        if (narrationClip != null)
            AudioManager.Instance.PlayVoice(narrationClip);
    }
}
