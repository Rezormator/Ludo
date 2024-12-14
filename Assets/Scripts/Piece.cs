using System;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public GameState gameState;
    public int playerIndex;
    public int position;
    public Cell startCell;
    public GameObject highlight;

    private void Start()
    {
        position = -1;
    }

    private void OnMouseDown()
    {
        if (playerIndex != gameState.playerIndex || !gameState.dices[playerIndex].rolled)
        {
            return;
        }

        var diceValue = gameState.dices[playerIndex].lastRoll;
        if (!CanMakeMove(diceValue))
        {
            return;
        }
        
        var newPosition = position == -1 ? 0 : position + diceValue;
        if (newPosition == gameState.AllCells[playerIndex].Count)
        {
            gameState.audioManager.PlaySound(gameState.audioManager.piece);
            gameState.homeCells[playerIndex].AddPiece(this);
        }
        else
        {
            MoveToCell(newPosition);
        }

        if (diceValue == 6)
        {
            gameState.ClearFromPlayer(playerIndex);
            gameState.currentPlayer = (gameState.currentPlayer + 3) % 4;
        }
        
        gameState.NextPlayer();
    }

    private void MoveToCell(int cellIndex)
    {
        gameState.AllCells[playerIndex][cellIndex].Clear();
        gameState.AllCells[playerIndex][cellIndex].SetPiece(this);
        position = cellIndex;
    }

    public void MoveToStartCell()
    {
        position = -1;
        startCell.SetPiece(this);
    }

    public void MakeMove()
    {
        gameState.audioManager.PlaySound(gameState.audioManager.piece);
        MoveToCell(position == -1 ? 0 : position + gameState.dices[playerIndex].lastRoll);
    }

    public bool CanMakeMove(int diceValue)
    {
        var newPosition = position == -1 ? 0 : position + diceValue;
        if (newPosition == gameState.AllCells[playerIndex].Count)
        {
            return true;
        }
        if (newPosition > gameState.AllCells[playerIndex].Count)
        {
            return false;
        }

        var cell = gameState.AllCells[playerIndex][newPosition];
        if (cell.piece != null && cell.piece.playerIndex == playerIndex && !cell.empty)
        {
            return false;
        }
        if (newPosition == 0 && diceValue != 6)
        {
            return false;
        }

        return true;
    }
}
