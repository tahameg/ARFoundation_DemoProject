using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OrbDemo.Scriptables;


namespace OrbDemo.Gameplay
{
    public class ARObjectManager : MonoBehaviour
    {
        [SerializeField]
        ARObjectData ObjectToSpawn;

        GameObject selectedObject;

        public void Initialize()
        {
            selectedObject = ObjectToSpawn.ObjectPrefab;
        }

        public GameObject CreateSelectedInstance()
        {
            return selectedObject == null ? null : Instantiate(selectedObject);
        }
    }
}
