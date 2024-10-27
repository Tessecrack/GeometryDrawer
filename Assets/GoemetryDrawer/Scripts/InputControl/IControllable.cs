using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.InputControl
{
    public interface IControllable
    {
        void Move(Vector3 direction);
        void Rotate(Vector3 direction);

        void Select();
    }
}
