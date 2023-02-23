using UnityEngine;

namespace Script.UIMenu.mainMenu.WindowsUI
{
    public class CloseWindow : MonoBehaviour
    {
        private void OnDisable()
        {
            gameObject.SetActive(false);
        }
    }
}