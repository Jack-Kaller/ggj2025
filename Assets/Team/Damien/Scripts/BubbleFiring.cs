using UnityEngine;
using UnityEngine.Assertions;

public class BubbleFiring : MonoBehaviour
{

    [Header("Bubble stats")]
    [SerializeField] private float bubbleDeliverySpeed = 10.0f;
    
    [SerializeField] GameObject bubbleSpawner;
    [SerializeField] GameObject bubblePrefab;
    [SerializeField] float waitTimeBeforeShoot = 1;
    public int actionTriggered;

    private bool _isReloading = false;
    private float _timer;

    public float m_checkWallMaxDistance = 0.1f;
    public LayerMask m_wallAndOtherLayer;

    void Start()
    {
        Assert.IsNotNull(bubbleSpawner);
        Assert.IsNotNull(bubblePrefab);

        _timer = waitTimeBeforeShoot;
    }

    public void OnAction(int action)
    {
        actionTriggered = action;
        if (actionTriggered == 1300)
        {
            var t = bubbleSpawner.transform;
            Debug.DrawLine(t.position, t.position+ t.forward*m_checkWallMaxDistance, Color.yellow,10);
            RaycastHit hit;
            if (!Physics.Raycast(t.position, t.forward, out hit, m_checkWallMaxDistance, m_wallAndOtherLayer))
            {
                Debug.DrawLine(t.position, t.position + t.forward * m_checkWallMaxDistance*1.1f, Color.red, 10);
                if (!isBubbleOnReload())
                {
                    GameObject bubble = Instantiate(bubblePrefab, bubbleSpawner.transform.position, Quaternion.identity);
                    bubble.GetComponent<Rigidbody>().AddForce(bubbleSpawner.transform.forward * bubbleDeliverySpeed);
                    _timer = waitTimeBeforeShoot;
                    _isReloading = true;
                }
            }
        }
    }

    void Update()
    {
        if (_isReloading && _timer > 0)
        {
            _timer -= Time.deltaTime;
        }
    }

    bool isBubbleOnReload() 
    {
        if (_timer > 0 && _isReloading)
        {
            _isReloading = true;
        }
        else
        {
            _isReloading = false;
        }
        
        return _isReloading;
    }
}
