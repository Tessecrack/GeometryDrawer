using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Utils.Meshes
{
    public class PrismMesh : BaseMesh
    {
        [SerializeField, Range(3, 50)] private int _segmentsAmount = 24;
        [SerializeField, Range(1f, 50f)] private float _height = 1f;
        [SerializeField] private float _radius = 0.5f;
        
        [SerializeField] private bool _endCap = true;

        private int _heightSegments = 24;
        private MeshFilter _filter;
        private MeshRenderer _renderer;

        private MeshCollider _collider;
        private Mesh _mesh;

        public float Radius => _radius;
        public float Segments => _segmentsAmount;
        public float Height => _height;

        protected override void Initialize()
        {
            _filter = GetComponent<MeshFilter>();
            _renderer = GetComponent<MeshRenderer>();
            _collider = GetComponent<MeshCollider>();

            _radius = 15.0f;
            _segmentsAmount = 15;
            _height = 15.0f;

            UpdateData();
            _filter.mesh = _mesh;
        }

        private const int MIN_SEGMENTS_RADIAL = 1;
        private const int MIN_SEGMENTS_HEIGHT = 3;

        public void UpdateRadius(float newRadius)
        {
            _radius = newRadius;
            UpdateData();
        }

        public void UpdateHeight(float newHeight)
        {
            _height = newHeight;
            UpdateData();
        }

        public void UpdateSegments(int segmentsAmount)
        {
            _segmentsAmount = segmentsAmount;
            UpdateData();
        }

        public void UpdateData()
        {
            _segmentsAmount = Mathf.Max(_segmentsAmount, MIN_SEGMENTS_RADIAL);
            _heightSegments = Mathf.Max(_heightSegments, MIN_SEGMENTS_HEIGHT);

            var scale = 1.0f;
            _radius *= scale;
            _height *= scale;

            // how many vertices we need
            int vertCols = _segmentsAmount + 1; // +1 for welding
            int vertRows = _heightSegments + 1;
            int numVerts = vertCols * vertRows;
            int numSlideTris = _segmentsAmount * _heightSegments * 2;
            int numCapTris = _segmentsAmount - 2;
            // 3 places in the array for each tri
            int trisArrayLength = _endCap ? (numSlideTris + numCapTris * 2) * 3 : numSlideTris * 2 * 3;

            Vector3[] vertices = new Vector3[numVerts];
            
            int[] triangles = new int[trisArrayLength];

            float heightStep = _height / _heightSegments;
            float angleStep = 2 * Mathf.PI / _segmentsAmount;

            // A. draw tube
            for (int row = 0; row < vertRows; row++)
            {
                for (int col = 0; col < vertCols; col++)
                {
                    float angle = col * angleStep;

                    float ratio = (float)(row) / vertRows;
                    float radius = mix(_radius, _radius, ratio);

                    // first and last vertex of each row at same spot
                    if (col == vertCols - 1)
                    {
                        angle = 0;
                    }

                    int index = row * vertCols + col;
                    // pos curent vertex
                    vertices[index] = new Vector3(
                        radius * Mathf.Cos(angle),
                        row * heightStep - _height / 2,
                        radius * Mathf.Sin(angle)
                    );

                    if (row == 0 || col >= vertCols - 1)
                    {
                        continue;
                    }

                    int index2 = ((row - 1) * _segmentsAmount * 6 + col * 6);

                    if (_endCap)
                    {
                        index2 += numCapTris * 3;
                    }

                    triangles[index2 + 0] = row * vertCols + col;
                    triangles[index2 + 1] = row * vertCols + col + 1;
                    triangles[index2 + 2] = (row - 1) * vertCols + col;

                    triangles[index2 + 3] = (row - 1) * vertCols + col;
                    triangles[index2 + 4] = row * vertCols + col + 1;
                    triangles[index2 + 5] = (row - 1) * vertCols + col + 1;
                }
            }

            // B. draw caps
            if (_endCap)
            {
                bool isLeftSide = true;
                int leftIndex = 0;
                int rightIndex = 0;
                int middleIndex = 0;
                int topCapVertexOffset = numVerts - vertCols;

                for (int n = 0; n < numCapTris; n++)
                {
                    int bottomCapBaseIndex = n * 3;
                    int topCapBaseIndex = (numCapTris + numSlideTris + n) * 3;

                    if (n == 0)
                    {
                        middleIndex = 0;
                        leftIndex = 1;
                        rightIndex = vertCols - 2;
                        isLeftSide = true;
                    }
                    else if (isLeftSide)
                    {
                        middleIndex = rightIndex;
                        rightIndex--;
                    }
                    else
                    {
                        middleIndex = leftIndex;
                        leftIndex++;
                    }

                    isLeftSide = !isLeftSide;

                    // assign bottom tris
                    triangles[bottomCapBaseIndex + 0] = rightIndex;
                    triangles[bottomCapBaseIndex + 1] = middleIndex;
                    triangles[bottomCapBaseIndex + 2] = leftIndex;
                    // assign top tris
                    triangles[topCapBaseIndex + 0] = topCapVertexOffset + leftIndex;
                    triangles[topCapBaseIndex + 1] = topCapVertexOffset + middleIndex;
                    triangles[topCapBaseIndex + 2] = topCapVertexOffset + rightIndex;
                }
            }
            // else
            // {
            //     for (int row = 0; row < vertRows; row++)
            //     {
            //         for (int col = 0; col < vertCols; col++)
            //         {
            //             if (row == 0 || col >= vertCols - 1)
            //             {
            //                 continue;
            //             }

            //             int index = ((row - 1) * radialSegments * 6 + col * 6) + (trisArrayLength/2);

            //             triangles[index + 0] = row * vertCols + col;
            //             triangles[index + 1] = (row - 1) * vertCols + col;
            //             triangles[index + 2] = row * vertCols + col + 1;

            //             triangles[index + 3] = (row - 1) * vertCols + col;
            //             triangles[index + 4] = (row - 1) * vertCols + col + 1;
            //             triangles[index + 5] = row * vertCols + col + 1;
            //         }
            //     } 
            // }

            Mesh mesh = new()
            {
                vertices = vertices,
                triangles = triangles
            };

            _mesh = mesh;

            _filter.mesh = _mesh;
            _collider.sharedMesh = _mesh;
        }

        private static float mix(float x, float y, float a)
        {
            return x * (1f - a) + y * a;
        }
    }
}
