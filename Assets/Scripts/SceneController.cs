using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    private GameObject _enemy;

    private float speed = 0;

    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemy == null)
        {
            _enemy = Instantiate(enemyPrefab);
            _enemy.transform.position = this.transform.position;
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);

            WanderingAI ai = _enemy.GetComponent<WanderingAI>();

            if (ai != null)
            {
                if (speed < 1e-6)
                {
                    speed = ai.speed;
                }
                else
                {
                    ai.speed = speed;
                }
            }


        }
    }
    private void OnSpeedChanged(float value)
    {
        if (_enemy != null)
        {
            WanderingAI ai = _enemy.GetComponent<WanderingAI>();

            if (ai != null)
            {
                speed = ai.baseSpeed * value;
            }
        }
    }

}
