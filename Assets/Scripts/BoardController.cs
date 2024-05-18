using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Sprite[] cellBackgrounds;
    private int height = 6;
    private int width = 6;
    private List<GameObject> cells = new List<GameObject>();

    private static BoardController _instance;
    public static BoardController Instance
    {
        get
        {
            return _instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
            {
                var cell = Instantiate(cellPrefab, this.transform);
                cell.GetComponent<Image>().sprite = cellBackgrounds[(i + j) % 2];
                cells.Add(cell);
            }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<GameObject> GetCellList()
    {
        return cells;
    }
    public Vector2 GetCellSize()
    {
        return GetComponent<GridLayoutGroup>().cellSize;
    }
}
