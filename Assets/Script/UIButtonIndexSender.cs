using UnityEngine;

public class UIButtonIndexSender : MonoBehaviour
{
   
    public void SendMainIndex(int index)
    {
        UIIndexContext.mainIndex = index;
        UIIndexContext.subIndex = -1; // reset sub tiap ganti fitur

        Debug.Log($"[SEND MAIN] {index}");
    }

    public void SendSubIndex(int index)
    {
        UIIndexContext.subIndex = index;

        Debug.Log($"[SEND SUB] {index}");
    }
}
