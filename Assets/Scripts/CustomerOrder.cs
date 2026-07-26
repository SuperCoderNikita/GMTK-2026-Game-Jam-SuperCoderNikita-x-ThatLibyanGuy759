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
    public GameObject orderBubble;
    public CustomerTimer status;

    [Range(0f, 1f)]
    public float pizzaChance = 0.2f; // lower = rarer pizza orders

    private bool receivedBurger;
    private bool receivedSoda;
    private bool receivedPizza;

    void Awake()
    {
        bool includesPizza = Random.value < pizzaChance;

        if (includesPizza)
        {
            wantsPizza = true;
            wantsBurger = Random.value < 0.2f;
            wantsSoda = !wantsBurger;
        }
        else
        {
            wantsPizza = false;
            wantsBurger = true;
            wantsSoda = true;
        }

        UpdateIcons();
    }

    void Update()
    {
        if (status.isDisapointed)
        {
            burgerIcon.SetActive(false);
            sodaIcon.SetActive(false);
            pizzaIcon.SetActive(false);
            orderBubble.SetActive(false);
        }
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

        bool complete = (!wantsBurger || receivedBurger) && (!wantsSoda || receivedSoda) && (!wantsPizza || receivedPizza);
        if (complete)
            orderBubble.SetActive(false);

        UpdateIcons();

        return complete ? DeliveryResult.OrderComplete : DeliveryResult.ItemAccepted;
    }
}