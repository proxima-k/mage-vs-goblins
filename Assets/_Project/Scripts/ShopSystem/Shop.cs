using UnityEngine;

public class Shop : MonoBehaviour {

    [SerializeField] private Player _player;
    private IShopCustomer _shopCustomer;
    [SerializeField] private ShopUI _shopUI;
    
    private void Start() {
        _shopCustomer = _player.GetComponent<IShopCustomer>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            _shopUI.Show(_shopCustomer);
            PlayerInputManager.I.SetPlayerInput(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            _shopUI.Hide();
            PlayerInputManager.I.SetPlayerInput(true);
        }
    }

    private void OnDisable() {
        _shopUI.Hide();
        PlayerInputManager.I.SetPlayerInput(true);
    }
}
