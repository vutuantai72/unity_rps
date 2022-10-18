public enum SelectType
{
    scissors,
    rock,
    paper,
}

public enum GameState
{
    Initializing,
    InsertingCoin,
    Selecting,
    Selected,
    SelectionComplete,
    Result,
    CoinRoulette,
    GetCoins,
    CoinResult,
    EndGame,
    //Jacpost
}

public enum ResultType
{
    Draw,
    Win,
    Lose
}

public enum GameMode
{
    NONE = 0,
    PVP =1,
    TOURNAMENT =2,
    TOURNAMENT1VSN = 3,
}

public enum ReasonMode
{
    OUTROOM = 1,
    MATCHINGTIMEOUT,
    OUTOFCOIN,
    CONTINUETIMEOUT,
}

public enum ReasonModeTour
{
    MATCHINGTIMDENIED = 1,
    MATCHINGTIMEOUT = 3,
    OUTOFCOIN = 5,
}