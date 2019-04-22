using System;
using UnityEngine;

namespace Osim.Views
{
    public class Orbit : MonoBehaviour
    {
        private Material _material;
        private Sim.Planet _planet;

        // -------------------------------------------------------------------------------------------------------------------
        public Sim.Planet planet
        {
            get { return _planet; }
            set
            {
                _planet = value;
                var material = GetComponent<Renderer>().material;
                material.color = planet.asset.icon.color;
            }
        }

        // -------------------------------------------------------------------------------------------------------------------
        public void Start()
        {
            _material = GetComponent<Renderer>().material;
        }

        // -------------------------------------------------------------------------------------------------------------------
        public void Update()
        {
            if (_material == null || planet == null) return;
            double angle = Math.Abs(planet.simulation.time - planet.asset.orbit.startTime) % planet.asset.orbit.period / planet.asset.orbit.period;
            _material.SetFloat("_PlanetPosition", (float)angle);
        }
    }
}
