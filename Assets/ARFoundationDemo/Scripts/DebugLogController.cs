using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DebugLogController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI debugText;

    static DebugLogController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static void Write(object obj)
    {
        instance.debugText.text = obj.ToString();
    }

    public static void Clear()
    {
        instance.debugText.text = "";
    }
}
