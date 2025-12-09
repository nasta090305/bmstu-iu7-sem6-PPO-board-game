namespace GameLogic;

public interface BaseGameManager
{
    public void StartGame(List<string> players_names);
    public void ContinueGame();
    public GameDTO GetInfo();
    public List<CardDTO> ShowDiscard1();
    public List<CardDTO> ShowDiscard2();
    public List<CardDTO> ShowUpper2Deck();
    public void MakeTurn(DeckType deck_id, int card_id = 0, DeckType discard = 0);
    public bool CheckPlayable(int card1_id, int card2_id);
    public void PlayCards(int card1_id, int card2_id, params int[] args);
    public bool CheckEndGame();
    public GameResultDTO? FinishTurn();
    public GameResultDTO FinishGame();
}
