using System;
using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.UI;
using System.Collections;

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
        playerIndex = PlayerPrefs.GetInt("PlayerColor");
        currentPlayer = 3;
        playerColor = new List<string> { "Red", "Green", "Yellow", "Blue" };
        AllPieces = new Dictionary<int, List<Piece>>
            { { 0, redPieces }, { 1, greenPieces }, { 2, yellowPieces }, { 3, bluePieces } };
        AllCells = new Dictionary<int, List<Cell>> 
            { { 0, redCells }, { 1, greenCells }, { 2, yellowCells }, { 3, blueCells } };
        Invoke(nameof(NextPlayer), 0.1f);
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

        if (currentPlayer != playerIndex)
        {
            StartCoroutine("MakeAIMove");
        }
    }

    public void ClearFromPlayer(int clearFromIndex)
    {
        dices[clearFromIndex].rolled = false;
        AllPieces[clearFromIndex].ForEach(piece => piece.highlight.SetActive(false));
        highlights[clearFromIndex].SetActive(false);
    }

    private IEnumerator MakeAIMove()
    {
        do
        {
            yield return new WaitForSeconds(0.5f);
            dices[currentPlayer].rolled = false;
            dices[currentPlayer].RollDice();
            yield return new WaitForSeconds(1.0f);
            var bestMove = MinMaxAlgorithm(dices[currentPlayer].lastRoll);

            if (bestMove != -1)
            {
                yield return new WaitForSeconds(0.5f);
                if (AllPieces[currentPlayer][bestMove].position + dices[currentPlayer].lastRoll == AllCells[currentPlayer].Count)
                {
                    homeCells[currentPlayer].AddPiece(AllPieces[currentPlayer][bestMove]);
                }
                else
                {
                    AllPieces[currentPlayer][bestMove].MakeMove();
                }
            }
            AllPieces[currentPlayer].ForEach(piece => piece.highlight.SetActive(false));
        } 
        while (dices[currentPlayer].lastRoll == 6);

        NextPlayer();
    }

    private int MinMaxAlgorithm(int diceValue)
    {
        var bestPieceIndex = -1;
        var bestScore = int.MinValue;

        for (var i = 0; i < AllPieces[currentPlayer].Count; i++)
        {
            if (!AllPieces[currentPlayer][i].CanMakeMove(diceValue))
            {
                continue;
            }
            
            var currentPosition = AllPieces[currentPlayer][i].position;

            if (currentPosition == -1)
            {
                return i;
            }

            var score = GetMoveScore(currentPosition + diceValue);
            
            if (score <= bestScore)
            {
                continue;
            }
            
            bestScore = score;
            bestPieceIndex = i;
        }
        
        return bestPieceIndex;
    }

    private int GetMoveScore(int newPosition)
    {
        var score = 0;

        if (newPosition > AllCells[currentPlayer].Count - 6)
        {
            score += 10;
        }

        for (var i = 0; i < playerColor.Count; i++)
        {
            if (i == currentPlayer)
            {
                continue;
            }

            foreach (var opponentPosition in AllPieces[i])
            {
                if (newPosition == opponentPosition.position)
                {
                    score += 20;
                }
                if (Mathf.Abs(newPosition - opponentPosition.position) <= 6)
                {
                    score -= 5;
                }
            }
        }
        
        return score;
    }
}