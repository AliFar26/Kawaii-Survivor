using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro damageText;

    //[Header(" Movement ")]
    //[SerializeField] private float moveAmplitude;
    //[SerializeField] private float moveSpeed;

    private void Start()
    {
        //Configure(Random.Range(1, 500).ToString());
    }

    [NaughtyAttributes.Button]
    public void Animate(string damage,bool isCriticalHit)
    {
        damageText.text = damage.ToString();
        damageText.color = isCriticalHit ? Color.yellow : Color.white;
        animator.Play("Animate");

    }



    public void Configure(string textString)
    {
        //text.text = textString;
        animator.Play("Animate");
    }
}
