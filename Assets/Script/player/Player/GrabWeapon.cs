using UnityEngine;

public class GrabWeapon : MonoBehaviour
{
    [SerializeField] private GameObject hand;
    [SerializeField] private bool canGrab = true;

    private void Awake() => hand.EnsureNotNull();

    public bool Grab(GameObject weapon)
    {
        if (canGrab)
        {
             hand = Instantiate(weapon,hand.transform);
             canGrab = false;
             return true;
        }

        return canGrab;
    }
}