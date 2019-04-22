using UnityEngine;

namespace Osim.Views
{
    public class SpaceBody : MonoBehaviour
    {
        public Sim.SpaceBody spaceBody { get; set; }
        public ICameraController cameraController { get; set; }

        // -------------------------------------------------------------------------------------------------------------------
        public void Update()
        {
            if (spaceBody == null) return;
            transform.position = spaceBody.position.ToVector3(spaceBody.simulation.starSystem.scale);
        }
    }
}