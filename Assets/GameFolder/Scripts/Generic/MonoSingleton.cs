using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    static volatile T instance = null; // volatile -> De?i?kenin de?erinin direkt bellekten al?nmas?n? sa?lar.

    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    protected void Singleton(bool dontDestroyOnLoad = false)
    {
        if (dontDestroyOnLoad)
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (instance == null)
            {
                instance = this as T;
            }
        }
    }
}