using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Utils.Meshes
{
    public class ParallelepipedMesh : BaseMesh
    {
        [SerializeField] private float _width = 5f;
        [SerializeField] private float _height = 5f;
        [SerializeField] private float _depth = 5f;

        private float _previousWidth;
        private float _previousHeight;
        private float _previousLength;

        private Vector3 _position = Vector3.zero;

        private Mesh _mesh;
        private MeshCollider _meshCollider;

        private Vector3[] _vertices;
        private int[] _triangles;

        public float Width => _width;
        public float Height => _height;
        public float Depth => _depth;



        protected override void Initialize()
        {
            _mesh = GetComponent<MeshFilter>().mesh;
            _meshCollider = this.GetComponent<MeshCollider>();
            _position = this.transform.position;
            
            _width = 15f;
            _height = 15f;
            _depth = 15f;

            UpdatePrimitive();
        }

        public void UpdatePosition(Vector3 newPosition)
        {
            _position = newPosition;
            UpdatePrimitive();
        }

        public void UpdateHeight(float newHeight)
        {
            _height = newHeight;
            UpdatePrimitive();
        }

        public void UpdateWidth(float newWidth)
        {
            _width = newWidth;
            UpdatePrimitive();
        }

        public void UpdateLength(float newLength)
        {
            _depth = newLength;
            UpdatePrimitive();
        }

        private void UpdatePrimitive()
        {
            //var frontPlane = GeneratePlane(4, _width, _height);

            GenerateVertices();
            GenerateTriangles();

            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles;

            _meshCollider.sharedMesh = _mesh;
            //this.transform.position = _position;
        }

        private void GenerateVertices()
        {
            var posX = 0;//_position.x;
            var posY = 0;//_position.y;
            var posZ = 0;//_position.z;

            _vertices = new Vector3[8]
            {
                new Vector3(posX - _width / 2, posY - _height / 2, posZ - _depth / 2),   // 0
                new Vector3(posX - _width / 2, posY + _height / 2, posZ - _depth / 2),   // 1
                new Vector3(posX + _width / 2, posY - _height / 2, posZ - _depth / 2),   // 2
                new Vector3(posX + _width / 2, posY + _height / 2, posZ - _depth / 2),   // 3

                new Vector3(posX - _width / 2, posY - _height / 2, posZ + _depth / 2),   // 4
                new Vector3(posX - _width / 2, posY + _height / 2, posZ + _depth / 2),   // 5

                new Vector3(posX + _width / 2, posY + _height / 2, posZ + _depth / 2),    // 6
                new Vector3(posX + _width / 2, posY - _height / 2, posZ + _depth / 2)     // 7
            };
        }

        private void GenerateTriangles()
        {
            _triangles = new int[] 
            { 
                0, 1, 2, 2, 1, 3,
                0, 4, 1, 4, 5, 1,
                1, 5, 6, 1, 6, 3,
                4, 6, 5, 6, 4, 7,
                6, 2, 3, 2, 6, 7,
                4, 0, 7, 0, 2, 7
            };
        }
    }
}