using UnityEngine;

public class BulletBehavior1 : MonoBehaviour,IPooledObject
{
    private GameManager _gm;
    public float bulletSpeed;
    private Vector3 _direction;
    private void Start()
    {
        _gm = GameManager.Instance;
    }

    public void OnObjectSpawn(Vector3 direction)
    {
        _direction = direction;
    }

    private void Update()
    {
        transform.Translate(_direction * Time.deltaTime* bulletSpeed);
        if (transform.position.x > _gm.screenBounds.x || transform.position.x < -_gm.screenBounds.x || transform.position.y > _gm.screenBounds.y || transform.position.y < -_gm.screenBounds.y)
        {
            gameObject.SetActive(false);
        }
    }
}
