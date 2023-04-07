using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootOnClick : MonoBehaviour, IAttack
{
    [SerializeField]
    private Pooler_v2 m_pooler;
    [SerializeField] private Camera m_camera;
    [SerializeField] private Transform m_firepointParent;
    [SerializeField] private GameObject m_firepoint;
    
    [SerializeField] private PlayerReference m_playerReference;

    private bool m_waitEndDelay;
    private int m_salveBulletShooted = 0;

    // Update is called once per frame
    void Update()
    {
        var l_dir = Input.mousePosition - m_camera.WorldToScreenPoint(m_firepointParent.transform.position);

        var l_angle = Mathf.Atan2(l_dir.y, l_dir.x) * Mathf.Rad2Deg;
        m_firepointParent.transform.rotation = Quaternion.AngleAxis(l_angle + 90, Vector3.forward);
    }
    
    public void MakeAttack()
    {
        if (!m_waitEndDelay)
        {
            CallBullet();
        }
    }

    void CallBullet()
    {
        m_waitEndDelay = true;

        m_salveBulletShooted++;

        GameObject l_obj = m_pooler.ActiveObj(m_firepoint);
        l_obj.transform.parent = null;
        
        StartCoroutine(DelayBullet());
    }
    IEnumerator DelayBullet()
    {
        yield return new WaitForSeconds(m_playerReference.Acquire().Stats.AttackSpeed);
        m_waitEndDelay = false;
    }
}
