using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AnimatedGridBase: MonoBehaviour
{
    public List<GameObject> animatedObjects;
    public float hideDelay = 0.1f;
    public float showDelay = 0.3f;
    private bool _isShown = false;

    private void Awake()
    {
        Hide();
    }

    private void Hide()
    {
        _isShown = false;
        foreach (GameObject g in animatedObjects)
        {
           g.SetActive(false);
        }
    }

    public void ShowItens()
    {
        if (_isShown)
        {
            Hide();
        }
        else 
        {
            _isShown = true;
            StartCoroutine(Show());
        }

    }

    IEnumerator Show()
    {
        foreach (GameObject g in animatedObjects)
        {
            yield return new WaitForSeconds(hideDelay);
            g.SetActive(true);
            g.transform.DOScale(0, showDelay).From();
        }
        


    }
}
