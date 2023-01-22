using Script.weapon;
using UnityEngine;

public class HandWeapon : MonoBehaviour
{
    [SerializeField] private GameObject hand;
    [SerializeField] private bool canGrab = true;
    private IGun weapon;

    private void Awake() => hand.EnsureNotNull();

    public bool Grab(GameObject weapon)
    {
        if (canGrab)
        {
             hand = Instantiate(weapon,hand.transform);
             hand.TryGetComponent<IGun>(out var gun);
             this.weapon = gun;
             canGrab = false;
             return !canGrab;
        }

        return canGrab;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && weapon != null)
        {
            weapon.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && weapon != null)
        {
            weapon.Reload();
        }
    }
}