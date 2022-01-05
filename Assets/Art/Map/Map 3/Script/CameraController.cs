using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Rigidbody2D rigidbody;

    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;

        float horizon = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        pos.x += 1f * horizon;
        pos.y += 1f * vertical;
        rigidbody.MovePosition(pos);
        if(Input.GetKey(KeyCode.Escape))
        {
            GameManager._instance.pause();
            pauseMenu.SetActive(true);
        }
    }
}
