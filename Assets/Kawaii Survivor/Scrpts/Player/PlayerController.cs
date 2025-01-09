using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour,IPlayerStatsDependency
{
    [Header("Element")]
    [SerializeField]private MobileJoystick mobileJoystick;
    private Rigidbody2D rb;

    [Header("Setting")]
    [SerializeField] private float BaseMoveSpeed;
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = Vector2.right;
    }

 

    private void FixedUpdate()
    {
        rb.velocity = mobileJoystick.GetMoveVector() * moveSpeed * Time.deltaTime;
    }

    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        float moveSpeedPercent = playerStatsManager.GetStatValue(Stat.MoveSpeed) / 100; 
        moveSpeed = BaseMoveSpeed * (1 + moveSpeedPercent);
    }
}
