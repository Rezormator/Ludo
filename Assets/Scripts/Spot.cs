using UnityEngine;
using UnityEngine.UI;

public class Spot : MonoBehaviour
{
    public bool empty;
    public Pawn pawn;

    public void Strart()
    {
        empty = true;
    }

    public void SetPawn(Pawn pawn)
    {
        if (!empty && this.pawn != null)
        {
            this.pawn.ReturnToStart();
        }

        empty = false;
        this.pawn = pawn;
        pawn.transform.position = this.transform.position;
        pawn.selected.transform.position = this.transform.position;
    }

    public void ClearPawn()
    {
        empty = true;
    }
}
