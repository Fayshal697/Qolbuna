using UnityEngine;

public class SceneFeatureByIndex : MonoBehaviour
{
    [System.Serializable]
    public class FeatureEntry
    {
        public int identityIndex;
        public GameObject[] featureObjects;
    }

    [Header("Feature Mapping")]
    [SerializeField] private FeatureEntry[] features;

    void Start()
    {
        DisableAll();

        int index = SceneSelectionData.selectedIndex;

        foreach (var feature in features)
        {
            if (feature.identityIndex == index)
            {
                Activate(feature.featureObjects);
                return;
            }
        }

        Debug.LogWarning("No feature matched index: " + index);
    }

    void DisableAll()
    {
        foreach (var feature in features)
            foreach (var go in feature.featureObjects)
                go.SetActive(false);
    }

    void Activate(GameObject[] list)
    {
        foreach (var go in list)
            go.SetActive(true);
    }
}
