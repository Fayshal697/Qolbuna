using UnityEngine;

public class EscapeQuit : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Keluar dari game...");
            Application.Quit();

#if UNITY_EDITOR
            // Supaya bisa testing di editor Unity
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
