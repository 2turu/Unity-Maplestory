using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector2 velocity;
    [SerializeField] private float smoothTimeY = 0;
    [SerializeField] private float smoothTimeX = 0;
    [SerializeField] private float moveCameraY = 0;
    [SerializeField] private float moveCameraX = 0;

    public GameObject player;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate() {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x - moveCameraX, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y - moveCameraY, player.transform.position.y - moveCameraY, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
