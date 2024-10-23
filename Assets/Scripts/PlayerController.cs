
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2;
    float horizontal;
    float vertical;
    public float speed;
    private void Awake()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        rigidbody2.velocity=new Vector2(horizontal*speed, vertical*speed);
    }
}
