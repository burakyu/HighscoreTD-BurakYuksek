using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
#if UNITY_IOS || UNITY_ANDROID
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
#endif
    }
}
