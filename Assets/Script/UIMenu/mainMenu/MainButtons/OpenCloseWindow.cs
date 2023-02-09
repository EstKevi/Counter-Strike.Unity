using Script.Other;
using UnityEngine;
using UnityEngine.UI;

public class OpenCloseWindow : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject window;
    private const int lastPosition = -1;
    private void Awake()
    {
        var startPosition = window.transform.position;
        button.EnsureNotNull().onClick.AddListener(() =>
        {
            window.SetActive(!window.activeSelf);
            window.transform.SetSiblingIndex(lastPosition);
            window.transform.position = startPosition;
        });
    }
}