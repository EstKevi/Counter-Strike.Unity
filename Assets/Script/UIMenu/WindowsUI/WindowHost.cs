using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowHost : MonoBehaviour , IDragHandler
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_Dropdown chooseMap;
    [SerializeField] private TMP_InputField enterIP;
    [SerializeField] private MainCanvas mainCanvas;

    private string ipAddressText;
    public string IpAddressHost() => ipAddressText;

    private void Awake()
    {
        mainCanvas.EnsureNotNull();
        closeButton.EnsureNotNull().onClick.AddListener((() => gameObject.SetActive(false)));
        startButton.EnsureNotNull().onClick.AddListener((() => mainCanvas.StartGameHost.Invoke()));
        chooseMap.EnsureNotNull();
        enterIP.EnsureNotNull().onValueChanged.AddListener((text => ipAddressText = text ));
    }

    public void OnDrag(PointerEventData eventData)
    {
        //TODO перемещение курсором мыши
    }
}