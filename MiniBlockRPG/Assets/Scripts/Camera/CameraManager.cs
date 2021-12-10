using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraManager : MonoBehaviour
{
    [System.Serializable]
    public struct CameraInfo
    {
        public GameObject camera;
        public KeyCode swapToKey;
    }

    private GameObject activeCamera = null;

    public CameraInfo[] cameras;

    private void Start()
    {
        activeCamera = cameras.FirstOrDefault().camera;
    }

    void Update()
    {
        // FIXME: Move to new INPUT SYSTEM
        foreach (var item in cameras)
        {
            if (Input.GetKeyDown(item.swapToKey))
            {
                if (item.camera.activeSelf == true)
                {
                    item.camera.SetActive(false);
                    activeCamera = cameras.FirstOrDefault().camera;
                }
                else
                {
                    activeCamera.SetActive(false);
                    activeCamera = item.camera;
                }
            }
        }

        activeCamera.SetActive(true);
    }
}
