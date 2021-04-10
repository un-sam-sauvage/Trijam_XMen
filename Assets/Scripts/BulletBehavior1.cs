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
        BulletTime.instance.stopBulletTimeEvent.AddListener(StopBulletTime);
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
            DestroyImmediate(gameObject, true);
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
        Debug.Log("Start bullet time event");
        
        
        bulletSpeed = bulletReducSpeed;
        isDragable = true;
    }
    
    
    public void StopBulletTime()
    {
        Debug.Log("Stop bullet time event");
        
        
        bulletSpeed = _saveBulletSpeed;
        isDragable = false;
    }
}
