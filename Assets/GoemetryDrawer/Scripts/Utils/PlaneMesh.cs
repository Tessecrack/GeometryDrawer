using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Utils
{
    public class PlaneMesh : MonoBehaviour
    {
        [SerializeField] private float _width;
        [SerializeField] private float _height;

        private float _previousWidth;
        private float _previousHeight;

        Mesh _mesh;

        Vector3[] _vertices;
        int[] _triangles;

        private void Start()
        {
            _mesh = GetComponent<MeshFilter>().mesh;
        }

        private void Update()
        {
            if (_previousWidth == _width && _previousHeight == _height)
            {
                return;
            }

            UpdatePrimitive();
        }

        private void UpdatePrimitive()
        {
            GenerateVertices();
            GenerateTriangles();

            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles;
        }

        private void GenerateVertices()
        {
            _vertices = new Vector3[4]
            {
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(0.0f, _height, 0.0f),
                new Vector3(_width, 0.0f, 0.0f),
                new Vector3(_width, _height, 0.0f),
            };
        }

        private void GenerateTriangles()
        {
            _triangles = new int[] { 0, 1, 2, 2, 1, 3 };
        }
    }
}
