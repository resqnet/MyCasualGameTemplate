using UniRx;

public enum GameState
{
	Initialize,
  PrepareGame,
  InGame,
  StageSelect,
  Finalize
}

public enum InGameState
{
  Initialize,
  Prepare,
  WaitStart,
  Play,
  Gameover,
  Result,
  Finalize
}

[System.Serializable]
public class GameStateReactiveProperty : ReactiveProperty<GameState>
{
  public GameStateReactiveProperty() { }
  public GameStateReactiveProperty(GameState initialValue) : base(initialValue) { }
}

[System.Serializable]
public class InGameStateReactiveProperty : ReactiveProperty<InGameState>
{
  public InGameStateReactiveProperty() { }
  public InGameStateReactiveProperty(InGameState initialValue) : base(initialValue) { }
}

