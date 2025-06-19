using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coin : MonoBehaviour
{
    [SerializeField] float _CoinSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, _CoinSpeed, 0);

    }

    private void OnTriggerEnter(Collider other)
    {

        //FindFirstObjectByType<Ciunt_coins>().AddOne();
        FindAnyObjectByType<Count_coin>().AddOne();
        Destroy(gameObject);
        Destroy(gameObject);

    }
}
