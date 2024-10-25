using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Meshes
{
    public class PlaneMesh : GeometryMesh
    {
        protected override void Initialize()
        {
            _vertexes = new Vector3[4];

            _vertexes[0] = new Vector3(0, 0, 0);
            _vertexes[1] = new Vector3(0, 1, 0);
            _vertexes[2] = new Vector3(1, 1, 0);
            _vertexes[3] = new Vector3(1, 0, 0);

            _triangles = new int[6];

            _triangles[0] = 0;
            _triangles[0] = 1;
            _triangles[0] = 2;

            _triangles[0] = 2;
            _triangles[0] = 1;
            _triangles[0] = 3;

            _mesh = GetComponent<MeshFilter>().mesh;
            _mesh.vertices = _vertexes;
            _mesh.triangles = _triangles;
        }
    }
}
