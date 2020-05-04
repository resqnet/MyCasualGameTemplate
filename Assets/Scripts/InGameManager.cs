using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class InGameManager : MonoBehaviour
{
  public InGameStateReactiveProperty _InGameState;
  public static InGameManager Instance;

  [SerializeField]
  public Text TextTapStart;

  [SerializeField]
  public Text TextTapNext;

  private void Awake()
  {
    Instance = this;
  }

  private void Start()
  {
    _InGameState
     .DistinctUntilChanged()
     .Subscribe(_ =>
     {
       Debug.Log("Enter:" + _InGameState);
       switch (_InGameState.Value)
       {
         case InGameState.Prepare:
           Prepare();
           break;
         case InGameState.WaitStart:
           TextTapStart.enabled = true;
           break;
         case InGameState.Play:
           TextTapStart.enabled = false;
           break;
         case InGameState.Result:
           TextTapNext.enabled = true;
           break;
         default:
           Debug.Log(_InGameState);
           break;
       }
     });

    // FixedUpdate
    this.UpdateAsObservable()
      .AsObservable()
      .BatchFrame(0, FrameCountType.FixedUpdate)
      .Subscribe(_ =>
      {
        Debug.Log("Update:" + _InGameState);
        switch (_InGameState.Value)
        {
          case InGameState.WaitStart:
            UpdateWaitStart();
            break;
          case InGameState.Play:
            UpdatePlay();
            break;
          case InGameState.Result:
            if (Input.GetKeyDown(KeyCode.Space))
            {
              _InGameState.Value = InGameState.Prepare;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
              GameManager.Instance.MoveStageSelect();
            }
            break;
        }
      });

      if(_InGameState.Value == InGameState.Initialize)
      {
        _InGameState.Value = InGameState.Prepare;
      }
  }

  void Prepare()
  {
    TextTapStart.enabled = false;
    TextTapNext.enabled = false;
    _InGameState.Value = InGameState.WaitStart;
  }

  void UpdateWaitStart()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      _InGameState.Value = InGameState.Play;
    }
  }

  void UpdatePlay()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      _InGameState.Value = InGameState.Result;
    }
  }
}
