using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    //[Header("Elements")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sortingOrder = -(int)(transform.position.y * 10);

    }
}
