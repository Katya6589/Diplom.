using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public bool isOccupied = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("c1") || other.CompareTag("c2") || other.CompareTag("c3") || other.CompareTag("c4"))
        {
            isOccupied = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("c1") || other.CompareTag("c2") || other.CompareTag("c3") || other.CompareTag("c4"))
        {
            isOccupied = false;
        }
    }

}
