using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NavigationManager : MonoBehaviour
{
    [Tooltip("Isi semua tombol dalam urutan navigasi (atas ke bawah atau kiri ke kanan)")]
    public List<Selectable> selectables;

    private int currentIndex = 0;

    private void OnEnable()
    {
        if (selectables == null || selectables.Count == 0)
            return;

        currentIndex = 0;
        SelectCurrent();
    }

    private void Update()
    {
        if (KeyBindings.GetDown()) MoveNext();
        if (KeyBindings.GetUp()) MovePrev();
        if (KeyBindings.GetLeft()) MovePrev();
        if (KeyBindings.GetRight()) MoveNext();

        if (KeyBindings.GetConfirm()) ActivateCurrent();

        // Back
        if (Input.GetKeyDown(KeyBindings.Back))
            OnBack();

        // Toggle Voice
        if (Input.GetKeyDown(KeyBindings.ToggleSound))
            AudioManager.Instance.ToggleSound();
    }

    private void MoveNext()
    {
        if (selectables.Count == 0) return;

        currentIndex = (currentIndex + 1) % selectables.Count;
        SelectCurrent();
    }

    private void MovePrev()
    {
        if (selectables.Count == 0) return;

        currentIndex = (currentIndex - 1 + selectables.Count) % selectables.Count;
        SelectCurrent();
    }

    private void SelectCurrent()
    {
        var element = selectables[currentIndex];

        if (element == null) return;

        // set focus visually
        EventSystem.current.SetSelectedGameObject(element.gameObject);

        // voice label
        var acc = element.GetComponent<AccessibleSelectable>();
        // if (acc != null)
            // AudioManager.Instance.PlayVoice(acc.accessibilityLabel);
    }

    private void ActivateCurrent()
    {
        var element = selectables[currentIndex];
        var btn = element.GetComponent<Button>();

        if (btn)
        {
            btn.onClick.Invoke();
            // AudioManager.Instance.PlaySFX("MouseClick_SFX"); # nanti ditambahkan SFX klik
        }
    }

    public void OnBack()
    {
        // AudioManager.Instance.PlayVoice("Kembali");
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
