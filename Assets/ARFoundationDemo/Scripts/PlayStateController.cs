using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace OrbDemo.Gameplay
{
    public enum PlayMode
    {
        EditScene,
        Interact
    }
    public class PlayStateController : MonoBehaviour
    {
        public delegate void ModeDelegate(PlayMode newMode);
        public event ModeDelegate OnModeChange;

        [SerializeField]
        ARRaycastManager m_raycastManager;
        [SerializeField]
        ARSessionOrigin m_arOrigin;

        bool m_isInitialized;

        List<GameObject> m_sceneObjects;
        public GameObject PlaceCursorObjectPrefab;
        List<ARRaycastHit> hitResults;
        GameObject PlaceCursorObject;
        public PlayMode Mode { get; private set; }
        private void OnEnable()
        {
            if (!m_isInitialized)
            {
                Initialize();
                m_isInitialized = true;
            }
        }

        void Initialize()
        {
            m_sceneObjects = new List<GameObject>();
            hitResults = new List<ARRaycastHit>();
            if (PlaceCursorObject == null)
            {
                PlaceCursorObject = Instantiate(PlaceCursorObjectPrefab);
            }
            Mode = PlayMode.EditScene;
            ModeChanged(Mode);
            OnModeChange?.Invoke(Mode);
        }

        void ModeChanged(PlayMode newMode)
        {
            if(newMode == PlayMode.EditScene)
            {
                PlaceCursorObject.SetActive(true);
            }
            else
            {
                if(PlaceCursorObject != null)
                {
                    PlaceCursorObject.SetActive(false);
                }
            }
        }
        public void SetMode(PlayMode newMode)
        {
            if(newMode != Mode)
            {
                Mode = newMode;
                ModeChanged(Mode);
                OnModeChange?.Invoke(Mode);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(m_isInitialized)
            {
                if (Mode == PlayMode.EditScene)
                {
                    OperateEditMode();
                }
                else
                {
                    OperateInteractMode();
                }

                if (Input.GetMouseButton(0))
                {
                    if (Mode == PlayMode.EditScene && PlaceCursorObject.activeInHierarchy)
                    {
                        GameObject createdObject = GameplayManager.Instance.ObjectManager.CreateSelectedInstance();
                        createdObject.transform.position = PlaceCursorObject.transform.position;
                        m_sceneObjects.Add(createdObject);
                        SetMode(PlayMode.Interact);
                    }
                }
            }
        }

        void OperateEditMode()
        {
            Ray ray = new Ray(m_arOrigin.camera.transform.position, m_arOrigin.camera.transform.forward);
            if (m_raycastManager.Raycast(ray, hitResults))
            {
                DebugLogController.Write("RaycastHit");
                if (!PlaceCursorObject.activeInHierarchy)
                {
                    PlaceCursorObject.SetActive(true);
                }
                PlaceCursorObject.transform.position = hitResults[0].pose.position;
            }
            else
            {
                DebugLogController.Write("Didn't hit");
                if (PlaceCursorObject.activeInHierarchy)
                {
                    PlaceCursorObject.SetActive(false);
                }
            }
        }

        void OperateInteractMode()
        {

        }
    }
}
