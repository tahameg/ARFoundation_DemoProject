using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

namespace OrbDemo.Gameplay
{
    public class PlaneHealthChecker : MonoBehaviour
    {
        [SerializeField]
        ARPlaneManager m_planeManager;
        [SerializeField]
        ARSession m_Session;
        [SerializeField]
        float targetAreaSize;
        public bool IsPlaneCheckComplete { get; private set; }

        bool start = false;
        public void StartPlaneHealthCheck()
        {
            start = true;
        }


        float CalculateRectSize(Vector2 size)
        {
            return Mathf.Sqrt(Mathf.Pow(size.x, 2f) + Mathf.Pow(size.y, 2f));
        }
        private void Update()
        {
            if (m_Session.isActiveAndEnabled && !IsPlaneCheckComplete && start)
            {
                float area = 0.0f;
                foreach (var p in m_planeManager.trackables)
                {
                    area += CalculateRectSize(p.size);
                    
                }
                DebugLogController.Write($"total {area} printing");
                if (area >= targetAreaSize)
                {
                    IsPlaneCheckComplete = true;
                }
            }
        }
    }
}
