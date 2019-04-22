using System;
using UnityEngine;

namespace Osim
{
    [Serializable]
    public class SimSettings
    {
        public double time;
        public double speed;
    }

    public class Root : MonoBehaviour
    {
        private Simulation _simulation;
        private CameraController _cameraController;

        public Assets.StarSystem starSystem;
        public SimSettings simSettings;

        // -------------------------------------------------------------------------------------------------------------------
        public void Start()
        {
           // Physics.queriesHitTriggers = true;

            if (starSystem == null) return;
            _simulation = new Simulation(starSystem, simSettings.time);
            _cameraController = GetComponent<CameraController>();
            _cameraController.camera = Camera.main;
            CreateViews();
        }

        // -------------------------------------------------------------------------------------------------------------------
        public void FixedUpdate()
        {
            if (_simulation == null) return;
            _simulation.Simulate(Time.deltaTime * simSettings.speed);
        }

        // -------------------------------------------------------------------------------------------------------------------
        private void CreateViews()
        {
            var bodies = new GameObject("Bodies");
            var canvasObject = GameObject.Find("Canvas");
            var canvas = canvasObject.GetComponent<Canvas>();
            var iconContainer = canvasObject.transform.Find("Bodies").gameObject;
            // star view
            var starView = Instantiator.CreateSpaceBodyView(_simulation.star, bodies);
            Instantiator.CreateSpaceBodyIcon(starView, Camera.main, canvas, _cameraController, iconContainer);
            _cameraController.Target = starView.gameObject;
            // planet views
            for (int i = 0; i < starSystem.planets.Length; ++i)
            {
                var container = new GameObject(starSystem.planets[i].name);
                container.SetParent(bodies);
                var planetView = Instantiator.CreateSpaceBodyView(_simulation.planets[i], container);
                Instantiator.CreateOrbitView(_simulation.planets[i], container);
                Instantiator.CreateSpaceBodyIcon(planetView, Camera.main, canvas, _cameraController, iconContainer);
            }
        }
    }
}