using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public GameStateReactiveProperty _GameState;
  public static GameManager Instance;
  private InGameManager _InGameManager;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  void Start()
  {
    _GameState
     .DistinctUntilChanged()
     .Where(x => x == GameState.PrepareGame)
     .Subscribe(_GameState => PrepareGame());

    _GameState.Value = GameState.PrepareGame;
  }

  private void PrepareGame()
  {
    Debug.Log("PrepareGame");
    GameObject test =GameObject.Find("InGameManager");
    _InGameManager = GameObject.Find("InGameManager").GetComponent<InGameManager>();
    _InGameManager._InGameState.Value = InGameState.Prepare;
  }

  public void MoveStageSelect()
  {
    _GameState.Value = GameState.StageSelect;
    SceneManager.LoadScene("SampleScene2");
  }

  public void MoveGameScene()
  {
    SceneManager.sceneLoaded+= SceneLoder;
    SceneManager.LoadScene("SampleScene");
  }

  void SceneLoder(Scene nextScene, LoadSceneMode mode)
  {
    Debug.Log(nextScene.name);
    if(nextScene.name == "SampleScene")
    {
      //SampleScene
    }
  }
}
