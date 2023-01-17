using UnityEngine;
using UnityEngine.UI;

public class OpenWindow : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject window;
    
    private void Awake() => button.EnsureNotNull().onClick.AddListener(() =>
        window.SetActive(!window.activeSelf)
    );
}
