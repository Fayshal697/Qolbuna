using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class AccessibleSelectable : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    public AudioClip narrationClip;   // Narasi khusus tombol ini
    public AudioClip hoverSFX;        // SFX opsional

    public void OnPointerEnter(PointerEventData eventData)
    {
        Announce();
    }

    public void OnSelect(BaseEventData eventData)
    {
        Announce();
    }

    void Announce()
    {
        // SFX hover
        if (hoverSFX != null)
            AudioManager.Instance.PlaySFX(hoverSFX);

        // Narasi
        if (narrationClip != null)
            AudioManager.Instance.PlayVoice(narrationClip);
    }
}
