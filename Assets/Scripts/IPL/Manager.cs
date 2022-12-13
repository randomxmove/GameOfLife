using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Manager : MonoBehaviour
{
    public static Manager _instance;

    [SerializeField] private IPLGridView gridView;
    [SerializeField] GameObject cellPrefab;

    private IPLGrid gridModel;
    private IPLCellView[,] cellViewArray;

    private IPLGridView GridView { get { return gridView; } }
    private UIController UIController { get { return UIController._instance; } }
    private CameraController CameraController { get { return CameraController._instance; } }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        CreateGrid();
    }


    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            gridModel.SetValue(GetMouseWorldPosition());
            Vector2Int vector = gridModel.GetXY(GetMouseWorldPosition());
            UpdateCellView(vector.x, vector.y);

            UIController.UpdateLiveCellCount(gridModel.LiveCellCount);
        }
    }
    public void UpdateSimulationSpeed(float value)
    {
        gridModel.SimulationSpeed = value;
    }

    public void Clear()
    {
        if (gridModel == null) return;

        StopSimulation();
        gridModel.Clear();
        UpdateCellViews();
        UIController.UpdateGenerationCount(gridModel.GenerationCount);
        UIController.UpdateLiveCellCount(gridModel.LiveCellCount);
    }
    
    public void StartSimulation()
    {
        CancelInvoke();
        InvokeRepeating("Generate", 0, 1 / gridModel.SimulationSpeed);
    }

    public void StopSimulation()
    {
        CancelInvoke();
    }
    public void Generate()
    {
        gridModel.Generate();

        UpdateCellViews();

        UIController.UpdateGenerationCount(gridModel.GenerationCount);
        UIController.UpdateLiveCellCount(gridModel.LiveCellCount);
    }

    private void CreateGrid()
    {
        StopSimulation(); 

        gridModel = new IPLGrid();
        GridInitialize();
        InitializeUI();
    }


    public void CreateGrid(int widthValue, int heightValue, int cellSizeValue, float simulationSpeed)
    {
        StopSimulation();

        gridModel = new IPLGrid(widthValue, heightValue, cellSizeValue, simulationSpeed);
        GridInitialize();
    }

    private void GridInitialize()
    {
        gridModel.Initialize();
        GridView.ClearCells();

        CreateCellViews();

        CameraController.UpdateCameraPosition(GetCenterTransform(), gridModel.Width, gridModel.Height, gridModel.CellSize);

        UIController.UpdateGenerationCount(gridModel.GenerationCount);
        UIController.UpdateLiveCellCount(gridModel.LiveCellCount);

        UpdateCellViews();
    }

    private void CreateCellViews()
    {
        float cellSize = gridModel.CellSize;
        int width = gridModel.Width;
        int height = gridModel.Height;

        cellViewArray = new IPLCellView[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f;
                IPLCell cellModel = gridModel.CellArray[x, y];
                IPLCellView cellView = Instantiate(cellPrefab, position, Quaternion.identity, gridView.transform).GetComponent<IPLCellView>();
                cellView.Initialize(cellModel);
                cellView.transform.localScale = Vector2.one * cellSize;
                cellViewArray[x, y] = cellView;
            }
        }
    }

    private void UpdateCellViews()
    {
        for (int x = 0; x < gridModel.Width; x++)
        {
            for (int y = 0; y < gridModel.Height; y++)
            {
                UpdateCellView(x, y);
            }
        }
    }

    private void UpdateCellView(int x, int y)
    {
        cellViewArray[x, y].UpdateCellState();
    }


    private Vector3 GetMouseWorldPosition()
    {
        Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vector.z = 0f;
        return vector;
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * gridModel.CellSize;
    }

    public Transform GetCenterTransform()
    {
        int x = gridModel.CellArray.GetLength(0) / 2;
        int y = gridModel.CellArray.GetLength(1) / 2;

        return cellViewArray[x, y].transform;
    }

    private void InitializeUI()
    {
        UIController.UpdateGenerationSpeed(gridModel.SimulationSpeed);
        UIController.UpdateGenerationCount(gridModel.GenerationCount);
        UIController.UpdateLiveCellCount(gridModel.LiveCellCount);
        UIController.UpdateWidthInputText(gridModel.Width);
        UIController.UpdateHeightInputText(gridModel.Height);
        UIController.UpdateCellSizeInputText(gridModel.CellSize);
    }
}
