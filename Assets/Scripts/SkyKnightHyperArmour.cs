using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyKnightHyperArmour : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float iFramesDuration = 1f;
    [SerializeField] private int noFlashes = 3;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer.GetComponent<SpriteRenderer>();
    }

    private void ActivateHyperArmour()
    {
        StartCoroutine(Iframes());
    }

    private IEnumerator Iframes()
    {
        for(int i = 0; i < noFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration/ (noFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration/ (noFlashes * 2));
        }
    }
}
