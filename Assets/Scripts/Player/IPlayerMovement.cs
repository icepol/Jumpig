using UnityEngine;

public interface IPlayerMovement
{
    void MoveStep();
    void MoveTo(Vector3 position);
}