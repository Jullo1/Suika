using UnityEngine;
using System.Collections.Generic;

public class Circle : MonoBehaviour
{
    Rigidbody2D _rb;
    SpriteRenderer _sr;
    CircleCollider2D _col;
    AudioSource _aud;

    public int size;
    [SerializeField] List<Sprite> spriteList;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _aud = GetComponent<AudioSource>();
        _col = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        RandomizeSize();
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
        FindFirstObjectByType<UI>().UpdateScore(size * 50);
        if (size > 7)
        {
            GameObject.FindGameObjectWithTag("ExtraAudio").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
        else
        {
            _aud.Play();
            transform.localScale += Vector3.one / 3;
            UpdateSprite();
        }
    }

    void RandomizeSize()
    {
        int spawnSize = Player.circleCount / 10;
        if (spawnSize > 4) spawnSize = 4;
        size = Random.Range(0, spawnSize + 1);
        transform.localScale += (Vector3.one / 3) * size;
        UpdateSprite();
    }

    void UpdateSprite()
    {
        if (size > 7) return;
        _sr.sprite = spriteList[size];
        _sr.sortingOrder = 0 - size;
    }
}
