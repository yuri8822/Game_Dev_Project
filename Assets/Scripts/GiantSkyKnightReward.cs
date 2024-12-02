using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSkyKnightReward : MonoBehaviour
{
    [SerializeField] private GameObject crystalDescription;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip victoryJingle;
    private void OnDestroy()
    {
        crystalDescription.SetActive(true);
        audioSource.Stop();
        audioSource.PlayOneShot(victoryJingle);
        Time.timeScale = 0;
    }
}
