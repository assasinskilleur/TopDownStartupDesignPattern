using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    [SerializeField]
    private Pooler_v2 m_pooler;
    [SerializeField] private Transform m_head;
    [SerializeField] private GameObject m_firepoint;
    private GameObject m_player;

    [SerializeField] private float m_delayBullet;
    private bool m_isOut = false;
    private bool m_waitEndDelay = false;

    [SerializeField] private OrganicDifficultyReference m_diff;

    // Start is called before the first frame update
    void Start()
    {
        m_player = null;
        Debug.Log(m_head.localRotation);
    }

    // Update is called once per frame
    void Update()
    {

        if (m_player != null)
        {
            if (m_isOut) { return; }

            Vector3 l_look = m_head.InverseTransformPoint(m_player.transform.position);
            float l_angle = Mathf.Atan2(l_look.y, l_look.x) * Mathf.Rad2Deg - 90;

            m_head.Rotate(0, 0, l_angle);

            CallBullet();
            Debug.Log(m_head.localRotation);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IHealth l_health))
        {
            if(m_player == null)
            {
                m_player = collision.gameObject;
            }

            m_isOut = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IHealth l_health))
        {
            m_head.localRotation = Quaternion.Euler(0, 0, 0);
            m_isOut = true;
        }
    }

    void CallBullet()
    {
        if (m_waitEndDelay) { return; }

        m_waitEndDelay = true;

        GameObject l_obj = m_pooler.ActiveObj(m_firepoint);
        l_obj.transform.parent = null;

        
        StartCoroutine(DelayBullet());
    }
    IEnumerator DelayBullet()
    {
        yield return new WaitForSeconds(m_delayBullet / m_diff.Acquire()._diff);
        m_waitEndDelay = false;
    }
}
