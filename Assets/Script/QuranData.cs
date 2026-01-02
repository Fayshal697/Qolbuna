using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "QuranData", menuName = "Scriptable Objects/QuranData")]
public class QuranData : ScriptableObject
{
    public string surahName;
    public int surahNumber;
    public List<string> ayats;
}

[System.Serializable]
public class AyatData {
    public int ayatNumber;
    [TextArea] public string arabicText;
    public AudioClip audioClip;
    public Sprite image;
}
