using UnityEngine;

public class Circle : MonoBehaviour
{
    Rigidbody2D _rb;
    CircleCollider2D _col;

    public int size;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CircleCollider2D>();
        _col.enabled = false;
    }

    void Start()
    {
        RandomizeSize();
        _rb.gravityScale = 0;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Circle circle2 = collision.collider.GetComponent<Circle>();
        if (circle2 != null) if (circle2.size == size) Merge(circle2);
    }

    public void Release()
    {
        transform.parent = null;
        _col.enabled = true;
        _rb.gravityScale = 1;
    }

    void Merge(Circle circle2)
    {
        Destroy(circle2.gameObject);
        size++;
        transform.localScale += Vector3.one/3;
        FindFirstObjectByType<UI>().UpdateScore(size * 50);
    }

    void RandomizeSize()
    {
        int spawnSize = Player.circleCount / 10;
        if (spawnSize > 4) spawnSize = 4;
        size = Random.Range(0, spawnSize + 1);
        transform.localScale += (Vector3.one / 3) * size;
    }
}
