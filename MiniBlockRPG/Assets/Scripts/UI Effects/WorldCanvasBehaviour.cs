using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIEffects
{
    [RequireComponent(typeof(Canvas))]
    public class WorldCanvasBehaviour : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        private GameObject mainCamera;

        private void OnEnable()
        {    
            if (mainCamera == null)
            {
                mainCamera = Camera.main.gameObject;
            }

            if (canvas == null)
            {
                canvas = GetComponent<Canvas>();
                canvas.worldCamera = Camera.main;
            }
        }

        private void Update()
        {
            // FIXME: Face camera
            transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
        }
    }
}