using System.Collections;
using UnityEngine;
using System.Linq;

public class Dice : MonoBehaviour
{
    public GameProcesses gameProcesses;
    public int playerIndex;
    public int lastRoll { get; private set; }
    private Sprite[] diceSides;
    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
    }

    private void OnMouseDown()
    {
        if (gameProcesses.currentPlayer == playerIndex && !gameProcesses.diceRolled[playerIndex]) {
            StartCoroutine("RollTheDice");
        }
    }

    private IEnumerator RollTheDice()
    {
        int randomDiceSide = 0;

        for (int i = 0; i <= 10; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }

        lastRoll = randomDiceSide + 1;

        if (gameProcesses.pawns[playerIndex].All(pawn => pawn.CanMakeMove(lastRoll) == false)) 
        {
            gameProcesses.NextPlayer();
        }
        else
        {
            gameProcesses.pawns[gameProcesses.currentPlayer].Where(pawn => pawn.CanMakeMove(lastRoll)).ToList().ForEach(pawn => pawn.selected.SetActive(true));
            gameProcesses.diceRolled[playerIndex] = true;
        }
    }
}