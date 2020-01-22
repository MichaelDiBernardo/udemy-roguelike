using UnityEngine;

public class EnemyShooter : Sleeper
{
    public bool shouldShoot;
    public float fireRate;
    public float shootRange;
    
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
        if (!shouldShoot || IsAsleep)
        {
            return;
        }

        float playerDistance = Vector3.Distance(
            transform.position,
            PlayerController.instance.transform.position
        );

        if (playerDistance > shootRange)
        {
            return;
        }

        if (_shotTimer.CheckThisFrame())
        {
            AudioManager.instance.PlaySFX(SoundEffect.Shoot2);
            Instantiate(bullet, muzzlePoint.position, muzzlePoint.rotation);
        }
    }
}
