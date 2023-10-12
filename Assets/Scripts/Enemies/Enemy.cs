using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite sprite;
    [SerializeField] private Vector2 rightSidePos;
    [SerializeField] private Vector2 leftSidePos;
    [SerializeField] private float velocity = 10;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

    private void Start()
    {
        StartCoroutine(MoveEnemy());
    }

    private IEnumerator MoveEnemy()
    {
        while (gameObject.activeSelf)
        {
            if (transform.position.x < rightSidePos.x)
            {
                while (transform.position.x < rightSidePos.x)
                {
                    spriteRenderer.flipX = true; // Não inverte o sprite (olhando para a direita)
                    yield return new WaitForSeconds(0.1f);
                    transform.Translate(Vector2.right * velocity * Time.deltaTime);
                }
            }
            else if (transform.position.x > leftSidePos.x)
            {
                while (transform.position.x > leftSidePos.x)
                {
                    spriteRenderer.flipX = false; // Inverte o sprite (olhando para a esquerda)
                    yield return new WaitForSeconds(0.1f);
                    transform.Translate(Vector2.left * velocity * Time.deltaTime);
                }
            }

            yield return new WaitForSeconds(2);
        }
    }
}

