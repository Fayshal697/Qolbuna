using UnityEngine;

public class UIButtonIndexSender : MonoBehaviour
{
    public void SendIndex(int buttonIndex)
    {
        SceneSelectionData.selectedIndex = buttonIndex;
    }
}
