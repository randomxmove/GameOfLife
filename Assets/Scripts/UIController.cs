using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController _instance;

    private readonly string generationCountStr = "Generation Count : {0}";
    private readonly string generationSpeedStr = "Generation Speed : {0}";
    private readonly string liveCellCountStr = "Live Cell Count : {0}";
    private readonly string startButtonStr = "Start";
    private readonly string stopButtonStr = "Stop";

    [SerializeField] private UIView uiView;

    private UIView UIView { get { return uiView; } }
    private Manager GameController { get { return Manager._instance; } }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        if(uiView != null)
        {
            UIView.actionUpdate = OnClickUpdateGrid;
            UIView.actionStart = OnClickStart;
            UIView.actionNext = OnClickNext;
            UIView.actionClear = OnClickClear;
            UIView.actionInfo = OnClickInfo;
            UIView.actionSpeed = UpdateGenerationSpeed;
        }
    }

    public void UpdateWidthInputText(int width)
    {
        string text = width.ToString();

        UIView.SetWidthInputText(text);
    }
    public void UpdateHeightInputText(int height)
    {
        string text = height.ToString();

        UIView.SetHeightInputText(text);
    }

    public void UpdateCellSizeInputText(float size)
    {
        string text = size.ToString();

        UIView.SetCellSizeInputText(text);
    }

    public void UpdateGenerationSpeed(float speed)
    {
        string text = string.Format(generationSpeedStr, (int)speed);

        GameController.UpdateSimulationSpeed(speed);

        UIView.SetGenerationSpeedText(text);
    }

    public void UpdateGenerationCount(int count)
    {
        string text = string.Format(generationCountStr, count);

        UIView.SetGenerationCountText(text);
    }
    public void UpdateLiveCellCount(int count)
    {
        string text = string.Format(liveCellCountStr, count);
        UIView.SetLiveCellCountText(text);
    }

    private void OnClickUpdateGrid()
    {
        GameController.CreateGrid(UIView.GridWidth, UIView.GridHeight, UIView.CellSize, UIView.Speed);
    }

    private void OnClickStart()
    {
        GameController.StartSimulation();

        UIView.SetStartButtonText(stopButtonStr);
        UIView.DisableSpeedSlider();
        UIView.actionStart = OnClickStop;
    }

    private void OnClickStop()
    {
        GameController.StopSimulation();

        UIView.SetStartButtonText(startButtonStr);
        UIView.EnableSpeedSlider();
        UIView.actionStart = OnClickStart;
    }

    private void OnClickNext()
    {
        OnClickStop();
        GameController.Generate();
    }
    
    private void OnClickClear()
    {
        OnClickStop();
        GameController.Clear();
    }

    private void OnClickInfo()
    {
        if (UIView.IsInfoVisible)
            UIView.HideInfo();
        else
            UIView.ShowInfo();
    }

}
