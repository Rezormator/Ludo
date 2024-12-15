using UnityEngine;
using UnityEngine.UI;

public class HomeCell : MonoBehaviour
{
    public GameState gameState;
    public int playerIndex;
    public int piecesCount;
    public Text text;
    public bool win;

    private void Start()
    {
        piecesCount = 0;
        win = false;
    }

    public void AddPiece(Piece piece)
    {
        if (piece.position == -1) 
        {
            piece.startCell.empty = true;
        }
        else
        {
            gameState.AllCells[piece.playerIndex][piece.position].empty = true;
        }
        
        piece.transform.position = transform.position;
        piece.highlight.transform.position = transform.position;
        piece.position = gameState.AllCells[playerIndex].Count;
        piecesCount++;
        text.text = piecesCount.ToString();

        if (piecesCount == 4)
        {
            win = true;
        }
    }
}
