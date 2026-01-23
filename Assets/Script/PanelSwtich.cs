using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelSwitcherInput : MonoBehaviour
{
    [Header("Panel Settings (URUTAN PENTING)")]
    public List<GameObject> panels;

    [Header("Arrow Buttons (Optional)")]
    public Button leftArrowButton;
    public Button rightArrowButton;

    public int currentIndex = 0;

    private void Start()
    {
        if (leftArrowButton != null)
            leftArrowButton.onClick.AddListener(PreviousPanel);

        if (rightArrowButton != null)
            rightArrowButton.onClick.AddListener(NextPanel);

        // Sinkron awal
        currentIndex = GetActivePanelIndex();

        ShowPanel(currentIndex);
        UpdateArrowState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            PreviousPanel();

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            NextPanel();
    }

    public void NextPanel()
    {
        currentIndex = GetActivePanelIndex(); 

        if (panels.Count == 0) return;
        if (currentIndex >= panels.Count - 1) return;

        currentIndex++;
        ShowPanel(currentIndex);
        UpdateArrowState();
    }

    public void PreviousPanel()
    {
        currentIndex = GetActivePanelIndex(); 

        if (panels.Count == 0) return;
        if (currentIndex <= 0) return;

        currentIndex--;
        ShowPanel(currentIndex);
        UpdateArrowState();
    }

    private void ShowPanel(int index)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if (panels[i] != null)
                panels[i].SetActive(i == index);
        }
    }

    private void UpdateArrowState()
    {
        if (leftArrowButton != null)
            leftArrowButton.interactable = currentIndex > 0;

        if (rightArrowButton != null)
            rightArrowButton.interactable = currentIndex < panels.Count - 1;
    }


    private int GetActivePanelIndex()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if (panels[i] != null && panels[i].activeSelf)
                return i;
        }

        return 0; // fallback aman
    }
}
