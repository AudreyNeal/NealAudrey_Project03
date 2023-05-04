using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
  [Header("Movement Settings")]
  [SerializeField] float _speed = 2;
  public enum TypeOfMotion
  {
    LOOPING,
    BACK_AND_FORTH,
    ONE_WAY
  };
  public TypeOfMotion _typeOfMotion;


  [SerializeField] bool PlayOnAwake = true;

  [Header("Movement possitions")]
  // node system 

  public Transform[] _nodes = new Transform [3];
  public float[] _waitTimes = new float [3];

  private int _currentNode = 0;
  public bool _reverse = false;
  // allows for multiple nodes each with their own wait time

  [Header("Audio")]
[SerializeField] AudioClip _audioClip = null;
public enum AudioPlayback
{
  LOOPING,
  PLAY_AT_START,
  PLAY_AT_END,

}

    
    void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    Debug.Log(_currentNode);
    if (PlayOnAwake)
    {
      MoveObject();
    }
    
    }

    public void MoveObject()
  {
    if (Vector3.Distance(transform.position, _nodes[_currentNode].position) < 0.1f)
    {

      StartCoroutine(Wait(_speed));
      if (_typeOfMotion == TypeOfMotion.LOOPING)
      {
        if (_currentNode == _nodes.Length - 1)
        {
          _reverse = true;

        }
        else if (_currentNode == 0) { _reverse = false; }

        if (_reverse == true)
        {
          _currentNode--;
        }
        if (_reverse == false)
        {
          _currentNode++;
        }
      }
      if (_typeOfMotion == TypeOfMotion.BACK_AND_FORTH) 
      { 
      bool CycleMade = false;
        if (CycleMade == true) 
        {
          _currentNode = 0;
        return; 
        
        }
        

        if (_reverse == true )
        {
          _currentNode--;
        }
        if (_reverse == false)
        {
          _currentNode++;
        }
        if (_currentNode == _nodes.Length - 1)
        {
          _reverse = true;

        }
     
        if (_currentNode <= 0 && _reverse == true)
        {
        _currentNode = 0;
          CycleMade= true;
        }



      }
    }



      transform.position = Vector3.MoveTowards(gameObject.transform.position, _nodes[_currentNode].position, _speed * Time.deltaTime);

  }

  private IEnumerator Wait (float currentSpeed) 
  {
    _speed = 0;
    yield return new WaitForSeconds(_waitTimes[_currentNode]);
    _speed = currentSpeed;
  }
}
