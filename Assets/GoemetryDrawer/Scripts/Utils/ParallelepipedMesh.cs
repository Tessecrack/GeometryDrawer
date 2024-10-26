using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.GoemetryDrawer.Scripts.Utils
{
    public class ParallelepipedMesh : MonoBehaviour
    {
        [SerializeField] private float _width = 5f;
        [SerializeField] private float _height = 5f;
        [SerializeField] private float _length = 5f;

        private float _previousWidth;
        private float _previousHeight;
        private float _previousLength;

        private Vector3 _position = Vector3.zero;

        private Mesh _mesh;

        private Vector3[] _vertices;
        private int[] _triangles;

        private void Start()
        {
            _mesh = GetComponent<MeshFilter>().mesh;
            _position = this.transform.position;

            _width = 5f;
            _height = 5f;
            _length = 5f;

            UpdatePrimitive();
        }

        public void UpdatePosition(Vector3 newPosition)
        {
            _position = newPosition;
            UpdatePrimitive();
        }

        public void UpdateHeight(float newHeight)
        {
            Debug.Log(newHeight);
            _height = newHeight;
            UpdatePrimitive();
        }

        public void UpdateWidth(float newWidth)
        {
            Debug.Log(newWidth);
            _width = newWidth;
            UpdatePrimitive();
        }

        public void UpdateLength(float newLength)
        {
            Debug.Log(newLength);
            _length = newLength;
            UpdatePrimitive();
        }

        private void Update()
        {
            if (_previousWidth == _width && _previousHeight == _height && _previousLength == _length)
            {
                return;
            }

            //UpdatePrimitive();
        }

        private void UpdatePrimitive()
        {
            GenerateVertices();
            GenerateTriangles();

            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles;
            this.transform.position = _position;
        }

        private void GenerateVertices()
        {
            var posX = _position.x;
            var posY = _position.y;
            var posZ = _position.z;

            _vertices = new Vector3[8]
            {
                new Vector3(posX - _width / 2, posY - _height / 2, posZ - _length / 2), // 0
                new Vector3(posX - _width / 2, posY + _height / 2, posZ - _length / 2), // 1
                new Vector3(posX + _width / 2, posY - _height / 2, posZ - _length / 2), // 2
                new Vector3(posX + _width / 2, posY + _height / 2, posZ - _length / 2), // 3

                new Vector3(posX + _width / 2, posY - _height / 2, posZ + _length / 2),   // 4
                new Vector3(posX - _width / 2, posY + _height / 2, posZ + _length / 2),// 5

                new Vector3(posX + _width / 2, posY + _height / 2, posZ + _length / 2), // 6
                new Vector3(posX + _width / 2, posY - _height / 2, posZ + _length / 2)     // 7
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