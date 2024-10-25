using System.Collections.Generic;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Utils
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class ParallelepipedMeshGenerator : MonoBehaviour
    {
        Mesh planeMesh;
        Mesh cubeMesh;
        MeshFilter meshFilter;
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        [SerializeField] float size;
        [SerializeField] int resolution;
        [SerializeField] Vector3 origin;
        [SerializeField] bool isSphere;

        [Range(0, 1)] public float morphValue;

        //helper Variables
        float previousSize;
        int previousResolution;
        Vector3 previousOrigin;
        bool previousSphereState;
        float previousMorphValue;

        void Awake()
        {
            planeMesh = new Mesh();
            cubeMesh = new Mesh();
            meshFilter = this.GetComponent<MeshFilter>();
            meshFilter.mesh = new Mesh();
        }

        void UpdateData()
        {
            //clamps resolution avoid errors and performance issues
            resolution = Mathf.Clamp(resolution, 1, 30);

            //only generate when changes occur
            if (ValuesHaveChanged())
            {
                GenerateCube(size, resolution, origin);
                if (isSphere)
                {
                    cubeMesh.vertices = SpherizeVectors(cubeMesh.vertices);
                }
                AssignMesh(cubeMesh);

                //help keep track of changes
                AssignValuesAsPreviousValues();

            }
        }

        void GenerateCube(float size, int resolution, Vector3 origin)
        {
            vertices.Clear();
            triangles.Clear();

            Mesh planeMesh = GeneratePlane(size, resolution);

            //FrontFace 
            List<Vector3> frontVertices = ShiftVertices(origin, planeMesh.vertices, -Vector3.forward * size / 2);
            List<int> frontTriangles = new List<int>(planeMesh.triangles);
            vertices.AddRange(frontVertices);
            triangles.AddRange(frontTriangles);

            //BackFace
            List<Vector3> backVertices = ShiftVertices(origin, planeMesh.vertices, Vector3.forward * size / 2);
            List<int> backTriangles = ShiftTriangleIndexes(ReverseTriangles(planeMesh.triangles), vertices.Count);
            vertices.AddRange(backVertices);
            triangles.AddRange(backTriangles);

            //Dimension switch
            Mesh rotatedPlane = new Mesh();
            rotatedPlane.vertices = SwitchXAndZ(planeMesh.vertices);
            rotatedPlane.triangles = planeMesh.triangles;

            //RightFace
            List<Vector3> rightVertices = ShiftVertices(origin, rotatedPlane.vertices, Vector3.right * size / 2);
            List<int> rightTriangles = ShiftTriangleIndexes(rotatedPlane.triangles, vertices.Count);
            vertices.AddRange(rightVertices);
            triangles.AddRange(rightTriangles);

            //LeftFace
            List<Vector3> leftVertices = ShiftVertices(origin, rotatedPlane.vertices, Vector3.left * size / 2);
            List<int> leftTriangles = ShiftTriangleIndexes(ReverseTriangles(rotatedPlane.triangles), vertices.Count);
            vertices.AddRange(leftVertices);
            triangles.AddRange(leftTriangles);

            //Dimension switch 2: the enswitchening
            rotatedPlane.vertices = SwitchYAndZ(planeMesh.vertices);
            rotatedPlane.triangles = planeMesh.triangles;

            //TopFace
            List<Vector3> topVertices = ShiftVertices(origin, rotatedPlane.vertices, Vector3.up * size / 2);
            List<int> topTriangles = ShiftTriangleIndexes(rotatedPlane.triangles, vertices.Count);
            vertices.AddRange(topVertices);
            triangles.AddRange(topTriangles);

            //BottomFace
            List<Vector3> bottomVertices = ShiftVertices(origin, rotatedPlane.vertices, Vector3.down * size / 2);
            List<int> bottomTriangles = ShiftTriangleIndexes(ReverseTriangles(rotatedPlane.triangles), vertices.Count);
            vertices.AddRange(bottomVertices);
            triangles.AddRange(bottomTriangles);

            cubeMesh.Clear();
            cubeMesh.vertices = vertices.ToArray();
            cubeMesh.triangles = triangles.ToArray();
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
            planeMesh.Clear();
            planeMesh.vertices = generatedVertices.ToArray();
            planeMesh.triangles = generatedTriangles.ToArray();
            return planeMesh;
        }

        void AssignMesh(Mesh mesh)
        {
            Mesh filterMesh = meshFilter.mesh;
            filterMesh.Clear();
            filterMesh.vertices = mesh.vertices;
            filterMesh.triangles = mesh.triangles;
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
                Vector3 vector = vectors[i] - origin;
                Vector3 sphereVector = vector.normalized * (size / 2) * 1.67f;
                Vector3 lerpdVector = Vector3.Lerp(vector, sphereVector, morphValue);
                vectors[i] = origin + lerpdVector;
            }
            return vectors;
        }

        bool ValuesHaveChanged()
        {
            if (previousSize != size || previousResolution != resolution || previousOrigin != origin || previousSphereState != isSphere || morphValue != previousMorphValue)
            {
                return true;
            }
            else return false;
        }

        void AssignValuesAsPreviousValues()
        {
            previousSize = size;
            previousResolution = resolution;
            previousOrigin = origin;
            previousSize = size;
            previousResolution = resolution;
            previousOrigin = origin;
            previousSphereState = isSphere;
            previousMorphValue = morphValue;
        }
    }
}
