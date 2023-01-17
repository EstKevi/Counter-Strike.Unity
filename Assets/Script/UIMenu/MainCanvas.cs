using UnityEngine;
using UnityEngine.Events;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private EntryPoint entryPoint;
    [SerializeField] private WindowHost windowHost;
    [SerializeField] private WindowConnect windowConnect;

    public UnityEvent StartGameHost = new();
    public UnityEvent StartGameClient = new();

    private void Awake()
    {
        entryPoint = FindObjectOfType<EntryPoint>().EnsureNotNull();
        windowHost.EnsureNotNull();
        windowConnect.EnsureNotNull();
    }

    private void Start()
    {
        StartGameHost.AddListener(GameHost);
        StartGameClient.AddListener(GameClient);
    }

    private void GameHost()
    {
        entryPoint.ChangeIpAddress(windowHost.IpAddressHost());
        entryPoint.StartGameHost();
        gameObject.SetActive(false);
    }

    private void GameClient()
    {
        entryPoint.ChangeIpAddress(windowConnect.IpAddressConnect());
        entryPoint.StartGameClient();
        gameObject.SetActive(false);
    }
}
