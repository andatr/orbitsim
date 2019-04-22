using System;
using UnityEngine;

namespace Osim.Assets
{
    [CreateAssetMenu(fileName = "NewSpaceBody", menuName = "Assets/Space Body")]
    public class SpaceBody : ScriptableObject
    {
        public new string name;
        public GameObject prefab;
        public SpaceBodyIcon icon;
    }

    [Serializable]
    public class SpaceBodyIcon
    {
        public Color color;
        public GameObject prefab;
    }
}
