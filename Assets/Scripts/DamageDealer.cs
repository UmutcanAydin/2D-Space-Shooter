using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damageAmount = 10;

    public int getDamageAmount()
    {
        return damageAmount;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
