using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveCounter : MonoBehaviour
{

    public TMP_Text movesText;
    public int moves;


    public void SetMoves(int moves)
    {
        this.moves = moves;
        movesText.text = moves.ToString();
    }
    
    public void DecMove()
    {
        moves--;
        movesText.text = moves.ToString();
    }
}
