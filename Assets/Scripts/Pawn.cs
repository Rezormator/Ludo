using UnityEngine;

public class Pawn : MonoBehaviour
{
    public GameProcesses gameProcesses;
    public int playerIndex;
    public int position;
    public Spot startSpot;
    public GameObject selected;

    public void Start()
    {
        position = -1;
    }

    public void ReturnToStart()
    {
        gameProcesses.movementBlocks[playerIndex][position].empty = true;
        startSpot.SetPawn(this);
        position = -1;
    }

    private void OnMouseDown()
    {
        // Кубик не кидали
        if (!gameProcesses.diceRolled[playerIndex])
        {
            return;
        }

        int newPosition = position == -1 ? 0 : position + gameProcesses.dices[playerIndex].lastRoll;

        // Не валідний крок
        if (!CanMakeMove(gameProcesses.dices[playerIndex].lastRoll))
        {
            return;
        }

        if (position != -1)
        {
            gameProcesses.movementBlocks[playerIndex][position].empty = true;
        }

        // Нова точка виявилась фінальною
        if (newPosition == gameProcesses.movementBlocks[playerIndex].Count)
        {
            gameProcesses.spotsFinal[playerIndex].AddPawn(this);
        }
        else 
        {
            gameProcesses.movementBlocks[playerIndex][newPosition].SetPawn(this);
        }

        position = newPosition;
        gameProcesses.NextPlayer();
    }

    public bool CanMakeMove(int diceValue)
    {
        int newPosition = position == -1 ? 0 : position + gameProcesses.dices[playerIndex].lastRoll;

        // Точка за мажеми ігрового поля
        if (gameProcesses.movementBlocks[playerIndex].Count < newPosition)
        {
            return false;
        }
        // Точка фінальна
        if (gameProcesses.movementBlocks[playerIndex].Count == newPosition)
        {
            return true;
        }
        // На точці вже є фігурка цього гравця
        if (gameProcesses.movementBlocks[playerIndex][newPosition].pawn != null)
        {
            if (gameProcesses.movementBlocks[playerIndex][newPosition].pawn.playerIndex == playerIndex && !gameProcesses.movementBlocks[playerIndex][newPosition].empty)
            {
                return false;
            }
        }
        // Знаходиться на початковій точці і на кубику випало менше 6
        if (gameProcesses.dices[playerIndex].lastRoll != 6 && position == -1)
        {
            return false;
        }
        return true;
    }
}
