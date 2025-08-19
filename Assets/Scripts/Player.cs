using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    InputAction _moveAction;
    InputAction _attackAction;
    [SerializeField] float speed;
    float releaseCD;

    [SerializeField] GameObject circlePrefab;
    Circle _circle;

    void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _attackAction = InputSystem.actions.FindAction("Attack");
    }

    void Start()
    {
        NextCircle();
    }

    void Update()
    {
        Move();
        Cooldowns();
        if (_attackAction.IsPressed() && releaseCD <= 0) ReleaseCircle();
    }

    void NextCircle()
    {
        GameObject circleObject = Instantiate(circlePrefab, gameObject.transform);
        _circle = circleObject.GetComponent<Circle>();
    }

    void ReleaseCircle()
    {
        releaseCD = 2;
        _circle.transform.parent = null;
        _circle.Release();
        NextCircle();
    }

    void Move()
    {
        if (transform.position.x < -2.5) transform.position = new Vector3(-2.5f, transform.position.y, 0);
        else if (transform.position.x > 2.5) transform.position = new Vector3(2.5f, transform.position.y, 0);

        else transform.position += Vector3.right * _moveAction.ReadValue<Vector2>().x * Time.deltaTime * speed;
    }

    void Cooldowns()
    {
        releaseCD -= Time.deltaTime;
    }
}
