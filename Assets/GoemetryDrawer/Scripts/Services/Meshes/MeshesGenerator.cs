using Assets.GoemetryDrawer.Scripts.Utils;
using Assets.GoemetryDrawer.Scripts.Utils.Meshes;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Services.Meshes
{
    public class MeshesGenerator : MonoBehaviour
    {
        [SerializeField] private SphereMesh _sphereMeshPrefab;
        [SerializeField] private ParallelepipedMesh _parallelepipedMeshPrefab;
        [SerializeField] private PrismMesh _prismMeshPrefab;
        [SerializeField] private CapsuleMesh _capsuleMeshPrefab;

        [SerializeField] private Transform _container;

        public SphereMesh GenerateSphereMesh()
        {
            return Instantiate(_sphereMeshPrefab, _container);
        }

        public ParallelepipedMesh GenerateParallelepipedMesh()
        {
            return Instantiate(_parallelepipedMeshPrefab, _container);
        }

        public PrismMesh GeneratePrismMesh()
        {
            return Instantiate(_prismMeshPrefab, _container);
        }

        public CapsuleMesh GenerateCapsuleMesh()
        {
            return Instantiate(_capsuleMeshPrefab, _container);
        }
    }
}
