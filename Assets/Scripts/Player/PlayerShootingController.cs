using Events;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] private int damagePerShot = 20;
    [SerializeField] private float timeBetweenBullets = 0.15f;
    [SerializeField] private float range = 100f;


    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;


    float effectsDisplayTime = 0.2f;


    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            var eventDataRequest = new GetDisableEffectsEvent();
            EventStream.Game.Publish(eventDataRequest);
        }
    }

    void Shoot()
    {
        timer = 0f;

        var eventDataRequest = new GetShootEvent();
        EventStream.Game.Publish(eventDataRequest);

        var eventDataRequest2 = new GetAppearanceEffectEvent();
        EventStream.Game.Publish(eventDataRequest2);

        var eventDataRequest3 = new GetNewDirectionEvent(0, transform.position);
        EventStream.Game.Publish(eventDataRequest3);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealthController enemyHealthController = shootHit.collider.GetComponent<EnemyHealthController>();
            if (enemyHealthController != null)
            {
                enemyHealthController.TakeDamage(damagePerShot, shootHit.point);
            }

            var eventDataRequest4 = new GetNewDirectionEvent(1, shootHit.point);
            EventStream.Game.Publish(eventDataRequest4);
        }
        else
        {
            var eventDataRequest5 = new GetNewDirectionEvent(1, shootRay.origin + shootRay.direction * range);
            EventStream.Game.Publish(eventDataRequest5);
        }
    }
}