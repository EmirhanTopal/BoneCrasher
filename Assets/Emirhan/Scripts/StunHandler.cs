using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunHandler : MonoBehaviour
{
    public CharacterMovement player1;
    public CharacterMovement player2;

    public void StunBothPlayers(float duration)
    {
        player1.ApplyStun(duration);
        player2.ApplyStun(duration);
    }
}
