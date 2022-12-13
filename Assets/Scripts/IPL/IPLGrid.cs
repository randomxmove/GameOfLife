using System;
using UnityEngine;

public class IPLGrid
{
    public static int default_size = 10;
    public static int default_width = 50;
    public static int default_height = 50;
    public static float default_simulationSpeed = 1;

    private int width;
    private int height;

    private int cellSize;

    private int generationCount;
    private int liveCellCount;
    private float simulationSpeed;

    private IPLCell[,] cellArray;

    public int GenerationCount { get => generationCount; }
    public int LiveCellCount { get => liveCellCount; }
    public float CellSize { get => cellSize; }
    public int Width { get => width; }
    public int Height { get => height; }
    public float SimulationSpeed { get => simulationSpeed; set => simulationSpeed = value; }
    public IPLCell[,] CellArray { get => cellArray; }

    
    public IPLGrid()
    {
        width = default_size;
        height = default_size;
        cellSize = default_size;
        simulationSpeed = default_simulationSpeed;
    }

    public IPLGrid(int width, int height)
    {
        this.width = width <= 0 ? default_size : width;
        this.height = height <= 0 ? default_size : height;
        cellSize = default_size;
        simulationSpeed = default_simulationSpeed;
    }
    public IPLGrid(int width, int height, int cellSize)
    {
        this.width = width <= 0 ? default_size : width;
        this.height = height <= 0 ? default_size : height;
        this.cellSize = cellSize <= 0 ? default_size : cellSize;
        simulationSpeed = default_simulationSpeed;
    }
    public IPLGrid(int width, int height, int cellSize, float simulationSpeed)
    {
        this.width = width <= 0 ? default_size : width;
        this.height = height <= 0 ? default_size : height;
        this.cellSize = cellSize <= 0 ? default_size : cellSize;
        this.simulationSpeed = simulationSpeed;
    }

    public void Initialize()
    {
        if (width <= 0) width = default_size;
        if (height <= 0) height = default_size;

        cellArray = new IPLCell[width, height];

        for (int x = 0; x < cellArray.GetLength(0); x++)
        {
            for (int y = 0; y < cellArray.GetLength(1); y++)
            {
                IPLCell cell = new IPLCell(cellSize);

                cell.State = IPLCellState.Dead;
                cell.Position = new Vector2(x, y);

                if(cell.State == IPLCellState.Alive) liveCellCount++;
                cellArray[x, y] = cell;
            }
        }

        generationCount = 0;
        liveCellCount = 0;
    }

    public void SetValue(Vector3 worldPosition)
    {
        Vector2Int cellVector = GetXY(worldPosition);
        SetValue(cellVector.x, cellVector.y);
    }
    public void SetValue(int x, int y)
    {
        if (x < 0 || x >= cellArray.GetLength(0) || y < 0 || y >= cellArray.GetLength(1)) return;

        cellArray[x, y].ToggleState();
        if (cellArray[x, y].State == IPLCellState.Alive) liveCellCount++;
    }

    public Vector2Int GetXY(Vector3 worldPosition)
    {
        return new Vector2Int(Mathf.FloorToInt(worldPosition.x / cellSize), Mathf.FloorToInt(worldPosition.y / cellSize));
    }

    public void CellSelected(int x, int y)
    {
        cellArray[x, y].ToggleState();
    }

    public void Generate()
    {
        IPLCell[,] tempArray = new IPLCell[cellArray.GetLength(0), cellArray.GetLength(1)];

        liveCellCount = 0;

        for (int x = 0; x < cellArray.GetLength(0); x++)
        {
            for (int y = 0; y < cellArray.GetLength(1); y++)
            {
                tempArray[x, y] = cellArray[x, y];
                tempArray[x, y] = new IPLCell(cellSize);
                tempArray[x, y].State = cellArray[x, y].State;
                tempArray[x, y].Position = cellArray[x, y].Position;
            }
        }

        for (int x = 0; x < tempArray.GetLength(0); x++)
        {
            for (int y = 0; y < tempArray.GetLength(1); y++)
            {
                IPLCell cell = cellArray[x, y];
                IPLCell tempCell = tempArray[x, y];

                int neighborCount = 0;

                int minX = x - 1 < 0 ? 0 : x - 1;
                int maxX = x + 1 > width - 1 ? width - 1 : x + 1;

                int minY = y - 1 < 0 ? 0 : y - 1;
                int maxY = y + 1 > height - 1 ? height - 1 : y + 1;


                for (int mx = minX; mx <= maxX; mx++)
                {
                    for (int my = minY; my <= maxY; my++)
                    {
                        if (tempArray[mx, my].State == IPLCellState.Alive)
                        {
                            if (mx != x || my != y)
                            {
                                neighborCount++;
                            }
                        }
                    }
                }

                if (cell.State == IPLCellState.Alive)
                {
                    if (neighborCount < 2) cell.State = IPLCellState.Dead;
                    else if (neighborCount == 2 || neighborCount == 3) cell.State = IPLCellState.Alive;
                    else if (neighborCount > 3) cell.State = IPLCellState.Dead;
                }
                else if (cell.State == IPLCellState.Dead)
                {
                    if (neighborCount == 3) cell.State = IPLCellState.Alive;

                }

                if (cell.State == IPLCellState.Alive) liveCellCount++;
            }
        }

        generationCount++;
    }

    public void Clear()
    {

        for (int x = 0; x < cellArray.GetLength(0); x++)
        {
            for (int y = 0; y < cellArray.GetLength(1); y++)
            {
                IPLCell cell = cellArray[x, y];
                cell.State = IPLCellState.Dead;
            }
        }

        generationCount = 0;
        liveCellCount = 0;
    }

}
