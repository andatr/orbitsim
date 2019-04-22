using UnityEngine;

namespace Osim.Assets
{
    [CreateAssetMenu(fileName = "NewStarSystem", menuName = "Assets/Star System")]
    public class StarSystem : ScriptableObject
    {
        public SpaceBody star;
        public Planet[] planets;
        public double scale;
    }
}