using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioManager))]
public class Manager : MonoBehaviour
{
    public static AudioManager Audio { get; private set; }

    private List<IGameManager> _startSequence;

    // Start is called before the first frame update
    void Awake()
    {
        Audio = GetComponent<AudioManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Audio);

        StartCoroutine(StartUpManagers());
    }

    private IEnumerator StartUpManagers()
    {
        foreach(IGameManager manager in _startSequence)
        {
            manager.Startup();
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }

            if (numReady > lastReady)
            {
                Debug.Log($"Progress: {numReady} / {numModules}");
            }

            yield return null;

            Debug.Log("All managers started up!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
