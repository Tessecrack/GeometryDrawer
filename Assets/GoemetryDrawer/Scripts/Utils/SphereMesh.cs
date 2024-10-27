using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Utils
{
    public class SphereMesh : BaseMesh
    {
        private Mesh _planeMesh;
        private Mesh _cubeMesh;
        
        private MeshFilter _meshFilter;
        private List<Vector3> _vertices = new List<Vector3>();
        private List<int> _triangles = new List<int>();

        [SerializeField] private float _size;
        [SerializeField] private int _resolution = 20;
        [SerializeField] private Vector3 _origin;
        [SerializeField] private bool _isSphere = true;

        //helper Variables
        private float _previousSize;
        private int _previousResolution;
        private Vector3 _previousOrigin;
        private bool _previousSphereState;

        public int Resolution => _resolution;
        public float Radius => _size;

        private MeshCollider _meshCollider;

        protected override void Initialize()
        {
            _planeMesh = new Mesh();
            _cubeMesh = new Mesh();
            _meshFilter = this.GetComponent<MeshFilter>();
            _meshCollider = this.GetComponent<MeshCollider>();
            _meshFilter.mesh = new Mesh();
            _meshCollider.sharedMesh = new Mesh();
            
        }

        public void UpdateRadius(float newRadius)
        {
            _size = newRadius;
            UpdateData();
        }

        public void UpdateResolution(int newResolution)
        {
            _resolution = newResolution;
            UpdateData();
        }

        private void Update()
        {
            UpdateData();
        }

        private void UpdateData()
        {
            //clamps resolution avoid errors and performance issues
            _resolution = Mathf.Clamp(_resolution, 1, 30);

            //only generate when changes occur
            if (ValuesHaveChanged())
            {
                GenerateCube(_size, _resolution, _origin);
                if (_isSphere)
                {
                    _cubeMesh.vertices = SpherizeVectors(_cubeMesh.vertices);
                }
                AssignMesh(_cubeMesh);
                _meshCollider.sharedMesh = _cubeMesh;
                //help keep track of changes
                AssignValuesAsPreviousValues();
            }
        }

        void GenerateCube(float size, int resolution, Vector3 origin)
        {
            _vertices.Clear();
            _triangles.Clear();

            Mesh planeMesh = GeneratePlane(size, resolution);

            //FrontFace 
            List<Vector3> frontVertices = ShiftVertices(origin, planeMesh.vertices, -Vector3.forward * size / 2);
            List<int> frontTriangles = new List<int>(planeMesh.triangles);
            _vertices.AddRange(frontVertices);
            _triangles.AddRange(frontTriangles);

            //BackFace
            List<Vector3> backVertices = ShiftVertices(origin, planeMesh.vertices, Vector3.forward * size / 2);
            List<int> backTriangles = ShiftTriangleIndexes(ReverseTriangles(planeMesh.triangles), _vertices.Count);
            _vertices.AddRange(backVertices);
            _triangles.AddRange(backTriangles);

            //Dimension switch
            Mesh rotatedPlane = new Mesh();
            rotatedPlane.vertices = SwitchXAndZ(planeMesh.vertices);
            rotatedPlane.triangles = planeMesh.triangles;

            //RightFace
            List<Vector3> rightVertices = ShiftVertices(origin, rotatedPlane.vertices, Vector3.right * size / 2);
            List<int> rightTriangles = ShiftTriangleIndexes(rotatedPlane.triangles, _vertices.Count);
            _vertices.AddRange(rightVertices);
            _triangles.AddRange(rightTriangles);

            //LeftFace
            List<Vector3> leftVertices = ShiftVertices(origin, rotatedPlane.vertices, Vector3.left * size / 2);
            List<int> leftTriangles = ShiftTriangleIndexes(ReverseTriangles(rotatedPlane.triangles), _vertices.Count);
            _vertices.AddRange(leftVertices);
            _triangles.AddRange(leftTriangles);

            //Dimension switch 2: the enswitchening
            rotatedPlane.vertices = SwitchYAndZ(planeMesh.vertices);
            rotatedPlane.triangles = planeMesh.triangles;

            //TopFace
            List<Vector3> topVertices = ShiftVertices(origin, rotatedPlane.vertices, Vector3.up * size / 2);
            List<int> topTriangles = ShiftTriangleIndexes(rotatedPlane.triangles, _vertices.Count);
            _vertices.AddRange(topVertices);
            _triangles.AddRange(topTriangles);

            //BottomFace
            List<Vector3> bottomVertices = ShiftVertices(origin, rotatedPlane.vertices, Vector3.down * size / 2);
            List<int> bottomTriangles = ShiftTriangleIndexes(ReverseTriangles(rotatedPlane.triangles), _vertices.Count);
            _vertices.AddRange(bottomVertices);
            _triangles.AddRange(bottomTriangles);

            _cubeMesh.Clear();
            _cubeMesh.vertices = _vertices.ToArray();
            _cubeMesh.triangles = _triangles.ToArray();
        }

        Mesh GeneratePlane(float size, int resolution)
        {
            //Create vertices
            List<Vector3> generatedVertices = new List<Vector3>();
            float sizePerStep = size / resolution;
            //Makes sure it's centered in the middle
            Vector3 shiftValue = ((size / 2) * (Vector3.left + Vector3.down));
            for (int y = 0; y < resolution + 1; y++)
            {
                for (int x = 0; x < resolution + 1; x++)
                {
                    generatedVertices.Add(new Vector3(x * sizePerStep, y * sizePerStep, 0) + shiftValue);
                }
            }

            //Create triangles
            List<int> generatedTriangles = new List<int>();
            for (int row = 0; row < resolution; row++)
            {
                for (int column = 0; column < resolution; column++)
                {
                    int i = (row * resolution) + row + column;

                    //first triangle
                    generatedTriangles.Add(i);
                    generatedTriangles.Add(i + (resolution) + 1);
                    generatedTriangles.Add(i + (resolution) + 2);

                    //second triangle
                    generatedTriangles.Add(i);
                    generatedTriangles.Add(i + resolution + 2);
                    generatedTriangles.Add(i + 1);

                    //reverse triangles for visible backside
                    // generatedTriangles.Add(i);
                    // generatedTriangles.Add(i+(resolution)+2);
                    // generatedTriangles.Add(i+(resolution)+1);

                    // generatedTriangles.Add(i);
                    // generatedTriangles.Add(i+1);
                    // generatedTriangles.Add(i+resolution+2);
                }
            }
            _planeMesh.Clear();
            _planeMesh.vertices = generatedVertices.ToArray();
            _planeMesh.triangles = generatedTriangles.ToArray();
            return _planeMesh;
        }

        void AssignMesh(Mesh mesh)
        {
            Mesh filterMesh = _meshFilter.mesh;

            filterMesh.Clear();
            filterMesh.vertices = mesh.vertices;
            filterMesh.triangles = mesh.triangles;

            //_meshCollider.sharedMesh.vertices = filterMesh.vertices;
            //_meshCollider.sharedMesh.triangles = filterMesh.triangles;
        }

        List<Vector3> ShiftVertices(Vector3 origin, Vector3[] vertices, Vector3 shiftValue)
        {
            List<Vector3> shiftedVertices = new List<Vector3>();
            foreach (Vector3 vertex in vertices)
            {
                shiftedVertices.Add(origin + vertex + shiftValue);
            }
            return shiftedVertices;
        }

        int[] ReverseTriangles(int[] triangles)
        {
            System.Array.Reverse(triangles);
            return triangles;
        }

        List<int> ShiftTriangleIndexes(int[] triangles, int shiftValue)
        {
            List<int> newTriangles = new List<int>();
            foreach (int triangleIndex in triangles)
            {
                newTriangles.Add(triangleIndex + shiftValue);
            }
            return newTriangles;
        }

        Vector3[] SwitchXAndZ(Vector3[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                Vector3 value = values[i];
                float storedValue = value.x;
                value.x = value.z;
                value.z = storedValue;
                values[i] = value;
            }
            return values;
        }

        Vector3[] SwitchYAndZ(Vector3[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                Vector3 value = values[i];
                float storedValue = value.y;
                value.y = value.z;
                value.z = storedValue;
                values[i] = value;
            }
            return values;
        }

        Vector3[] SpherizeVectors(Vector3[] vectors)
        {
            for (int i = 0; i < vectors.Length; i++)
            {
                Vector3 vector = vectors[i] - _origin;
                Vector3 sphereVector = vector.normalized * (_size / 2) * 1.67f;
                Vector3 lerpdVector = Vector3.Lerp(vector, sphereVector, 1);
                vectors[i] = _origin + lerpdVector;
            }
            return vectors;
        }

        bool ValuesHaveChanged()
        {
            if (_previousSize != _size || _previousResolution != _resolution || _previousOrigin != _origin || _previousSphereState != _isSphere)
            {
                return true;
            }
            else return false;
        }

        void AssignValuesAsPreviousValues()
        {
            _previousSize = _size;
            _previousResolution = _resolution;
            _previousOrigin = _origin;
            _previousSize = _size;
            _previousResolution = _resolution;
            _previousOrigin = _origin;
            _previousSphereState = _isSphere;
        }
    }
}
