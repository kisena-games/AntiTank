using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _flySpeed = 50f;

    private float _radius = 0.2f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private void Start()
    {
        StartCoroutine(ListenColliders());
    }

    private void Update()
    {
        transform.localPosition += transform.forward * _flySpeed * Time.deltaTime;           
    }

    private IEnumerator ListenColliders()
    {
        while (Physics.OverlapSphere(transform.position, _radius).Length == 0)
        {
            yield return new WaitForSeconds(.05f); // прим. 20 кадров в секунду
        }

        Destroy(gameObject);
    }
}
