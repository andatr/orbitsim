using System;
using UnityEngine;

namespace Osim.Assets
{
    [Serializable]
    public class Orbit
    {
        public OrbitFunction function;
        public double period;
        public double startTime;
        public GameObject prefab;

        public Orbit()
        {
            function = new OrbitFunction();
        }
    }

    [Serializable]
    public class OrbitFunction
    {
        public string className;
        public string procName;
    }
}