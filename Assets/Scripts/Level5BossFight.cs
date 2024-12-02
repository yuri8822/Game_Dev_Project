using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level5BossFight : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject bossHealthBar;
    [SerializeField] private GameObject[] gusts;
    [SerializeField] private GameObject boss;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bossMusic;
    [SerializeField] private PlayerMechanics player;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            obstacle.SetActive(true);
            bossHealthBar.SetActive(true);
            boss.SetActive(true);
            foreach (GameObject gust in gusts)
            {
                gust.SetActive(true);
            }
        }
        audioSource.clip = bossMusic;
        audioSource.Play();
        gameObject.SetActive(false);
        player.Heal(100);
    }
}
