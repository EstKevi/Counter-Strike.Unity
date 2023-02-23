using Script.Other;
using Script.UIMenu.mainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace Script.NeedDeleted
{
    public class TEST_StartGame : MonoBehaviour
    {
        [SerializeField] private Button buttonHost;
        [SerializeField] private Button buttonClient;
        [SerializeField] private EntryPoint entryPoint;

        private void Awake()
        {
            entryPoint.EnsureNotNull();
        
            buttonHost.onClick.AddListener(ChooseHost);
            buttonClient.onClick.AddListener(ChooseClient);
        }

        private void ChooseHost()
        {
            entryPoint.StartGame(MainCanvas.ModeGame.Host);
            gameObject.SetActive(false);
        }

        private void ChooseClient()
        {
            entryPoint.StartGame(MainCanvas.ModeGame.Client);
            gameObject.SetActive(false);
        }
    }
}
