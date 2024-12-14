using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    public GameState gameState;
    public int playerIndex;
    public bool rolled;
    public int lastRoll;
    private Sprite[] _diceSides;
    private SpriteRenderer _rend;
    
    private void Start()
    {
        rolled = false;
        lastRoll = -1;
        _rend = GetComponent<SpriteRenderer>();
        _diceSides = Resources.LoadAll<Sprite>("DiceSides/");
    }

    private void OnMouseDown()
    {
        if (gameState.currentPlayer == playerIndex && !rolled && playerIndex == gameState.playerIndex)
        {
            rolled = true;
            RollDice();
        }
    }
    
    public void RollDice()
    {
        StartCoroutine("RollDiceE");
    }

    private IEnumerator RollDiceE()
    {
        var randomDiceSide = 0;

        for (var i = 0; i <= 10; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            _rend.sprite = _diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }

        lastRoll = randomDiceSide + 1;
        CheckMoves();
    }

    private void CheckMoves()
    {
        gameState.AllPieces[playerIndex].Where(piece => piece.CanMakeMove(lastRoll))
            .ToList().ForEach(piece => piece.highlight.SetActive(true));

        if (playerIndex == gameState.playerIndex && gameState.AllPieces[playerIndex].All(piece => !piece.CanMakeMove(lastRoll)))
        {
            gameState.NextPlayer();
        }
        // if (gameState.AllPieces[playerIndex].All(piece => !piece.CanMakeMove(lastRoll)))
        // {
        //     gameState.NextPlayer();
        // }
    }
}
