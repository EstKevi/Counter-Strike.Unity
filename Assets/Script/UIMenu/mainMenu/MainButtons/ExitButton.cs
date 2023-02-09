using Script.Other;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private Button exitButton;

    private void Awake() => exitButton.EnsureNotNull().onClick.AddListener((Application.Quit));
}
