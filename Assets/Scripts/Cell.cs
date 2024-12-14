using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool empty;
    public Piece piece;

    private void Start()
    {
        empty = true;
    }

    public void SetPiece(Piece newPiece)
    {
        if (newPiece.position == -1) 
        {
            newPiece.startCell.empty = true;
        }
        else
        {
            newPiece.gameState.AllCells[newPiece.playerIndex][newPiece.position].empty = true;
        }
        
        piece = newPiece;
        piece.transform.position = transform.position;
        piece.highlight.transform.position = transform.position;
        empty = false;
    }
    
    public void Clear()
    {
        if (empty || piece == null)
        {
            return;
        }
        
        piece.MoveToStartCell();
        empty = true;
    }
}
