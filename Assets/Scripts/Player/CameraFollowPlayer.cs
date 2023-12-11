using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraPos = new Vector3(0, 20, -20);
    
    void Update()
    {
        transform.position = player.transform.position + cameraPos;
    }
}
