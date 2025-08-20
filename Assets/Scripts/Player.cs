using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    AudioSource _aud;

    InputAction _moveAction;
    InputAction _sendAction;
    [SerializeField] float speed;
    float releaseCD;
    bool circleReady;
    public static int circleCount;

    [SerializeField] GameObject circlePrefab;
    Circle _circle;

    void Awake()
    {
        _aud = GetComponent<AudioSource>();
        _moveAction = InputSystem.actions.FindAction("Move");
        _sendAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        Move();
        Cooldowns();

        if (releaseCD == 0)
        {
            if (!circleReady) NextCircle();
            if (_sendAction.IsPressed() && circleReady) ReleaseCircle();
        }
    }

    void NextCircle()
    {
        transform.position = new Vector3(0, transform.position.y, 0);
        circleReady = true;
        circleCount++;
        GameObject circleObject = Instantiate(circlePrefab, gameObject.transform);
        _circle = circleObject.GetComponent<Circle>();
    }

    void ReleaseCircle()
    {
        circleReady = false;
        _circle.Release();
        releaseCD = 2;
        _aud.Play();
    }

    void Move()
    {
        if (transform.position.x < -2.1) transform.position = new Vector3(-2.1f, transform.position.y, 0);
        else if (transform.position.x > 2.1) transform.position = new Vector3(2.1f, transform.position.y, 0);

        else transform.position += Vector3.right * _moveAction.ReadValue<Vector2>().x * Time.deltaTime * speed;
    }

    void Cooldowns()
    {
        if (releaseCD > 0) releaseCD -= Time.deltaTime;
        else if (releaseCD < 0) releaseCD = 0;
    }
}
