using Unity.VisualScripting;
using UnityEngine;

public class SpotFinal : MonoBehaviour
{
    public GameProcesses gameProcess;
    public int playerIndex;
    public int pawnCount;
    public bool win;

    void Start()
    {
        pawnCount = 0;
        win = false;
    }

    public void AddPawn(Pawn pawn)
    {
        pawn.transform.position = this.transform.position;
        pawnCount++;
        gameProcess.finalsNumber[playerIndex].text = pawnCount.ToString();

        if (pawnCount == 4)
        {
            win = true;
        }
    }
}
