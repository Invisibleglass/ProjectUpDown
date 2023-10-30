using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] bool scrollLeft;

    [SerializeField]private float offset = 5f;
    private float singleTextureWidth;

    // Start is called before the first frame update
    private void Start()
    {
        SetupTexture();
        if (scrollLeft)
        {
            moveSpeed = -moveSpeed;
        }
    }

    private void SetupTexture()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        singleTextureWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }

    private void Scroll()
    {
        float delta = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(delta, 0f, 0f);
    }

    private void CheckReset()
    {
        if ((Mathf.Abs(transform.position.x - offset)- singleTextureWidth) > 0)
        {
            transform.position = new Vector3(offset, transform.position.y, transform.position.z);
        }
    }

    private void Update()
    {
        Scroll();
        CheckReset();
    }
}
