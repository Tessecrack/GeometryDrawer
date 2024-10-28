using UnityEngine;
using UnityEngine.UI;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views.Childs
{
    public class InstructionView : MonoBehaviour
    {
        [SerializeField] private Toggle _toggleInstruction;

        [SerializeField] private GameObject _instruction;

        public void HandlerToggleValueChanged(bool value)
        {
            _instruction.gameObject.SetActive(_toggleInstruction.isOn);
        }
    }
}
