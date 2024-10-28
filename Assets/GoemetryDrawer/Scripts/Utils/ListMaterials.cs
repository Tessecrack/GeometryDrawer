using System.Collections.Generic;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Utils
{
    public class ListMaterials : MonoBehaviour
    {
        [SerializeField] private List<Material> _materials;

        public Material GetMaterial(int index)
        {
            var tempIndex = Mathf.Max(0, index);
            tempIndex = Mathf.Min(tempIndex, _materials.Count - 1);
            return _materials[tempIndex];
        }
    }
}
