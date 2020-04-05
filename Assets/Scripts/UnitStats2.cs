using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitStats2 : MonoBehaviour
{


    public float curHealth;
    public float maxHealth;
    public float lengthHealth;
    public int protect;
    public int damage;

    void Start()
    {

    }

    void Update()
    {
        if (curHealth <= 0)
            Dead();

    }
    void Dead()
    {
        Destroy(gameObject);
    }

    public void ReceivingDamage(int recDamage)
    {
        curHealth -= recDamage * ((100 - protect) / 100f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("UnitPlayer2"))
            curHealth -= damage * ((100 - protect) / 100f);

    }


}

