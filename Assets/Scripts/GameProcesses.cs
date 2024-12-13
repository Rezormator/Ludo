using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameProcesses : MonoBehaviour
{
    public int currentPlayer;
    public List<Dice> dices;
    public List<bool> diceRolled;
    public List<Pawn> redPawns, greenPawns, yellowPawns, bluePawns;
    public Dictionary<int, List<Pawn>> pawns;
    public List<Spot> redMovementBlock, greenMovementBlock, yellowMovementBlock, blueMovementBlock;
    public Dictionary<int, List<Spot>> movementBlocks;
    public List<GameObject> selected;
    public List<SpotFinal> spotsFinal;
    public List<Text> finalsNumber;
    public Text winnerText;
    private List<string> winners;
    public GameObject winnerScrean;

    public void Start()
    {
        currentPlayer = 0;
        selected[currentPlayer].SetActive(true);
        diceRolled = new List<bool>() { false, false, false, false };
        pawns = new Dictionary<int, List<Pawn>> { { 0, redPawns }, { 1, greenPawns }, { 2, yellowPawns }, { 3, bluePawns } };
        movementBlocks = new Dictionary<int, List<Spot>> { { 0, redMovementBlock }, { 1, greenMovementBlock }, { 2, yellowMovementBlock }, { 3, blueMovementBlock } };
        winners = new List<string> { "Red", "Green", "Yellow", "Blue" };
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void NextPlayer()
    {
        selected[currentPlayer].SetActive(false);
        pawns[currentPlayer].ForEach(pawn => pawn.selected.SetActive(false));

        if (spotsFinal[currentPlayer].pawnCount == 4)
        {
            finalsNumber.ForEach(number => number.text = "");
            winnerText.text = winners[currentPlayer] + " player WIN!";
            currentPlayer = -1;
            winnerScrean.SetActive(true);
            return;
        }

        diceRolled[currentPlayer] = false;
        currentPlayer = (currentPlayer + 1) % 4;
        selected[currentPlayer].SetActive(true);
    }
}
