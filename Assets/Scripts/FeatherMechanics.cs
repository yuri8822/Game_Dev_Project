using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherMechanics : MonoBehaviour
{
    [SerializeField] private GameObject[] gustsOfWind;
    [SerializeField] private GameObject featherDescription;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach(GameObject gust in gustsOfWind)
            {
                gust.SetActive(true);
            }
            featherDescription.SetActive(true);
        }
        gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void OnClickExit()
    {
        featherDescription.SetActive(false);
        Time.timeScale = 1;
    }
}
