using UnityEngine;

public enum DeliveryResult { WrongItem, ItemAccepted, OrderComplete }

public class CustomerOrder : MonoBehaviour
{
    public bool wantsBurger;
    public bool wantsSoda;

    private bool receivedBurger;
    private bool receivedSoda;
    public GameObject burgerIcon;
    public GameObject sodaIcon;

    void Awake()
    {
        int roll = Random.Range(0, 3); 
        wantsBurger = (roll == 0 || roll == 2);
        wantsSoda = (roll == 1 || roll == 2);
 
        UpdateIcons();
    }

   void UpdateIcons()
    {
        if (burgerIcon != null) burgerIcon.SetActive(wantsBurger && !receivedBurger);
        if (sodaIcon != null) sodaIcon.SetActive(wantsSoda && !receivedSoda);
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

        if (!matched) return DeliveryResult.WrongItem;

        bool complete = (!wantsBurger || receivedBurger) && (!wantsSoda || receivedSoda);
        return complete ? DeliveryResult.OrderComplete : DeliveryResult.ItemAccepted;
    }
}