using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class ButtonGroup
{
    public string groupName;
    public List<Selectable> buttons;
}

public class NavigationManager : MonoBehaviour
{
    public List<ButtonGroup> buttonGroups;
    [HideInInspector] public List<Selectable> selectables = new List<Selectable>();

    [SerializeField] private Button backButton;
    [SerializeField] private UISceneManager uiSceneManager;

    private int currentIndex = 0;

    private void OnEnable()
    {
        BuildSelectableList();
        currentIndex = 0;
        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);
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

    private void Update()
    {
        // =====================
        // Tombol navigasi UI
        // =====================
        if (KeyBindings.GetDown() || KeyBindings.GetRight()) MoveNext();
        if (KeyBindings.GetUp() || KeyBindings.GetLeft()) MovePrev();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            ActivateCurrent();

        if (Input.GetKeyDown(KeyBindings.ToggleSound))
            AudioManager.Instance?.ToggleSound();

        if (Input.GetKeyDown(KeyCode.Backspace))
            backButton?.onClick.Invoke();

        if (Input.GetKeyDown(KeyCode.Escape))
            uiSceneManager?.GoToMainMenu();
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
        if (currentIndex < 0 || currentIndex >= selectables.Count) return;
        EventSystem.current.SetSelectedGameObject(selectables[currentIndex].gameObject);
    }

    private void ActivateCurrent()
    {
        if (selectables.Count == 0) return;
        if (currentIndex < 0 || currentIndex >= selectables.Count) return;

        var btn = selectables[currentIndex].GetComponent<Button>();
        if (btn != null)
            btn.onClick.Invoke();
    }
}
