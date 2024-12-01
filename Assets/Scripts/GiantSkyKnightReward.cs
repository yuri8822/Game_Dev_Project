using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSkyKnightReward : MonoBehaviour
{
    [SerializeField] private GameObject crystalDescription;
    private void OnDestroy()
    {
        crystalDescription.SetActive(true);
        Time.timeScale = 0;
    }
}
