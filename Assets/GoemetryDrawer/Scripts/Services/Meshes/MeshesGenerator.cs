using Assets.GoemetryDrawer.Scripts.Utils;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Services.Meshes
{
    public class MeshesGenerator : MonoBehaviour
    {
        [SerializeField] private SphereMesh _sphereMeshPrefab;
        [SerializeField] private ParallelepipedMesh _parallelepipedMeshPrefab;
        [SerializeField] private Transform _container;

        public SphereMesh GenerateSphereMesh()
        {
            return Instantiate(_sphereMeshPrefab, _container);
        }

        public ParallelepipedMesh GenerateParallelepipedMesh()
        {
            return Instantiate(_parallelepipedMeshPrefab, _container);
        }
    }
}
