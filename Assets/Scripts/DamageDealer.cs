using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float damageAmount = 10f;

    public float getDamageAmount()
    {
        return damageAmount;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
