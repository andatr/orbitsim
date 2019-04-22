namespace Osim.Sim
{
    public interface ISpaceBody
    {
        ISimulation simulation { get; }
        Assets.SpaceBody asset { get; }
        Vector position { get; }
    }

    public class SpaceBody : ISpaceBody
    {
        public ISimulation simulation { get; protected set; }
        public Assets.SpaceBody asset { get; protected set; }
        public Vector position { get; protected set; }

        // -------------------------------------------------------------------------------------------------------------------
        public SpaceBody(ISimulation simulation, Assets.SpaceBody asset)
        {
            this.simulation = simulation;
            this.asset = asset;
        }
    }
}
