using UnityEngine;

public class IPLCellView : MonoBehaviour
{
    private IPLCell cellModel;

    public void Initialize(IPLCell cellModel)
    {
        this.cellModel = cellModel;
    }
    private void Start()
    {
    }

    public void UpdateCellState()
    {
        if (cellModel.State == IPLCellState.Alive)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (cellModel.State == IPLCellState.Dead)
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

}
