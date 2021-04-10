using UnityEngine;

public class BulletBehavior2 : MonoBehaviour,IPooledObject
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        gameObject.transform.LookAt(player.transform.position);
    }

    private void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime* bulletSpeed);
        if (transform.position.x > _gm.screenBounds.x || transform.position.x < -_gm.screenBounds.x || transform.position.y > _gm.screenBounds.y || transform.position.y < -_gm.screenBounds.y)
        {
            gameObject.SetActive(false);
        }
    }
}
