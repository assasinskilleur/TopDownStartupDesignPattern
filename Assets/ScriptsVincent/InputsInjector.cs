using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsInjector : MonoBehaviour
{
    // Start is called before the first frame update

    private InputActions m_inputs;
    [SerializeField] private PlayerMovements m_playerMovements;
    [SerializeField] private RewindManagerReference m_rewindManager;
    
    
    void Start()
    {
        m_inputs = new InputActions();
        m_inputs.Enable();
        m_playerMovements.RegisterInputs(m_inputs);
        m_rewindManager.Acquire().RegisterInputs(m_inputs);
    }

}
