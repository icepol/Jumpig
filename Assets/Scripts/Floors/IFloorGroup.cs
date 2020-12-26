using UnityEngine;

public interface IFloorGroup
{
    int Size();
    FloorRow[] Rows();
    SpawnSetup SpawnSetup { get; }
}