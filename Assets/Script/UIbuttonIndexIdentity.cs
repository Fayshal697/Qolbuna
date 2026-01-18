using UnityEngine;

public class UIButtonIndexIdentity : MonoBehaviour
{
    [System.Serializable]
    public class SubFeature
    {
        public int subIndex;
        public GameObject[] objects;
    }

    [System.Serializable]
    public class Feature
    {
        public int mainIndex;
        public GameObject root;              // parent fitur
        public SubFeature[] subFeatures;      // optional
    }

    [Header("Feature Setup")]
    public Feature[] features;

    void Start()
    {
        Apply();
    }

    void Apply()
    {
        int main = UIIndexContext.mainIndex;
        int sub = UIIndexContext.subIndex;

        Debug.Log($"[IDENTITY] main={main}, sub={sub}");

        foreach (var feature in features)
        {
            bool isMainActive = feature.mainIndex == main;

            // 🔥 ON / OFF fitur utama
            feature.root.SetActive(isMainActive);

            if (!isMainActive || feature.subFeatures == null)
                continue;

            // 🔍 ADA SUB INDEX
            if (sub != -1)
            {
                foreach (var s in feature.subFeatures)
                {
                    bool active = s.subIndex == sub;
                    SetActive(s.objects, active);
                }
            }
            // 🔍 TIDAK ADA SUB → SEMUA SUB NYALA
            else
            {
                foreach (var s in feature.subFeatures)
                    SetActive(s.objects, true);
            }
        }
    }

    void SetActive(GameObject[] list, bool state)
    {
        foreach (var go in list)
            if (go) go.SetActive(state);
    }
}
