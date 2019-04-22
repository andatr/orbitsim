using Osim.Sim;

namespace Osim
{
    public interface ISimulation
    {
        Assets.StarSystem starSystem { get; }
        SpaceBody star { get; }
        Planet[] planets { get; }
        double time { get; }
    }

    public class Simulation : ISimulation
    {
        public Assets.StarSystem starSystem { get; protected set; }
        public SpaceBody star { get; protected set; }
        public Planet[] planets { get; protected set; }
        public double time { get; protected set; }

        // -------------------------------------------------------------------------------------------------------------------
        public Simulation(Assets.StarSystem starSystem, double initTime)
        {
            this.starSystem = starSystem;
            planets = new Planet[starSystem.planets.Length];
            star = new SpaceBody(this, starSystem.star);
            for (int i = 0; i < planets.Length; ++i)
                planets[i] = Instantiator.CreatePlanet(this, starSystem.planets[i]);
            SetTime(initTime);
        }

        // -------------------------------------------------------------------------------------------------------------------
        public void Simulate(double deltaTime)
        {
            SetTime(time + deltaTime);
        }

        // -------------------------------------------------------------------------------------------------------------------
        public void SetTime(double time)
        {
            this.time = time;
            foreach (var planet in planets)
            {
                planet.UpdatePosition(time);
            }
        }
    }
}
