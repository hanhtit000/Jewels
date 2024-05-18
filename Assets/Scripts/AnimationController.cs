using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AnimationState
{
    Rotate, Shine, Blink
}
public class AnimationController : MonoBehaviour
{
    [SerializeField] private Sprite[] rotateSprite;
    [SerializeField] private Sprite[] shineSprite;
    [SerializeField] private Sprite[] blinkSprite;
    private int count = 0;
    private AnimationState state;
    private float rotateDelay = 0.1f;
    private float shineDelay = 0.1f;
    private float blinkDelay = 0.3f;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        state = AnimationState.Rotate;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        switch (state)
        {
            case AnimationState.Rotate:
                if (count < rotateSprite.Length && time >= rotateDelay)
                {
                    GetComponent<Image>().sprite = rotateSprite[count];
                    count++;
                    time = 0;
                    transform.position += Vector3.up * 10;
                }
                else
                {
                    if(count == rotateSprite.Length)
                    {
                        state = AnimationState.Shine;
                        count = 0;
                    }    
                }
                break;
            case AnimationState.Shine:
                if (count < shineSprite.Length && time >= shineDelay)
                {
                    GetComponent<Image>().sprite = shineSprite[count];
                    count++;
                    time = 0;
                }
                else
                {
                    if (count == shineSprite.Length)
                    {
                        state = AnimationState.Blink;
                        count = 0;
                    }
                }
                break;
            case AnimationState.Blink:
                if (count < blinkSprite.Length && time >= shineDelay)
                {
                    GetComponent<Image>().sprite = blinkSprite[count];
                    count++;
                    time = 0;
                }
                else
                {
                    if (count == blinkSprite.Length)
                    {
                        Destroy(gameObject);
                    }
                }
                break;
        }
    }
}
