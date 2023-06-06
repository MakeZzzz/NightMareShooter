using Events;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    [SerializeField] Rigidbody _playerRigidbody;
    
    private Vector3 movement;
    private int _floorMask;
    private float camRayLength = 100f;


    void Awake()
    {
        _floorMask = LayerMask.GetMask("Floor");
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        _playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, _floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0f;

            Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);
            _playerRigidbody.MoveRotation(newRotatation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        var eventDataRequest = new GetPlayerWalkEvent(walking);
        EventStream.Game.Publish(eventDataRequest);
    }
}