using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public int playerIndex;
    public int currentPlayer;
    public List<string> playerColor;
    public List<Piece> redPieces, greenPieces, yellowPieces, bluePieces;
    public Dictionary<int, List<Piece>> AllPieces;
    public List<Cell> redCells, greenCells, yellowCells, blueCells;
    public Dictionary<int, List<Cell>> AllCells;
    public List<Dice> dices;
    public List<HomeCell> homeCells;
    public List<GameObject> highlights;
    public GameObject winnerScreen;
    public Text winnerText;

    private void Start()
    {
        currentPlayer = 0;
        playerColor = new List<string> { "Red", "Green", "Yellow", "Blue" };
        AllPieces = new Dictionary<int, List<Piece>>
            { { 0, redPieces }, { 1, greenPieces }, { 2, yellowPieces }, { 3, bluePieces } };
        AllCells = new Dictionary<int, List<Cell>> 
            { { 0, redCells }, { 1, greenCells }, { 2, yellowCells }, { 3, blueCells } };
        highlights[currentPlayer].SetActive(true);
    }

    public void NextPlayer()
    {
        ClearFromPlayer(currentPlayer);

        if (homeCells[currentPlayer].piecesCount == 4)
        {
            homeCells.ForEach(number => number.text.text = "");
            winnerText.text = playerColor[currentPlayer] + " player WIN!";
            winnerScreen.SetActive(true);
            currentPlayer = -1;
            return;
        }
        
        currentPlayer = (currentPlayer + 1) % 4;
        highlights[currentPlayer].SetActive(true);

        // if (currentPlayer != playerIndex)
        // {
        //     MakeAIMove();
        // }
    }

    public void ClearFromPlayer(int clearFromIndex)
    {
        dices[clearFromIndex].rolled = false;
        AllPieces[clearFromIndex].ForEach(piece => piece.highlight.SetActive(false));
        highlights[clearFromIndex].SetActive(false);
    }

    private void MakeAIMove()
    {
        do
        {
            dices[currentPlayer].rolled = false;
            dices[currentPlayer].RollDice();
            var bestMove = MinMaxAlgorithm();

            if (bestMove != -1)
            {
                AllPieces[currentPlayer][bestMove].MakeMove();
            }
        } 
        while (dices[currentPlayer].lastRoll == 6);

        NextPlayer();
    }

    private int MinMaxAlgorithm()
    {
        return -1;
    }
}