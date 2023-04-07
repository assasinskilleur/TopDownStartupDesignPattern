using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler_v2 : MonoBehaviour
{
    [SerializeField] private bool m_isActived;
    [SerializeField] private int m_maxInstanciation;
    [SerializeField] private GameObject m_prefab;
    
    private List<GameObject> m_list;

    private int m_currentObjActived = 0;
    private void Reset()
    {
        m_isActived = false;
        m_maxInstanciation = 10;
        m_currentObjActived = 0;
    }
    // Start is called before the first frame update

    private void Awake()
    {
        m_list = new List<GameObject>();
    }
    void Start()
    {
        if (m_isActived)
        {
            GenerateObj(m_prefab);
        }
    }

    private void GenerateObj(GameObject obj)
    {
        for (int i = 0; i < m_maxInstanciation; i++)
        {
            GameObject l_obj = Instantiate(obj);
            l_obj.transform.parent = transform;
            m_list.Add(l_obj);
            m_list[i].SetActive(false);
        }
    }

    public GameObject ActiveObj(GameObject l_parentObj)
    {
        GameObject l_obj = null;

        for(int i = 0; i < m_list.Count; i++)
        {
            l_obj = m_list[i];

            if (m_list[i].activeInHierarchy == false)
            {
                l_obj.transform.localScale = m_prefab.transform.localScale;
                l_obj.transform.parent = null;
                l_obj.transform.parent = l_parentObj.transform;

                l_obj.transform.position = l_parentObj.transform.position;
                l_obj.transform.rotation = l_parentObj.transform.rotation;

                l_obj.SetActive(true);

                m_currentObjActived++;
                break;
            }
        }
        return l_obj;
    }

    public void DeactiveObj(GameObject obj)
    {
        if (obj.activeInHierarchy == true)
        {
            obj.transform.localScale =  m_prefab.transform.localScale;

            obj.transform.parent = null;
            obj.transform.parent = transform;

            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            

            obj.SetActive(false);
        }
    }
}
