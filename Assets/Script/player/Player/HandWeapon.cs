using Script.player;
using Script.player.Inputs;
using Script.player.Inputs.Keyboard;
using Script.player.Inputs.Mouse;
using Script.weapon;
using Unity.Netcode;
using UnityEngine;

public class HandWeapon : NetworkBehaviour
{
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private GameObject hand;
    [SerializeField] private bool canGrab = true;

    private IInputMouse inputMouse = new PlugMouseInput();
    private IInput keyBoardInput = new PlugInput();
    private IGun weapon;

    private void Awake()
    {
        hand.EnsureNotNull();
        playerCamera.EnsureNotNull();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            inputMouse = new KeyMouseInput();
            keyBoardInput = new KeyBoardInput();
        }
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
        if (inputMouse.MouseLeft() && weapon != null)
        {
            weapon.Shoot(playerCamera.HitCollider);
        }

        if (keyBoardInput.R_Button() && weapon != null)
        {
            weapon.Reload();
        }
    }
}