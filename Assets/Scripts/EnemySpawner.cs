using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int i = startingWave; i < waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));        
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig wave)
    {
        for (int i = 0; i < wave.getNumOfEnemies(); i++)
        {
            var newEnemy = Instantiate(wave.getEnemy(), wave.getWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().setWaveConfig(wave);
            yield return new WaitForSeconds(wave.getTimeBetweenSpawns());
        }
    }
}
