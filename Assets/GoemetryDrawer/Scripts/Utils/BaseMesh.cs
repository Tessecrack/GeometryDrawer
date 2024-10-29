using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views;
using System;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Utils
{
    public abstract class BaseMesh : MonoBehaviour
    {
        protected const int MIN_AMOUNT_VERTICES = 3;

        [SerializeField] protected Material _standartMaterial;

        [SerializeField] protected Material _selectedMaterial;

        public string Id { get; private set; } = Guid.NewGuid().ToString();

        protected MeshRenderer _meshRenderer;

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
            this.transform.localPosition = Vector3.zero;
        }

        protected abstract void Initialize();

        public virtual void SetStandartMaterial(Material mat)
        {
            _standartMaterial = mat;
        }

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

        public virtual void MoveTo(Vector3 newPosition)
        {
            this.transform.position = newPosition;
        }

        public Primitive GeneratePlane(int amountVertices = 3, float width = 1, float height = 1)
        {
            var angleStep =  (2 * Mathf.PI) / amountVertices;
            var countTriangles = amountVertices - 2;
            var countTrianglesPoints = 3 * amountVertices;

            var vertices = new Vector3[amountVertices];
            for (int i = 0; i < amountVertices; ++i)
            {
                var vertice = new Vector3(Mathf.Cos(angleStep * i) * width, Mathf.Sin(angleStep * i) * height, 0);
                vertices[i] = vertice;
            }

            var triangles = new int[countTrianglesPoints];
            for (int i = 0; i < countTriangles; ++i)
            {
                triangles[i * 3] = 0;
                triangles[(i * 3) + 1] = i + 1;
                triangles[(i * 3) + 2] = i + 2;
            }

            return new Primitive { Triangles = triangles, Vertices = vertices };
        }

        public virtual void Remove()
        {
            Destroy(this.gameObject);
        }
    }

    public class Primitive
    {
        public Vector3[] Vertices { get; set; }
        public int[] Triangles { get; set; }
    }
}
