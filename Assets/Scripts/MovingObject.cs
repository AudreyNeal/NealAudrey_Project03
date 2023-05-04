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
    MOVE_ONCE,
  }
  [SerializeField] bool PlayOnAwake = true;

  [Header("Movement possitions")]
  // node system 
  public int _numberOfNodes = 1;

  public Transform[] _nodes = new Transform [3];
  public float[] _waitTimes = new float [3];

  private int _currentNode = 0; 
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
        MoveObject();
    Debug.Log(_currentNode);
    }

    public void MoveObject()
  {
  if (Vector3.Distance(transform.position, _nodes[_currentNode].position) < 0.1f)
    {
      StartCoroutine(Wait(_speed));
      _currentNode++;
      /*
      if (_currentNode >= _nodes.Length)
      {
        //return;
        _currentNode --;

      }
      */
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
