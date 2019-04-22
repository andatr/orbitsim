namespace Osim.Sim
{
    public class Planet : SpaceBody
    {
        public delegate Vector OrbitFunction(double jde);

        private readonly OrbitFunction _orbitFunction;

        // -------------------------------------------------------------------------------------------------------------------
        public new Assets.Planet asset
        {
            get { return base.asset as Assets.Planet; }
            protected set  { base.asset = value; }
        }

        // -------------------------------------------------------------------------------------------------------------------
        public Planet(ISimulation simulation, Assets.Planet asset, OrbitFunction orbitFunction) : base(simulation, asset)
        {
            _orbitFunction = orbitFunction;
        }

        // -------------------------------------------------------------------------------------------------------------------
        public void UpdatePosition(double time)
        {
            position = _orbitFunction(time);
        }
    }
}
