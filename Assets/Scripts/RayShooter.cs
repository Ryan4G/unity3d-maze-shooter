using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour
{
    public float shoot_interval = 1.0f;

    private Camera _camera;

    [SerializeField]
    private AudioSource soundSource;
    [SerializeField]
    private AudioClip hitWallSound;
    [SerializeField]
    private AudioClip hitEnemySound;

    // Start is called before the first frame update
    void Start()
    {
        _camera = this.GetComponent<Camera>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.shoot_interval -= 0.5f * Time.deltaTime;

        if (Input.GetMouseButton(0) && shoot_interval < 0 && !EventSystem.current.IsPointerOverGameObject())
        {
            shoot_interval = 1.0f;

            Vector3 point = new Vector3(_camera.pixelWidth * 0.5f, _camera.pixelHeight * 0.5f, 0);

            Ray ray = _camera.ScreenPointToRay(point);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject go = hit.transform.gameObject;
                ReactiveTarget target = go.GetComponent<ReactiveTarget>();

                if (target != null)
                {
                    target.ReactToHit();
                    soundSource.PlayOneShot(hitEnemySound);
                    Messenger.Boardcast(GameEvent.ENEMY_HIT);
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                    soundSource.PlayOneShot(hitWallSound);
                }
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }

    private void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth * 0.5f - size / 4;
        float posY = _camera.pixelHeight * 0.5f - size / 2;

        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}
