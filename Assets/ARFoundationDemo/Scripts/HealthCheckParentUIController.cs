using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrbDemo.Gameplay
{
    public class HealthCheckParentUIController : StateUIParent
    {
        public override bool IsActive { get { return isActive; } }
        bool isActive;
        public override void Activate()
        {
            gameObject.SetActive(true);
            isActive = true;
        }

        public override void Deactivate()
        {
            gameObject.SetActive(false);
            isActive = false;
        }
    }
}
