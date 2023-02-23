using System;
using Script.Other;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UIMenu.mainMenu
{
    public class ChangeViewCanvas : MonoBehaviour
    {
        [SerializeField] private MainCanvas mainCanvas;
        [SerializeField] private Color color;
        [SerializeField] private Image background;
        [SerializeField] private Button[] hideButtons = Array.Empty<Button>();

        private void Awake()
        {
            background.EnsureNotNull();
            mainCanvas.EnsureNotNull().EntryPoint.startGameUnityEvent.AddListener(() =>
                {
                    background.color = color;
                    foreach (var button in hideButtons)
                    {
                        button.gameObject.SetActive(false);
                    }
                }
            );
        }
    }
}
