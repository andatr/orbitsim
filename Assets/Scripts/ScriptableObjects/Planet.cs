using System;
using UnityEngine;

namespace Osim.Assets
{
    [CreateAssetMenu(fileName = "NewPlanet", menuName = "Assets/Planet")]
    public class Planet : SpaceBody
    {
        public Orbit orbit;

        // -------------------------------------------------------------------------------------------------------------------
        public Planet()
        {
            orbit = new Orbit();
        }
    }
}
