using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetSkin(GameObject skinPrefab)
    {
        GetComponent<MeshRenderer>().enabled = false;
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);
        var skin = Instantiate(skinPrefab, transform);
        skin.transform.position = transform.position;
        skin.transform.rotation = transform.rotation;
    }
}
