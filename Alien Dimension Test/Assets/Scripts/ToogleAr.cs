using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ToogleAr : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public ARPointCloudManager pointCloudManager;
    public void OnValueChanged(bool isOn)
    {
        VisualizePlanes(isOn);
        VisualizePoints(isOn);
    }
    void VisualizePlanes(bool active)
    {
        planeManager.enabled = active;
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(active);
        }
    }

    void VisualizePoints(bool active)
    {
        pointCloudManager.enabled = active;
        foreach (var point in pointCloudManager.trackables)
        {
            point.gameObject.SetActive(active);
        }
    }
}
