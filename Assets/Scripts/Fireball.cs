using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10.0f;

    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.CompareTo("Player") == 0)
        {
            PlayCharecter playCharecter = other.gameObject.GetComponent<PlayCharecter>();

            if (playCharecter != null)
            {
                playCharecter.Hurt(damage);
            }
        }

        Destroy(this.gameObject);
    }
}
