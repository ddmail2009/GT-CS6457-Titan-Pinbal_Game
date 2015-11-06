using UnityEngine;
using System.Collections;

public class OneSideBumper : MonoBehaviour
{
    public float force = 20.0f;
    public string direction;
    Behaviour halo;

    // Use this for initialization
    void Start()
    {
        halo = (Behaviour)gameObject.GetComponent("Halo");
        halo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.rigidbody && col.gameObject.tag == "Ball" && ((direction == "left" && ((col.contacts[0].point - col.transform.position).x < 0)) || (direction == "right" && ((col.contacts[0].point - col.transform.position).x > 0))))
        {
            Vector3 forceVec = Vector3.ProjectOnPlane(col.transform.position - transform.position, transform.up).normalized * force;
            col.rigidbody.AddForce(forceVec, ForceMode.VelocityChange);
            if (halo.enabled == false)
            {
                StartCoroutine(make_it_glow());
            }
        }
    }

    IEnumerator make_it_glow()
    {
        halo.enabled = true;
        yield return new WaitForSeconds((float)0.5);
        halo.enabled = false;
    }
}
