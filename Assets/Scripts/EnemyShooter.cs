using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public bool shouldShoot;
    public float fireRate;
    public float shootRange;

    public SpriteRenderer theBody;
    public GameObject bullet;
    public Transform muzzlePoint;

    private FrameTimer _shotTimer;
    
    void Start()
    {
        _shotTimer = new FrameTimer(fireRate, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldShoot || !theBody.isVisible)
        {
            return;
        }

        float playerDistance = Vector3.Distance(
            transform.position,
            PlayerController.instance.transform.position
        );

        if (playerDistance > shootRange)
        {
            _shotTimer.Reset();
            return;
        }

        if (_shotTimer.CheckThisFrame())
        {
            AudioManager.instance.PlaySFX(SoundEffect.Shoot2);
            Instantiate(bullet, muzzlePoint.position, muzzlePoint.rotation);
        }
    }
}
