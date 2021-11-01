using System;
using UnityEngine;
using System.Linq;
public class Hero : MonoBehaviour
{
    private float speed = 0.08f;
    private float blockV = 0;
    private float blockH = 0;
    private float nextValue = 0;

    public GameObject _gameManager;
    GameManager gameManager;


    // Start is called before the first frame update
    void Awake()
    {
        gameManager = _gameManager.GetComponent<GameManager>();//GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        string value = "NOT_WALL";
        if (horizontal > 0 || blockH > 0) {
            var pos = new Vector2(transform.position.x+1, transform.position.y);
            if (gameManager.elements.TryGetValue(pos, out value) && gameManager.elements[pos] == "WALL")
                return; 
        } else if (horizontal < 0 || blockH < 0) {
            var pos = new Vector2(transform.position.x-1, transform.position.y);
            if (gameManager.elements.TryGetValue(pos, out value) && gameManager.elements[pos] == "WALL")
                return; 
        } else if (vertical > 0 || blockV > 0) {
            var pos = new Vector2(transform.position.x, transform.position.y+1);
            if (gameManager.elements.TryGetValue(pos, out value)  && gameManager.elements[pos] == "WALL")
                return; 
        } else if (vertical < 0 || blockV < 0) {
            var pos = new Vector2(transform.position.x, transform.position.y-1);
            if (gameManager.elements.TryGetValue(pos, out value) && gameManager.elements[pos] == "WALL")
                return; 
        }   


        if (blockV == 0 && (horizontal != 0 || blockH != 0))
        { 
            if (blockH == 0)
            {
                blockH = Math.Sign(horizontal) * speed;
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
                    // Input.ResetInputAxes();
                }
                else
                {
                    transform.Translate(new Vector3(blockH, 0, 0));
                }
            }

        } 
        if (blockH == 0 && (vertical != 0 || blockV != 0))
        {
            if (blockV == 0)
            {
                blockV = Math.Sign(vertical) * speed;
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
                    // Input.ResetInputAxes();
                }
                else
                {
                    transform.Translate(new Vector3(0, blockV, 0));
                }
            }
        }
    }


    void OnTriggerEnter(Collider collider)
    {
        // foreach (ContactPoint contact in collision.contacts)
        // {
        //     Debug.DrawRay(contact.point, contact.normal, Color.white);
        // }

        // if (collision.relativeVelocity.magnitude > 2)
        //     audioSource.Play();  
        Debug.Log("HERO: poing!");
    }
}
