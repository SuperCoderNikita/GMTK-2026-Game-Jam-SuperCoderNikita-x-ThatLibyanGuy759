using UnityEngine;

public enum DeliveryResult { WrongItem, ItemAccepted, OrderComplete }

public class CustomerOrder : MonoBehaviour
{
    public bool wantsBurger;
    public bool wantsSoda;
    public bool wantsPizza;

    public GameObject burgerIcon;
    public GameObject sodaIcon;
    public GameObject pizzaIcon;

    private bool receivedBurger;
    private bool receivedSoda;
    private bool receivedPizza;

    void Awake()
    {
        // pick 2 distinct items out of 3: 0 = Burger, 1 = Soda, 2 = Pizza
        int first = Random.Range(0, 3);
        int second;
        do { second = Random.Range(0, 3); } while (second == first);

        wantsBurger = (first == 0 || second == 0);
        wantsSoda   = (first == 1 || second == 1);
        wantsPizza  = (first == 2 || second == 2);

        UpdateIcons();
    }

    void UpdateIcons()
    {
        if (burgerIcon != null) burgerIcon.SetActive(wantsBurger && !receivedBurger);
        if (sodaIcon != null) sodaIcon.SetActive(wantsSoda && !receivedSoda);
        if (pizzaIcon != null) pizzaIcon.SetActive(wantsPizza && !receivedPizza);
    }

    public DeliveryResult DeliverItem(HeldItem item)
    {
        bool matched = false;

        if (item == HeldItem.Burger && wantsBurger && !receivedBurger)
        {
            receivedBurger = true;
            matched = true;
        }
        else if (item == HeldItem.Soda && wantsSoda && !receivedSoda)
        {
            receivedSoda = true;
            matched = true;
        }
        else if (item == HeldItem.Pizza && wantsPizza && !receivedPizza)
        {
            receivedPizza = true;
            matched = true;
        }

        if (!matched) return DeliveryResult.WrongItem;

        bool complete = (!wantsBurger || receivedBurger)
                      && (!wantsSoda || receivedSoda)
                      && (!wantsPizza || receivedPizza);

        UpdateIcons();

        return complete ? DeliveryResult.OrderComplete : DeliveryResult.ItemAccepted;
    }
}