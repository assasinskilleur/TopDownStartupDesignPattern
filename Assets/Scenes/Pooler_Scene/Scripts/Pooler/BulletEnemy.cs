using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField]
    private float m_speed, m_lifeTime;
    
    [SerializeField] private float m_damage;

    private Pooler_v2 m_pooler;
    private void Reset()
    {
        m_speed = 10f;
        m_lifeTime = 3f;
    }
    
    private void OnEnable()
    {
        DestroyBullet();
    }

    private void OnDisable()
    {
        m_pooler = transform.parent.GetComponent<Pooler_v2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * m_speed * Time.deltaTime);
    }

    private void DestroyBullet()
    {
        StartCoroutine(LifeTime());

        IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(m_lifeTime);
            m_pooler.DeactiveObj(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IHealth l_health))
        {
            if (l_health.IsPlayer())
                l_health.TakeDamage(m_damage);
        }
        
        if(collision.gameObject.layer != 3 && collision.gameObject.layer != 8)
        {
            m_pooler.DeactiveObj(gameObject);
        }
    }
}
