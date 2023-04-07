using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class ShootOnClick : MonoBehaviour
{
    [SerializeField]
    private Pooler_v2 m_pooler;
    [SerializeField] private Camera m_camera;
    [SerializeField] private Transform m_firepointParent;
    [SerializeField] private GameObject m_firepoint;
    [SerializeField] private float m_delayBullet = 1f;

    private bool m_waitEndDelay;
    private int m_salveBulletShooted = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var l_dir = Input.mousePosition - m_camera.WorldToScreenPoint(m_firepointParent.transform.position);
        var l_angle = Mathf.Atan2(l_dir.y, l_dir.x) * Mathf.Rad2Deg;
        m_firepointParent.transform.rotation = Quaternion.AngleAxis(l_angle + 90, Vector3.forward);
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (m_waitEndDelay) { return; }
            
            CallBullet();
        }
    }

    void CallBullet()
    {
        m_waitEndDelay = true;

        m_salveBulletShooted++;

        if (m_salveBulletShooted >= 10)
        {
            GameObject l_obj = m_pooler.ActiveObj(m_firepoint);
            l_obj.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

            m_salveBulletShooted = 0;
        }
        else
        {
            m_pooler.ActiveObj(m_firepoint);
        }
        

        

        StartCoroutine(DelayBullet());
    }
    IEnumerator DelayBullet()
    {
        yield return new WaitForSeconds(m_delayBullet);
        m_waitEndDelay = false;
    }
}
