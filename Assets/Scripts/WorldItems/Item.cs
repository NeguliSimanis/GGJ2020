using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Skull,
    Boot,
    Worm,
    LifePickup
}
public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemType itemType;
    [SerializeField]
    private Sprite itemSprite;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().hasItem = true;
            PickupItem();
        }
    }

    private void PickupItem()
    {
        if (itemType != ItemType.LifePickup)
        {
            GodController godController = GameObject.FindGameObjectWithTag("God").GetComponent<GodController>();
            godController.SubmitItemForQuest(itemType, itemSprite);
        }
        else if (itemType == ItemType.LifePickup)
        {
            //Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            //player.Damage();
        }
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Destroy(this.gameObject);
    }

}
