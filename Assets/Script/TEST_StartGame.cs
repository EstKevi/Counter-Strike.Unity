using UnityEngine;
using UnityEngine.UI;

public class TEST_StartGame : MonoBehaviour
{
    [SerializeField] private Button buttonHost;
    [SerializeField] private Button buttonClient;
    [SerializeField] private EntryPoint entryPoint;

    private void Awake()
    {
        entryPoint = FindObjectOfType<EntryPoint>().EnsureNotNull();
        
        buttonHost.onClick.AddListener(ChooseHost);
        buttonClient.onClick.AddListener(ChooseClient);
    }

    private void ChooseHost()
    {
        entryPoint.StartGameHost();
        gameObject.SetActive(false);
    }

    private void ChooseClient()
    {
        entryPoint.StartGameClient();
        gameObject.SetActive(false);
    }
}
