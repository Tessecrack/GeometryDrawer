using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views;
using System;
using TMPro;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Utils
{
    public abstract class BaseMesh : MonoBehaviour
    {
        [SerializeField] protected Material _standartMaterial;

        [SerializeField] protected Material _selectedMaterial;

        public string Id { get; private set; } = Guid.NewGuid().ToString();

        private MeshRenderer _meshRenderer;

        public bool IsHighlightSelected { get; private set; }
        public bool IsHighlightStandart { get; private set; } = true;

        public BaseView BindedView => _baseView; 

        private BaseView _baseView;

        public void BindView(BaseView baseView)
        {
            _baseView = baseView;
        }

        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            Initialize();
        }

        protected abstract void Initialize();

        public virtual void HighlightStandart()
        {
            _meshRenderer.material = _standartMaterial;

            IsHighlightStandart = true;
            IsHighlightSelected = false;
        }

        public virtual void HighlightSelected()
        {
            _meshRenderer.material = _selectedMaterial;

            IsHighlightStandart = false;
            IsHighlightSelected = true;
        }

        public virtual void RotateMesh(Vector3 direction)
        {
            this.transform.Rotate(Vector3.forward, direction.z);
            this.transform.Rotate(Vector3.right, direction.x);
            this.transform.Rotate(Vector3.up, direction.y);
        }

        public virtual void ResetRotation()
        {
            this.transform.rotation = Quaternion.identity;
        }

        public virtual void MotionMesh(Vector3 direction)
        {
            this.transform.position += direction;
        }

        public virtual void Remove()
        {
            Destroy(this.gameObject);
        }
    }
}
