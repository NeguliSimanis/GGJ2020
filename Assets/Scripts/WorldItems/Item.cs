using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum ItemType
{
    Skull,
    Boot,
    Worm,
    LifePickup
}
public class Item : MonoBehaviour
{
    //private AudioSource pickupAudioSource;
    [SerializeField]
    AudioClip itemPickupSFX;

    [SerializeField]
    private ItemType itemType;
    [SerializeField]
    private Sprite itemSprite;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        StartCoroutine("DestroyItem");
    }

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
            Destroy(gameObject);
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

    private IEnumerator DestroyItem()
    {
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }

}
