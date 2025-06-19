using UnityEngine;

public class Stolknovenie : MonoBehaviour
{
    
    public Peremeshenie_cuba moveScript;
    public string[] cubeTags;
    
    void Start()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (string tag in cubeTags)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                //Debug.Log("�� ��������!");
                UIManager.Instance.OnGameFinished(false);
                moveScript.SetMoveEnabled(false);
                
                break; 
            }
        }
    }
}
