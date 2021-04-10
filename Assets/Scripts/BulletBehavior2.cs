using UnityEngine;

public class BulletBehavior2 : MonoBehaviour,IPooledObject
{
    private GameManager _gm;
    public float bulletSpeed;
    public float bulletReducSpeed;
    public bool isDragable, isDragging;
    private float _saveBulletSpeed;
    private Vector3 _direction;
    private void Start()
    {
        _saveBulletSpeed = bulletSpeed;

        _gm = GameManager.Instance;
        
        BulletTime.instance.startBulletTimeEvent.AddListener(StartBulletTime);
        BulletTime.instance.stopBulletTimeEvent.AddListener(StopBulletTime);
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
        
        if (Input.GetMouseButtonDown (0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10);
            

            if (hit && isDragable && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
            }
        }
        
        if (Input.GetMouseButtonUp (0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = pos;
        }
    }
    
    public void StartBulletTime()
    {
        bulletSpeed = bulletReducSpeed;
        isDragable = true;
    }
    
    
    public void StopBulletTime()
    {
        bulletSpeed = _saveBulletSpeed;
        isDragable = false;
    }
}
