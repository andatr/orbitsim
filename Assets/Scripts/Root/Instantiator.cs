using System;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Osim
{
    public static class Instantiator
    {
        // -------------------------------------------------------------------------------------------------------------------
        public static Sim.Planet CreatePlanet(ISimulation simulation, Assets.Planet asset)
        {
            var orbitFunction = CreateOrbitFunction(asset.orbit.function);
            return new Sim.Planet(simulation, asset, orbitFunction);
        }

        // -------------------------------------------------------------------------------------------------------------------
        public static Views.SpaceBody CreateSpaceBodyView(Sim.SpaceBody spaceBody, GameObject parent = null)
        {
            var model = Object.Instantiate(spaceBody.asset.prefab);
            model.SetParent(parent);
            model.name = spaceBody.asset.name + "Planet";
            var view = model.GetComponent<Views.SpaceBody>() ?? model.AddComponent<Views.SpaceBody>();
            view.spaceBody = spaceBody;
            return view;
        }

        // -------------------------------------------------------------------------------------------------------------------
        public static Views.Orbit CreateOrbitView(Sim.Planet planet, GameObject parent = null)
        {
            var model = Object.Instantiate(planet.asset.orbit.prefab);
            model.name = planet.asset.name + "Orbit";
            model.SetParent(parent);
            var view = model.GetComponent<Views.Orbit>() ?? model.AddComponent<Views.Orbit>();
            view.planet = planet;
            return view;
        }

        // -------------------------------------------------------------------------------------------------------------------
        public static Views.SpaceBodyIcon CreateSpaceBodyIcon(Views.SpaceBody spaceBodyView, Camera camera, Canvas canvas,
            ICameraController cameraController, GameObject parent = null)
        {
            var model = Object.Instantiate(spaceBodyView.spaceBody.asset.icon.prefab);
            model.name = spaceBodyView.spaceBody.asset.name + "Icon";
            model.SetParent(parent);
            var view = model.GetComponent<Views.SpaceBodyIcon>() ?? model.AddComponent<Views.SpaceBodyIcon>();
            view.spaceBody = spaceBodyView;
            view.camera = camera;
            view.canvas = canvas;
            view.cameraController = cameraController;
            return view;
        }

        // -------------------------------------------------------------------------------------------------------------------
        public static Sim.Planet.OrbitFunction CreateOrbitFunction(Assets.OrbitFunction function)
        {
            Type orbitMethodClass = Type.GetType(function.className);
            if (orbitMethodClass == null)
            {
                Debug.LogError("CreateOrbitFunction: could not find class '" + function.className + "'");
                return null;
            }
            MethodInfo orbitMethod = orbitMethodClass.GetMethod(function.procName, BindingFlags.Static | BindingFlags.Public);
            if (orbitMethod == null)
            {
                Debug.LogError("CreateOrbitFunction: could not find method '" +
                    function.className + "." + function.procName + "'");
                return null;
            }
            return Delegate.CreateDelegate(typeof(Sim.Planet.OrbitFunction), orbitMethod) as Sim.Planet.OrbitFunction;
        }
    }
}