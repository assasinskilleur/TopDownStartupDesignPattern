using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRewindable
{ 
    void AddRewindAction(object param = null);
    void OnStartRewind();
    void OnStopRewind();

}
