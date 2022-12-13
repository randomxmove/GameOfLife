using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UIView : MonoBehaviour
{

    [SerializeField] private Button updateGrid;
    [SerializeField] private Button startButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button infoButton;
    [SerializeField] private Button clearButton;

    [SerializeField] private GameObject infoPopup;

    [SerializeField] private TMP_InputField widthInput;
    [SerializeField] private TMP_InputField heightInput;
    [SerializeField] private TMP_InputField cellSizeInput;

    [SerializeField] private TMP_Text startButtonText;
    [SerializeField] private TMP_Text generationCountText;
    [SerializeField] private TMP_Text generationSpeedText;
    [SerializeField] private TMP_Text liveCellCountText;

    [SerializeField] private Slider speedSlider;

    public UnityAction actionUpdate;
    public UnityAction actionStart;
    public UnityAction actionNext;
    public UnityAction actionClear;
    public UnityAction actionInfo;
    public UnityAction<float> actionSpeed;

    public bool IsInfoVisible { get { return infoPopup.activeSelf; } }
    public bool IsSpeedSliderEnabled {  get { return speedSlider.enabled; } }
    public int GridWidth { get { return int.Parse(widthInput.text); } }
    public int GridHeight { get { return int.Parse(widthInput.text); } }
    public int CellSize { get { return int.Parse(widthInput.text); } }
    public float Speed { get { return speedSlider.value; } }

    private void Start()
    {
        widthInput.characterValidation = TMP_InputField.CharacterValidation.Integer;
        heightInput.characterValidation = TMP_InputField.CharacterValidation.Integer;
        cellSizeInput.characterValidation = TMP_InputField.CharacterValidation.Integer;

        updateGrid.onClick.AddListener(OnClickUpdate);
        startButton.onClick.AddListener(OnClickStart);
        nextButton.onClick.AddListener(OnClickNext);
        clearButton.onClick.AddListener(OnClickClear);
        infoButton.onClick.AddListener(OnClickInfo);
        speedSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnClickUpdate()
    {
        if (actionUpdate == null) return;
        actionUpdate.Invoke();
    }

    private void OnClickStart()
    {
        if (actionStart == null) return;
        actionStart.Invoke();
    }

    private void OnClickNext()
    {
        if (actionNext == null) return;
        actionNext.Invoke();
    }

    private void OnClickClear()
    {
        if (actionClear == null) return;
        actionClear.Invoke();
    }

    private void OnClickInfo()
    {
        if (actionInfo == null) return;
        actionInfo.Invoke();
    }

    private void OnSliderValueChanged(float value)
    {
        if (actionSpeed == null) return;
        actionSpeed.Invoke(value);
    }

    public void SetWidthInputText(string value) => widthInput.text = value;
    public void SetHeightInputText(string value) => heightInput.text = value;
    public void SetCellSizeInputText(string value) => cellSizeInput.text = value;
    public void SetGenerationSpeedText(string value) => generationSpeedText.text = value;
    public void SetGenerationCountText(string value) => generationCountText.text = value;
    public void SetLiveCellCountText(string value) => liveCellCountText.text = value;
    public void SetStartButtonText(string value) => startButtonText.text = value;

    public void ShowInfo() => infoPopup.SetActive(true);
    public void HideInfo() => infoPopup.SetActive(false);

    public void EnableSpeedSlider() => speedSlider.enabled = true;
    public void DisableSpeedSlider() => speedSlider.enabled = false;
}
