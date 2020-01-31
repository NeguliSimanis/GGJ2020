using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Skull,
    Boot,
    Knife
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
        if (collision.gameObject.name == "Player")
        {
            PickupItem();
        }
    }

    private void PickupItem()
    {
        Destroy(gameObject);
    }

}
