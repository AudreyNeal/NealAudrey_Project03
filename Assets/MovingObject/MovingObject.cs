using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
  [Header("Movement Settings")]
  [Space(3)]
  [SerializeField] float _speed = 2;
  public enum TypeOfMotion
  {
    LOOPING,
    BACK_AND_FORTH,
    ONE_WAY
  };
  public TypeOfMotion _typeOfMotion;


  [SerializeField] bool PlayOnAwake = true;

  // node system 

  [Space(3)]

  public Transform[] _nodes = new Transform[3];
  public float[] _waitTimes = new float[3];

  private int _currentNode = 0;
  private bool _reverse = false;
  // allows for multiple nodes each with their own wait time

  [Header("Audio")]
  [SerializeField] AudioClip _audioToPlay = null;
  public enum AudioPlayback
  {
    LOOPING,
    PLAY_AT_START,
    PLAY_AT_END,
  };
  public AudioPlayback _audioPlayback;


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
        PlayAtStart();
        PlayLoop();
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
        PlayAtStart();
        PlayLoop();
        bool CycleMade = false;
        if (CycleMade == true)
        {
          _currentNode = 0;
          return;

        }


        if (_reverse == true)
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
          PlayAtEnd();
          CycleMade = true;
        }
      }

      if (_typeOfMotion == TypeOfMotion.ONE_WAY)
      {
      PlayAtStart();
      PlayLoop();
        _currentNode++;
        if (_currentNode >= _nodes.Length - 1)
        {
          _currentNode = _nodes.Length - 1;
          PlayAtEnd();
          return;
        }

      }
     
    }



    transform.position = Vector3.MoveTowards(gameObject.transform.position, _nodes[_currentNode].position, _speed * Time.deltaTime);

  }

  private IEnumerator Wait(float currentSpeed)
  {
    _speed = 0;
    yield return new WaitForSeconds(_waitTimes[_currentNode]);
    _speed = currentSpeed;
  }

  private void PlayLoop()
  {
    if (_audioToPlay != null && GetComponent<AudioSource>() != null)
    {
      if (_audioPlayback == AudioPlayback.LOOPING)
      {
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().clip = _audioToPlay;
        GetComponent<AudioSource>().Play();
      }
      else { GetComponent<AudioSource>().loop = false; }
    }


  }
  private void PlayAtEnd()
  {
    if (_audioToPlay != null && GetComponent<AudioSource>() != null)
    {
      bool played = false;
      if (_audioPlayback == AudioPlayback.PLAY_AT_END && _currentNode == _nodes.Length - 1 && played == false)
      {
        GetComponent<AudioSource>().clip = _audioToPlay;
        GetComponent<AudioSource>().Play();
        played = true;

      }
    }
    }

    private void PlayAtStart()
  {
    if (_audioToPlay != null && GetComponent<AudioSource>() != null)
    {
      bool played = false;
      if (_audioPlayback == AudioPlayback.PLAY_AT_START && _currentNode == 0 && played == false)
      {
        GetComponent<AudioSource>().clip = _audioToPlay;
        GetComponent<AudioSource>().Play();
        played = true;

      }
    }
  }
}
