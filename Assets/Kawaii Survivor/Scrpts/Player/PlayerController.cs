using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Element")]
    [SerializeField]private MobileJoystick mobileJoystick;

    [Header("Setting")]
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.velocity = mobileJoystick.GetMoveVector() * moveSpeed * Time.deltaTime;
    }
}
