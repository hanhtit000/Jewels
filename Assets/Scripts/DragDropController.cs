using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private GameObject coinPrefab;
    private BoardController boardController;
    private JewelsController jewelsController;
    private Vector3 originPos;
    private bool inCell;
    private Vector2 cellSize;
    public bool InCell { get { return inCell; } }
    // Start is called before the first frame update
    void Start()
    {
        boardController = BoardController.Instance;
        jewelsController = JewelsController.Instance;
        inCell = false;
        cellSize = boardController.GetCellSize();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originPos = transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var mousePos = eventData.pointerDrag.transform.position;
        var boardPos = boardController.GetComponent<RectTransform>();
        var halfWidth = boardPos.sizeDelta.x / 2;
        var halfHeight = boardPos.sizeDelta.y / 2;

        if (mousePos.x < (boardPos.position.x - halfWidth) || mousePos.x > (boardPos.position.x + halfWidth))
        {
            transform.position = originPos;
        }
        else
        {
            if (mousePos.y < (boardPos.position.y - halfHeight) || mousePos.y > (boardPos.position.y + halfHeight))
            {
                transform.position = originPos;
            }
            else
            {
                Snap();
                CheckCell();
            }
        }


    }

    public void OnDrag(PointerEventData eventData)
    {
        inCell = false;
        transform.position = eventData.position;
    }
    public void Snap()
    {
        var list = boardController.GetCellList();
        var minDistance = float.MaxValue;
        var anchoredCell = list[0];
        foreach (var cell in list)
        {
            var distance = math.distance(transform.position, cell.transform.position);
            if (minDistance > distance)
            {
                minDistance = distance;
                anchoredCell = cell;
            }
        }
        transform.position = anchoredCell.transform.position;
        inCell = true;

    }
    public void CheckCell()
    {
        var smash = false;
        var target = gameObject;
        var list = jewelsController.GetJewelList();
        foreach (var jewel in list)
        {
            var incell = jewel.GetComponent<DragDrop>().InCell;
            if (incell && jewel.name != this.name && GetComponent<Image>().sprite == jewel.GetComponent<Image>().sprite)
            {
                var distance = math.distance(transform.position, jewel.transform.position);
                Vector3 directionCtoA = jewel.transform.position;
                Vector3 directionCtoB = transform.position;
                Vector3 midpointAtoB = new Vector3((directionCtoA.x + directionCtoB.x) / 2.0f, (directionCtoA.y + directionCtoB.y) / 2.0f, (directionCtoA.z + directionCtoB.z) / 2.0f);
                if (distance == cellSize.x)
                {
                    Debug.Log(distance + " " + midpointAtoB);
                    Instantiate(coinPrefab, FindAnyObjectByType<Canvas>().transform).transform.position = midpointAtoB;
                    smash = true;
                    target = jewel;
                }
            }
        }
        if (smash)
        {
            list.Remove(target);
            list.Remove(gameObject);
            jewelsController.SetJewelList(list);
            Destroy(target);
            Destroy(gameObject);
        }
    }
}
