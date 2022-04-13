using UnityEngine;

public class AlarmPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void Play()
    {
        audioSource.Play();
        //StartCoroutine(WaitStop(audioSource.clip.length));
    }

    public void Stop()
    {
        audioSource.Stop();
    }
    
    /*private IEnumerator WaitStop(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
 
        Debug.Log("STOP");
 
    }*/
}
