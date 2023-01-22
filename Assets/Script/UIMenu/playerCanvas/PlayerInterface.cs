using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    [SerializeField] private EntryPoint entryPoint;
    private void Awake()
    {
        entryPoint = FindObjectOfType<EntryPoint>().EnsureNotNull();
    }
}