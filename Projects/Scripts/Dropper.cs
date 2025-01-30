using System.Collections;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public GameObject circle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator Drop(){
        // do something
        Debug.Log("Drop");
        float   rX = Random.Range(-6f, 6f);
        Vector3 loc = new Vector3(rX, 6, 0);
        Instantiate(circle, loc, Quaternion.identity);
        // wait
        float next = Random.Range(0.25f, 1.5f);
        yield return new WaitForSeconds(0.5f);
        // go again
        StartCoroutine(routine: Drop());
    }

}
