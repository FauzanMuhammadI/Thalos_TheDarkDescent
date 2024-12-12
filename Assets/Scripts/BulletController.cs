using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletLife = 2f;
    public float speed = 5f;

    private float timer = 0f;

    private void Start()
    {
        //transform.rotation = Quaternion.Euler(-90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.position = new Vector3(transform.position.x, transform.position.y - 13.5f, transform.position.z);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > bulletLife)
        {
            Destroy(this.gameObject);
        }

        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
