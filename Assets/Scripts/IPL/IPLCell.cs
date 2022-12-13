using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IPLCellState
{
    Alive, Dead
}

public class IPLCell
{
    private readonly float default_size = 1;

    private float size;

    private Vector2 position;

    private IPLCellState state;

    public float Size { get => size; }
    public Vector2 Position { get => position; set => position = value; }
    public IPLCellState State { get => state; set => state = value; }

    public IPLCell()
    {
        size = default_size;
        state = IPLCellState.Dead;
    }

    public IPLCell(int size)
    {
        this.size = size <= 0 ? default_size : size;
    }

    public void ToggleState() // TOGGLE BETWEEN DEAD OR ALIVE
    {
        if (state == IPLCellState.Alive) 
            state = IPLCellState.Dead;
        else 
            state = IPLCellState.Alive;
    }


}
