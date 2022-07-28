using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScipt : MonoBehaviour
{
   public  bool y;
    public void PrintOutput(bool x)
    {
        if (y)
        {
            if (x) print("true");
            else print("false");
        }
        if (!y) 
        {
            if (x) print("falsey");
            else print("truuthu");
        }
    }
}
