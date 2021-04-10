using UnityEditor;
using UnityEngine;

public class BulletBehavior1 : MonoBehaviour,IPooledObject
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
        BulletTime.instance.startBulletTimeEvent.AddListener(StopBulletTime);
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

        
        if (Input.GetMouseButtonDown (0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics2D.Raycast(ray.origin, ray.direction * 10) && isDragable)
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
            transform.position = pos;
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
