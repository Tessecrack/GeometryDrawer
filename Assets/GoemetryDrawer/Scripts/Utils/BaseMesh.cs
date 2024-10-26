using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Utils
{
    public abstract class BaseMesh : MonoBehaviour
    {
        private void Start()
        {
            Initialize();
        }

        protected abstract void Initialize();
    }
}
