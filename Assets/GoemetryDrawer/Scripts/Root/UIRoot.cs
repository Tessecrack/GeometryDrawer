using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Root
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private Transform _uiContainer;

        public void AttachSceneUI(GameObject sceneUI)
        {
            Clear();
            sceneUI.transform.SetParent(_uiContainer, false);
        }

        private void Clear()
        {
            var childsCount = _uiContainer.childCount;
            for (int i = 0; i < childsCount; i++)
            {
                Destroy(_uiContainer.GetChild(i).gameObject);
            }
        }
    }
}
