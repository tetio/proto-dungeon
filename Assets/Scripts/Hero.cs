using System;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private float speed = 4.0f;
    private float blockV = 0;
    private float blockH = 0;
    private float nextValue = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = (blockV != 0)? 0 : Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float vertical = (blockH != 0)? 0 : Input.GetAxis("Vertical") * speed * Time.deltaTime;
        if (horizontal != 0 || blockH != 0)
        {
            if (blockH == 0)
            {
                blockH = Math.Sign(horizontal) * 0.06f;
                float truncated = (float)(Math.Truncate((double)transform.position.x * 100.0) / 100.0);
                nextValue = (horizontal > 0) ? truncated + 1 : truncated - 1;
                transform.Translate(new Vector3(blockH, 0, 0));
            }
            else
            {
                if ((blockH > 0 && transform.position.x > nextValue) ||
                    (blockH < 0 && transform.position.x < nextValue))
                {
                    transform.position = new Vector3(nextValue, transform.position.y, 0);
                    blockH = 0;
                    nextValue = 0;
                }
                else
                {
                    transform.Translate(new Vector3(blockH, 0, 0));
                }
            }

        }
        else if (vertical != 0 || blockV != 0)
        {
            if (blockV == 0)
            {
                blockV = Math.Sign(vertical) * 0.06f;
                float truncated = (float)(Math.Truncate((double)transform.position.y * 100.0) / 100.0);
                nextValue = (vertical > 0) ? truncated + 1 : truncated - 1;
                transform.Translate(new Vector3(0, blockV, 0));
            }
            else
            {
                if ((blockV > 0 && transform.position.y > nextValue) ||
                    (blockV < 0 && transform.position.y < nextValue))
                {
                    transform.position = new Vector3(transform.position.x, nextValue, 0);
                    blockV = 0;
                    nextValue = 0;
                }
                else
                {
                    transform.Translate(new Vector3(0, blockV, 0));
                }
            }
        }
    }
}
