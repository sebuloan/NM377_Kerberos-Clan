using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ToogleAR : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public ARPointCloudManager pointCloudManager;

    public void OnValueChange(bool isOn)
    {
        VisualizePlanes(isOn);
        VisualizePoints(isOn);
    }

    void VisualizePlanes(bool active)
    {
        planeManager.enabled = active;
        foreach(ARPlane plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(active);
        }
    }

 
}
