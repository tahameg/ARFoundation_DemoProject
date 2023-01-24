using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OrbDemo.Gameplay;

namespace OrbDemo.Scriptables
{
    [CreateAssetMenu(fileName = "NewARObject", menuName = "Orb/Entity")]
    public class ARObjectData : ScriptableObject
    {
        public string Name;
        public Sprite ItemIcon;
        public GameObject ObjectPrefab;
    }
}
