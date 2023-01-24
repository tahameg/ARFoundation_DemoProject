using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OrbDemo.Gameplay
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        StateUIParent m_healthCheckUIParent;
        [SerializeField]
        StateUIParent m_roomScanUIParent;
        [SerializeField]
        StateUIParent m_playUIParent;

        public void SetUIState(GameState oldState, GameState newState)
        {
            if(oldState == GameState.Start)
            {
                var newParent = GetController(newState);
                newParent.Activate();
            }
            else if (oldState != newState)
            {
                var oldParent = GetController(oldState);
                var newParent = GetController(newState);

                oldParent.Deactivate();
                newParent.Activate();
            }
        }

        StateUIParent GetController(GameState state) => state switch
        {
            GameState.HealthCheck => m_healthCheckUIParent,
            GameState.RoomScanning => m_roomScanUIParent,
            GameState.Play => m_playUIParent,
            _ => null
        };

    }
}
