using UnityEngine;
using UnityEditor;
using System.IO;

namespace Osim.Editor
{
    public class MeshCreator : MonoBehaviour
    {
        // -------------------------------------------------------------------------------------------------------------------
        [MenuItem("Assets/Create/Assets/Orbit Meshes")]
        public static void CreateOrbitMeshes()
        {
            var starSystems = AssetDatabase.FindAssets("t:Osim.Assets.StarSystem");
            foreach (var starSystemGuid in starSystems)
            {
                var systemPath = AssetDatabase.GUIDToAssetPath(starSystemGuid);
                var systemDir = Path.GetDirectoryName(systemPath);
                if (systemDir == null) continue;
                var starSystem = (Assets.StarSystem)AssetDatabase.LoadAssetAtPath(systemPath, typeof(Assets.StarSystem));
                foreach (var planet in starSystem.planets)
                {
                    var planetPath = AssetDatabase.GetAssetPath(planet);
                    var planetDir = Path.GetDirectoryName(planetPath);
                    if (planetDir == null) continue;
                    int period = (int)planet.orbit.period;
                    var points = new Vector3[period];
                    var orbitFunction = Instantiator.CreateOrbitFunction(planet.orbit.function);
                    for (int i = 0; i < period; ++i)
                        points[i] = orbitFunction(planet.orbit.startTime + i).ToVector3(starSystem.scale);
                    var mesh = LoadOrCreateAsset<Mesh>(Path.Combine(planetDir, planet.name + "OrbitMesh.mesh"));
                    CreateOrbitMesh(mesh, points);
                    EditorUtility.SetDirty(mesh);
                }
            }
        }

        // -------------------------------------------------------------------------------------------------------------------
        private static T LoadOrCreateAsset<T>(string path) where T : UnityEngine.Object, new()
        {
            var obj = (T)AssetDatabase.LoadAssetAtPath(path, typeof(T));
            if (obj == null)
            {
                obj = new T();
                AssetDatabase.CreateAsset(obj, path);
            }
            return obj;
        }

        // -------------------------------------------------------------------------------------------------------------------
        private static void CreateOrbitMesh(Mesh mesh, Vector3[] points)
        {
            Debug.LogWarning(points.Length);

            int lastPoint = points.Length - 1;
            int vertexCount = points.Length * 2;
            int lastVertex = lastPoint * 2;
            //
            var vertices  = new Vector3[vertexCount + 2];
            var normals   = new Vector3[vertexCount + 2];
            var tangents  = new Vector4[vertexCount + 2];
            var triangles = new int[points.Length * 6];
            // vertices, tangents
            for (int i = 0; i < points.Length; ++i)
            {
                int i2 = i * 2;
                float progress = (float) i / (float) points.Length;
                vertices[i2 + 0] = points[i];
                vertices[i2 + 1] = points[i];
                tangents[i2 + 0] = new Vector4(progress, 0.0f, 0.0f, 0.0f);
                tangents[i2 + 1] = new Vector4(progress, 1.0f, 0.0f, 0.0f);
            }
            vertices[vertexCount + 0] = points[0];
            vertices[vertexCount + 1] = points[0];
            tangents[vertexCount + 0] = new Vector4(1.0f, 0.0f, 0.0f, 0.0f);
            tangents[vertexCount + 1] = new Vector4(1.0f, 1.0f, 0.0f, 0.0f);
            // normals
            normals[0] = points[lastPoint];
            normals[1] = points[1];
            for (int i = 1; i < points.Length - 1; ++i)
            {
                int i2 = i * 2;
                normals[i2 + 0] = points[i - 1];
                normals[i2 + 1] = points[i + 1];
            }
            normals[lastVertex + 0] = points[lastPoint - 1];
            normals[lastVertex + 1] = points[0];
            normals[lastVertex + 2] = normals[0];
            normals[lastVertex + 3] = normals[1];
            // indicies
            for (int i = 0; i < points.Length; ++i)
            {
                int i2 = i * 2;
                int i6 = i * 6;
                triangles[i6 + 0] = i2 + 0;
                triangles[i6 + 1] = i2 + 1;
                triangles[i6 + 2] = i2 + 2;
                triangles[i6 + 3] = i2 + 1;
                triangles[i6 + 4] = i2 + 3;
                triangles[i6 + 5] = i2 + 2;
            }
            //
            mesh.vertices  = vertices;
            mesh.normals   = normals;
            mesh.tangents  = tangents;
            mesh.triangles = triangles;
            mesh.RecalculateBounds();
        }
    }
}

