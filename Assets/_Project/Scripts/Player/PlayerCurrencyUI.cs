using TMPro;
using UnityEngine;

public class PlayerCurrencyUI : MonoBehaviour {
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _textMesh;
    private CurrencySystem _playerCurrencySystem;

    private void Start() {
        _playerCurrencySystem = _player.GetCurrencySystem();
        _playerCurrencySystem.OnCurrencyChanged += UpdateText;
        UpdateText();
    }

    private void UpdateText() {
        _textMesh.text = _playerCurrencySystem.Currency.ToString();
    }
}
