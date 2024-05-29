using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlossaryManager : MonoBehaviour
{
    public List<GameObject> nonVisibleOnStart = new List<GameObject>();

    [SerializeField] private float _slideSpeed = 5;
    [SerializeField] private float _fasrSlideSpeed = 10000;
    [SerializeField] private float _currentIndex = 20;
    [SerializeField] private bool _canUseButton = true;

    [SerializeField] private GridLayoutGroup _layoutGroup;
    [SerializeField] private RectTransform _contentRect; 

    private Coroutine _currentCoroutine;

    
    /* intervalos en Padding
     20
    -1940
    -3900
    -5860
    -7820
    -9780
    -11740
    -13700
     */
    private void Start()
    {
        if (_contentRect != null)
            _contentRect.anchoredPosition = new Vector2(_currentIndex, _contentRect.anchoredPosition.y);

        //DeactiveElements();

    }
    [ContextMenu("Increase")]
    public void handleIncrease()
    {
        if (_canUseButton)
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = StartCoroutine(IncreaseIndex());
        }
    }

    public void handleDecrease()
    {
        if (_canUseButton)
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = StartCoroutine(DecreaseIndex());
        }
    }
    
    
 

    private void Update()
    {
       // _layoutGroup.padding.top += 1;
    }

    /*  private void increaseIndex(ref float index)
      {
          if (_canUseButton)
          {
              index -= 1960;
              Debug.Log(index);
          }
          if (index < -13700) index = 20;
      }*/

    private IEnumerator IncreaseIndex()
    {
        _canUseButton = false;
        float targetIndex = _currentIndex - 1800;
        if (targetIndex < -16200) /////////////////////////////////////////////////////
            targetIndex = 20;

        
        while (_currentIndex != targetIndex)
        {
            _currentIndex = Mathf.MoveTowards(_currentIndex, targetIndex, _slideSpeed * Time.deltaTime);
            if (_contentRect != null)
                _contentRect.anchoredPosition = new Vector2(_currentIndex, _contentRect.anchoredPosition.y);

            Debug.Log("Current Index: " + targetIndex);

            yield return null; 
        }
        _canUseButton = true;

    }

    private IEnumerator DecreaseIndex()
    {
        _canUseButton = false;
        float targetIndex = _currentIndex + 1800;  
        if (targetIndex > 20)  
            targetIndex = -16200; ////////////////////////////////

        while (_currentIndex != targetIndex)
        {
            _currentIndex = Mathf.MoveTowards(_currentIndex, targetIndex, _slideSpeed * Time.deltaTime);
            if (_contentRect != null)
                _contentRect.anchoredPosition = new Vector2(_currentIndex, _contentRect.anchoredPosition.y);

            Debug.Log("Current Index: " + _currentIndex);  

            yield return null;
        }
        _canUseButton = true;
    }


    float currentSpeed(bool isNormal)
    {
        if (isNormal) return _slideSpeed;
        else return _fasrSlideSpeed;
    }


    [ContextMenu("Set Initial")]

    public void ResetToOrigin()
    {
      
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = StartCoroutine(MoveToOrigin());
        
    }

    private IEnumerator MoveToOrigin()
    {
        _canUseButton = false;
        float targetIndex = 20;  

        while (_currentIndex != targetIndex)
        {
            _currentIndex = Mathf.MoveTowards(_currentIndex, targetIndex, _slideSpeed * Time.deltaTime*20);
            if (_contentRect != null)
                _contentRect.anchoredPosition = new Vector2(_currentIndex, _contentRect.anchoredPosition.y);

            Debug.Log("Current Index: " + _currentIndex);  

            yield return null;
        }

        _canUseButton = true;
    }

    public void ActiveElements()
    {
        foreach (var item in nonVisibleOnStart)
        {
            item.SetActive(true);
        }
    }
    public void DeactiveElements()
    {
        foreach (var item in nonVisibleOnStart)
        {
            item.SetActive(false);
        }
    }

    public void deactivateGlossary()
    {
        gameObject.SetActive(false);
    }
}
