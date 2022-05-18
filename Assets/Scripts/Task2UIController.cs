using UnityEngine;
using TMPro;

public class Task2UIController : MonoBehaviour
{
    [SerializeField] private GameObject findPathButton;
    [SerializeField] private GameObject mazeControlls;
    [SerializeField] private MazeManager mazeManager;

    [SerializeField] private int minSize, maxSize;

    [SerializeField] private TextMeshProUGUI inputX;
    [SerializeField] private TextMeshProUGUI inputY;

    public void EnableFindPathButton() 
    {
        findPathButton.SetActive(true);
    }

    public void OnClickAddStart() 
    {
        mazeManager.placement = MazeManager.TileTypePlacement.Start;
    }

    public void OnClickAddEnd()
    {
        mazeManager.placement = MazeManager.TileTypePlacement.End;
    }

    public void OnClickFindPath() 
    {
        mazeManager.CalculatePath();
    }

    public void OnValueChangedSizeX() 
    {
        if (int.TryParse(inputX.text, out int value))
        {
            if (value < minSize || value > maxSize)
            {
                inputX.text = "500";
                value = 500;
            }
            mazeManager.XSize = value;
        }
    }

    public void OnValueChangedSizeY()
    {
        if (int.TryParse(inputY.text, out int value))
        {
            if (value < minSize || value > maxSize)
            {
                inputY.text = "500";
                value = 500;
            }
            mazeManager.YSize = value;
        }
    }

    public void OnClickBlankMaze() 
    {
        Debug.Log("Create maze...");
        mazeControlls.SetActive(false);
        mazeManager.GenerateMaze();
    }
}
