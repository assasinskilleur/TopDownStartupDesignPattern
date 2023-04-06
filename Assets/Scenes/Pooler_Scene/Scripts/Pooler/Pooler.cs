using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField]
    private GameObject m_prefab;

    [SerializeField]
    private int m_poolSize;

    [SerializeField]
    private bool m_isUsed;

    private List<GameObject> m_freeList;
    private List<GameObject> m_usedList;

    private void Reset()
    {
        m_poolSize = 10;
    }

    private void Awake()
    {
        m_freeList = new List<GameObject>();
        m_usedList = new List<GameObject>();

        for(int i = 0; i < m_poolSize; i++)
        {
            CreateObject();
        }
    }

    private void CreateObject()
    {
        GameObject l_gameObject = Instantiate(m_prefab);
        l_gameObject.transform.parent = transform;
        l_gameObject.SetActive(false);
        m_freeList.Add(l_gameObject);
    }

    public GameObject GetReference()
    {
        int l_sizeFree = m_freeList.Count;

        if (l_sizeFree == 0 && !m_isUsed)
        {
            return null;
        }
        else if(l_sizeFree == 1)
        {
            CreateObject();
        }

        Debug.Log(l_sizeFree);

        GameObject l_gameObject = m_freeList[l_sizeFree - 1];
        m_freeList.RemoveAt(l_sizeFree -1);
        m_usedList.Add(l_gameObject);
        return l_gameObject;
    }

    public void ReturnReference(GameObject obj)
    {
        Debug.Assert(m_usedList.Contains(obj));
        obj.SetActive(false);
        m_usedList.Remove(obj);
        m_freeList.Add(obj);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
