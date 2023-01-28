using Script.player.Inputs;
using Script.weapon;
using Unity.Netcode;
using UnityEngine;

public class HandWeapon : NetworkBehaviour
{
    [SerializeField] private GameObject hand;
    [SerializeField] private bool canGrab = true;
    private IGun weapon;
    private IInputMouse Mouse = new PlugMouseInput();
    private IInput KeyBoard = new PlugInput();

    private void Awake() => hand.EnsureNotNull();

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(!IsOwner)return;
        
        KeyBoard = new KeyBoardInput();
        Mouse = new KeyMouseInput();
    }

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
        if (Mouse.LeftMouseButton() && weapon != null)
        {
            weapon.Shoot();
        }

        if (KeyBoard.R_Button() && weapon != null)
        {
            weapon.Reload();
        }
    }
}