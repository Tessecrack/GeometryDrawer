using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Meshes
{
    public abstract class GeometryMesh : MonoBehaviour
    {
        protected Mesh _mesh;

        [SerializeField] protected Vector3[] _vertexes;

        [SerializeField] protected int[] _triangles;

        private void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            _mesh = GetComponent<MeshFilter>().mesh;
            _mesh.vertices = _vertexes;
            _mesh.triangles = _triangles;
        }
    }
}
