using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class ButtonGroup
{
    [Header("Nama Kelompok Button")]
    public string groupName;

    [Tooltip("Button-button dalam kelompok ini (URUTAN PENTING)")]
    public List<Selectable> buttons;
}

public class NavigationManager : MonoBehaviour
{
    [Header("Kelompok Button (Inspector Friendly)")]
    [Tooltip("Urutan group dan urutan button di dalamnya menentukan navigasi")]
    public List<ButtonGroup> buttonGroups;

    [HideInInspector]
    public List<Selectable> selectables = new List<Selectable>();

    [SerializeField] private Button backButton;
    [SerializeField] private UISceneManager uiSceneManager;

    private int currentIndex = 0;

    private void OnEnable()
    {
        BuildSelectableList();

        if (selectables == null || selectables.Count == 0)
            return;

        currentIndex = 0;
        StartCoroutine(WaitThenSelect());
    }

    private void BuildSelectableList()
    {
        selectables.Clear();

        foreach (var group in buttonGroups)
        {
            if (group == null || group.buttons == null) continue;

            foreach (var btn in group.buttons)
            {
                if (btn != null)
                    selectables.Add(btn);
            }
        }
    }

    private IEnumerator WaitThenSelect()
    {
        // Tunggu sampai suara scene selesai
        while (AudioManager.Instance != null &&
               AudioManager.Instance.IsVoicePlaying())
        {
            yield return null;
        }

        // buffer kecil biar aman
        yield return new WaitForSeconds(0.1f);

        SelectCurrent();
    }

    private void Update()
    {
        if (KeyBindings.GetDown()) MoveNext();
        if (KeyBindings.GetUp()) MovePrev();
        if (KeyBindings.GetLeft()) MovePrev();
        if (KeyBindings.GetRight()) MoveNext();

        if (KeyBindings.GetConfirm()) ActivateCurrent();

        if (Input.GetKeyDown(KeyBindings.ToggleSound))
            AudioManager.Instance.ToggleSound();

        // J → STOP SEMUA AUDIO
        if (Input.GetKeyDown(KeyCode.J))
        {
            AudioManager.Instance.StopAllAudio();
        }

        // K → ULANGI SCENE ANNOUNCER
        if (Input.GetKeyDown(KeyCode.K))
        {
            AudioManager.Instance.ReplaySceneNarration();
        }

        // BACKSPACE → tombol back
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (backButton != null && backButton.gameObject.activeInHierarchy)
                backButton.onClick.Invoke();
        }

        // ESC → kembali ke Main Menu (SCENE)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (uiSceneManager != null)
                uiSceneManager.GoToMainMenu();
        }
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

        EventSystem.current.SetSelectedGameObject(element.gameObject);
    }

    private void ActivateCurrent()
    {
        var btn = selectables[currentIndex].GetComponent<Button>();
        if (btn != null)
            btn.onClick.Invoke();
    }
}
