using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour {

    [SerializeField] private Transform cardPrefab;
    [SerializeField] private Transform cardGrid;
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private Orb[] _orbs;
    private IShopCustomer _shopCustomer;

    private Button _currButton;
    
    // create block for all upgradable attributes
        // get orbs
        // get abilities from orbs
        // get upgradable stats(attributes) from abilities
        // button upgrade
            // upgradeitem using interactor's currency
        
    private void Awake() {
        foreach (var orb in _orbs) {
            CreateCard(orb);
        }
    }

    private void CreateCard(Orb orb) {
        // if orb is unlocked
        Transform card = Instantiate(cardPrefab, cardGrid, true);
        card.Find("NameText").GetComponent<TextMeshProUGUI>().text = orb.OrbName;
        card.Find("OrbDisplay/Image").GetComponent<Image>().sprite = orb.Icon;
        Button shopButton = card.GetComponent<Button>();
        // might need to refactor this, seems illegal ;-;
        shopButton.onClick.AddListener(() => ClickButton(shopButton, orb));
    }

    private void ClickButton(Button clickedButton, Orb orb) {
        if (_currButton != clickedButton) {
            // show current clicked orb card's stats
            _currButton = clickedButton;
            _textMesh.text = orb.GetOrbInfo();

        } else { 
            // buy upgrade logic
            BuyUpgrade(orb);
            _textMesh.text = orb.GetOrbInfo();
        }
    }
    
    private void BuyUpgrade(Orb orb) {
        if (orb.CanUpgrade()) {
            if (orb.UpgradeOrb(_shopCustomer.GetCurrencySystem())) {
                Debug.Log($"Upgrade purchased! {orb.OrbName} is now level {orb.Level}");
                GameManager.Instance.Save();
            }
        }
        // events firing for buying stuff
        /*
            // check orb upgrade cost
            // if cost < interactor's currency, then upgrade orb
            if (OrbList[0].CanUpgrade())
                if (!OrbList[0].UpgradeOrb(new CurrencySystem(100)))
                    Debug.Log("Not enough currency");
            else {
                Debug.Log("capped"); 
        */
    }
    
    public void Show(IShopCustomer shopCustomer) {
        _shopCustomer = shopCustomer;
        _currButton = null;
        _textMesh.text = "Click a card to show it's properties and stat upgrades.\nClick again to purchase upgrade.";
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}

