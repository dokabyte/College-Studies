using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent (typeof(BoxCollider2D))]
public class Collectable : MonoBehaviour
{
    [SerializeField] private Sprite collectableSprite;
    [SerializeField] private Text AcornText;

    private SpriteRenderer spriteRenderer;
    public float Text;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = collectableSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBehaviour>().acornsCollected++;
            Destroy(this.gameObject);
            AcornText.text = "Acorns " + AcornText;
           
        }
    }
}
