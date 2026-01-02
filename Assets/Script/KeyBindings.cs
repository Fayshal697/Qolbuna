using UnityEngine;

public static class KeyBindings
{
    // Basic Navigation
    public static KeyCode Up1 = KeyCode.W;
    public static KeyCode Up2 = KeyCode.UpArrow;
    public static KeyCode Down1 = KeyCode.S;
    public static KeyCode Down2 = KeyCode.DownArrow;
    public static KeyCode Left1 = KeyCode.A;
    public static KeyCode Left2 = KeyCode.LeftArrow;
    public static KeyCode Right1 = KeyCode.D;
    public static KeyCode Right2 = KeyCode.RightArrow;

    // Confirm / Select
    public static KeyCode Confirm1 = KeyCode.Return;
    public static KeyCode Confirm2 = KeyCode.Space;

    // Back
    public static KeyCode Back = KeyCode.Escape;

    // Sound toggle
    public static KeyCode ToggleSound = KeyCode.T;

    // Media Player Keys
    public static KeyCode PlayPause = KeyCode.P;
    public static KeyCode Stop = KeyCode.O;
    public static KeyCode Replay = KeyCode.R;
    public static KeyCode Next = KeyCode.N;
    public static KeyCode Prev = KeyCode.M;

    // Helper Functions
    public static bool GetUp()      => Input.GetKeyDown(Up1) || Input.GetKeyDown(Up2);
    public static bool GetDown()    => Input.GetKeyDown(Down1) || Input.GetKeyDown(Down2);
    public static bool GetLeft()    => Input.GetKeyDown(Left1) || Input.GetKeyDown(Left2);
    public static bool GetRight()   => Input.GetKeyDown(Right1) || Input.GetKeyDown(Right2);
    public static bool GetConfirm() => Input.GetKeyDown(Confirm1) || Input.GetKeyDown(Confirm2);
}
