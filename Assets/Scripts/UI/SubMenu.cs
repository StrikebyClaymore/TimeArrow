using UnityEngine;

public class SubMenu : MonoBehaviour
{
    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }
    
    public virtual  void Deactivate()
    {
        gameObject.SetActive(false);
    }
}