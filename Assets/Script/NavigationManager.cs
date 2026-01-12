using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NavigationManager : MonoBehaviour
{
    [Tooltip("Isi semua tombol dalam urutan navigasi")]
    public List<Selectable> selectables;

    [SerializeField] private Button backButton;
    [SerializeField] private UISceneManager uiSceneManager;

    private int currentIndex = 0;

    private void OnEnable()
    {
        if (selectables == null || selectables.Count == 0)
            return;

        currentIndex = 0;
        StartCoroutine(WaitThenSelect());
    }

    private IEnumerator WaitThenSelect()
    {
        // Tunggu sampai suara scene selesai
        while (AudioManager.Instance != null &&
               AudioManager.Instance.IsVoicePlaying())
        {
            yield return null;
        }

        // buffer kecil biar aman (1 frame + dikit)
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
