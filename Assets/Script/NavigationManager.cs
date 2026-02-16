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
    [HideInInspector] public bool inputEnabled = true;

    private int currentIndex = 0;
    private int lastSelectableCount = -1;

    private bool allowInitialSelection = true;
    private bool hasUserNavigated = false;

    private PanelAudioAnnouncer panelAudio;

    private void OnEnable()
    {
        panelAudio = FindObjectOfType<PanelAudioAnnouncer>();

        // 🔥 JIKA ADA PANEL AUDIO → TUNDA HIGHLIGHT
        if (panelAudio != null && panelAudio.isActiveAndEnabled)
        {
            allowInitialSelection = false;
            hasUserNavigated = false;

            if (EventSystem.current != null)
                EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            // 🔥 SCENE NORMAL / HYPNO
            allowInitialSelection = true;
            hasUserNavigated = true;
        }

        RebuildIfNeeded(true);

        if (allowInitialSelection)
            SelectCurrent();
    }

    private void Update()
    {
        if (!inputEnabled) return;

        RebuildIfNeeded(false);

        // =====================
        // GLOBAL INPUT (SELALU HIDUP)
        // =====================
        if (Input.GetKeyDown(KeyCode.Backspace))
            backButton?.onClick.Invoke();

        if (Input.GetKeyDown(KeyCode.Escape))
            uiSceneManager?.GoToMainMenu();

        if (Input.GetKeyDown(KeyBindings.ToggleSound))
            AudioManager.Instance?.ToggleSound();

        // =====================
        // NAVIGASI PERTAMA (KHUSUS PANEL)
        // =====================
        bool navigationInput =
            KeyBindings.GetDown() ||
            KeyBindings.GetUp() ||
            KeyBindings.GetLeft() ||
            KeyBindings.GetRight();

        if (!hasUserNavigated && navigationInput)
        {
            hasUserNavigated = true;
            allowInitialSelection = true;

            panelAudio?.InterruptByUI();
            SelectCurrent();
            return;
        }

        if (!allowInitialSelection) return;

        // =====================
        // NAVIGASI NORMAL
        // =====================
        if (KeyBindings.GetDown() || KeyBindings.GetRight())
            MoveNext();

        if (KeyBindings.GetUp() || KeyBindings.GetLeft())
            MovePrev();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            ActivateCurrent();
    }

    private void RebuildIfNeeded(bool force)
    {
        BuildSelectableList();

        if (!force && selectables.Count == lastSelectableCount)
            return;

        lastSelectableCount = selectables.Count;
        currentIndex = 0;
    }

    private void BuildSelectableList()
    {
        selectables.Clear();

        foreach (var group in buttonGroups)
        {
            if (group == null || group.buttons == null) continue;

            foreach (var btn in group.buttons)
            {
                if (btn == null) continue;
                if (!btn.gameObject.activeInHierarchy) continue;
                if (!btn.IsInteractable()) continue;

                selectables.Add(btn);
            }
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
        if (!allowInitialSelection || selectables.Count == 0) return;

        int safety = 0;
        while (safety < selectables.Count)
        {
            var current = selectables[currentIndex];

            if (current != null &&
                current.gameObject.activeInHierarchy &&
                current.IsInteractable())
            {
                EventSystem.current.SetSelectedGameObject(current.gameObject);
                return;
            }

            currentIndex = (currentIndex + 1) % selectables.Count;
            safety++;
        }
    }

    private void ActivateCurrent()
    {
        if (!allowInitialSelection) return;
        if (selectables.Count == 0) return;

        var btn = selectables[currentIndex].GetComponent<Button>();
        if (btn != null && btn.IsInteractable())
            btn.onClick.Invoke();
    }
}
