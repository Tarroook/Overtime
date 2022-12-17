using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpened;

    public void interact()
    {
        if (isOpened)
            goToNextRoom();
        else
            doorIsClosed();
    }

    void goToNextRoom() 
    {
        
    }

    void doorIsClosed()
    {

    }
}
