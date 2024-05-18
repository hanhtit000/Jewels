using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class JewelsController : MonoBehaviour
{
    [SerializeField] private GameObject jewelPrefab;
    [SerializeField] private Sprite[] jewelSprites;
    [SerializeField] private int width;
    [SerializeField] private int height;
    private List<GameObject> jewels;

    private static JewelsController _instance;
    public static JewelsController Instance
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
        jewels = new List<GameObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(jewels.Count == 0)
        {
            for (int i = 1; i <= 12; i++)
            {
                var jewel = Instantiate(jewelPrefab, this.transform);
                jewel.GetComponent<Image>().sprite = jewelSprites[i % 6];
                System.Random random = new();
                float x = random.Next(-width, width);
                float y = random.Next(-height, height);
                jewel.transform.position = new float3(this.transform.position.x + x, this.transform.position.y + y, 0);
                jewel.name = "Jewel " + i;
                jewels.Add(jewel);
            }
        }
    }

    public List<GameObject> GetJewelList()
    {
        return jewels;
    }
    public void SetJewelList(List<GameObject> gameObjects)
    {
        jewels = gameObjects;
    }
}
