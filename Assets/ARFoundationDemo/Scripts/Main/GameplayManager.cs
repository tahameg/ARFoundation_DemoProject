using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace OrbDemo.Gameplay
{
    public enum GameState
    {
        Start,
        HealthCheck,
        RoomScanning,
        Play
    }
    public class GameplayManager : MonoBehaviour
    {
        public delegate void GameStateDelegate(GameState oldState, GameState newState);
        public event GameStateDelegate OnStateChanged;

        [SerializeField]
        ARSessionOrigin m_arOrigin;
        [SerializeField]
        UIManager m_uiManager;
        [SerializeField]
        PlaneHealthChecker m_planeHealthChecker;
        [SerializeField]
        ARPlaneManager m_planeManager;
        
        public PlayStateController PlayStateController;
        public ARObjectManager ObjectManager;
        public GameState State { get; private set; }
        public static GameplayManager Instance;

        void SetGameState(GameState newState)
        {
            if(newState != State)
            {
                GameState oldState = State;
                State = newState;
                OnStateChanged?.Invoke(oldState, newState);
            }
        }

        public void Start()
        {
            State = GameState.Start;
            m_planeManager.enabled = false;
            PlayStateController.enabled = false;
            ObjectManager.Initialize();
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
            StartCoroutine("MainGameRoutine");
        }

        IEnumerator MainGameRoutine()
        {
            m_uiManager.SetUIState(State, GameState.HealthCheck);
            //Do HealthCheck
            SetGameState(GameState.HealthCheck);
            if ((ARSession.state == ARSessionState.None) ||
                (ARSession.state == ARSessionState.CheckingAvailability))
            {
                yield return ARSession.CheckAvailability();
            }

            if (ARSession.state == ARSessionState.Unsupported)
            {
                //Do unsupported device notice
                yield break;
            }
            m_uiManager.SetUIState(State, GameState.RoomScanning);
            SetGameState(GameState.RoomScanning);
            m_planeManager.enabled = true;
            m_planeHealthChecker.StartPlaneHealthCheck();
            while (!m_planeHealthChecker.IsPlaneCheckComplete)
            {
                yield return null;
            }
            m_uiManager.SetUIState(State, GameState.Play);
            SetGameState(GameState.Play);
            PlayStateController.enabled = true;
            m_planeManager.enabled = false;
            foreach(var t in m_planeManager.trackables)
            {
                t.GetComponent<MeshRenderer>().enabled = false;
            }

        }
    }
}

