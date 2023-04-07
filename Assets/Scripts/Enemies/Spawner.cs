using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject m_enemyPrefab;
    
    [SerializeField] private float m_spawnRate;
    
    private float m_timer;
    
    private void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer >= m_spawnRate)
        {
            m_timer = 0;
            Instantiate(m_enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}
