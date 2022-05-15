using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private void Start()
    {
        this.transform.position = RandomVector();
        this.transform.localScale = new Vector3(100, 100, this.transform.localScale.z);
        StartCoroutine(DieOff());
    }

    public IEnumerator DieOff()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    private Vector3 RandomVector()
    {
        float x = Random.Range(-59, 56);
        return new Vector3(x, 28, 0);
    }
}
