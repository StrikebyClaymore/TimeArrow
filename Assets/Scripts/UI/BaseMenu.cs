using UnityEngine;

public abstract class BaseMenu : MonoBehaviour
{
    [HideInInspector]
    public RootMenu root;
    
    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }
    
    public virtual  void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
