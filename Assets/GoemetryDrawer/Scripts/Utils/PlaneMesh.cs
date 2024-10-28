using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Utils
{
    public class PlaneMesh : MonoBehaviour
    {
        [SerializeField] private float _width;
        [SerializeField] private float _height;

        Mesh _mesh;

        Vector3[] _vertices;
        int[] _triangles;
    }
}
