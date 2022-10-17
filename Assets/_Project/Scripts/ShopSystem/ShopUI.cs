using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour {
    // block template
        // orb sprite and name
        // when clicked, show next upgrade stats
            // get list of attributes
    [SerializeField] private Transform cardPrefab;
    [SerializeField] private Transform cardGrid;
    [SerializeField] private Orb[] _orbs;
    private IShopCustomer _shopCustomer;
    
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
        Button shopButton = card.GetComponent<Button>();
        // might need to refactor this, seems illegal ;-;
        shopButton.onClick.AddListener(() => BuyUpgrade(orb));
    }

    private void BuyUpgrade(Orb orb) {
        if (orb.CanUpgrade()) {
            if (orb.UpgradeOrb(_shopCustomer.GetCurrencySystem()))
                Debug.Log($"Upgrade purchased! {orb.OrbName} is now level {orb.Level}");
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
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}

// block template monobehavior?