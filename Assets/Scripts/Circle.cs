using UnityEngine;

public class Circle : MonoBehaviour
{
    Rigidbody2D _rb;
    CircleCollider2D _col;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CircleCollider2D>();
        _col.enabled = false;
    }

    void Start()
    {
        _rb.gravityScale = 0;
    }
	
	public void Release()
	{
        _col.enabled = true;
        _rb.gravityScale = 1;
    }
}
