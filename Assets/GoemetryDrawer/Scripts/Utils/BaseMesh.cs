using System;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Utils
{
    public abstract class BaseMesh : MonoBehaviour
    {
        [SerializeField] protected Material _standartMaterial;

        [SerializeField] protected Material _navigateMaterial;

        [SerializeField] protected Material _selectedMaterial;

        [HideInInspector] public Guid Id { get; private set; }

        private MeshRenderer _meshRenderer;

        private void Start()
        {
            Id = Guid.NewGuid();
            _meshRenderer = GetComponent<MeshRenderer>();
            Initialize();
        }

        protected abstract void Initialize();

        public virtual void HighlightNavigation()
        {
            _meshRenderer.material = _navigateMaterial;
        }

        public virtual void HighlightSelected()
        {
            _meshRenderer.material = _selectedMaterial;
        }
    }
}
