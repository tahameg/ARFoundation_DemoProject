using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class DeviceHealthChecker
{
    bool isChecking;

    public void StartCompatibilityCheck()
    {
        if (!isChecking)
        {
            isChecking = true;
        }
    }

    
}
