using UniRx;
using UnityEngine;
using System;
public partial class GameService : ManagerService<GameManager, GameService>
{
    public IObservable<ResultType> MakeResultStream()
    {
        return WhenSelectionIsComplete().SelectMany(IssuedAfterConfirmingTheResult());
    }

    public IObservable<ResultType> IssuedAfterConfirmingTheResult()
    {
        IObservable<ResultType> InSaseOfRain = enemyType
            .Where(type => type == playerType.Value)
            .Select(s => ResultType.Draw);

        IObservable<SelectType> scissors = enemyType.Where(type => type == global::SelectType.scissors);
        IObservable<SelectType> rock = enemyType.Where(type => type == global::SelectType.rock);
        IObservable<SelectType> paper = enemyType.Where(type => type == global::SelectType.paper);

        IObservable<SelectType> losing1 = scissors.Where(type => playerType.Value == global::SelectType.paper);
        IObservable<SelectType> losing2 = rock.Where(type => playerType.Value == global::SelectType.scissors);
        IObservable<SelectType> losing3 = paper.Where(type => playerType.Value == global::SelectType.rock);

        IObservable<SelectType> win1 = scissors.Where(type => playerType.Value == global::SelectType.rock);
        IObservable<SelectType> win2 = rock.Where(type => playerType.Value == global::SelectType.paper);
        IObservable<SelectType> win3 = paper.Where(type => playerType.Value == global::SelectType.scissors);

        IObservable<ResultType> InCaseOfLosing =
            losing1
            .Merge(losing2)
            .Merge(losing3)
            .Select(s => ResultType.Lose);

        IObservable<ResultType> IfYouWin =
            win1
            .Merge(win2)
            .Merge(win3)
            .Select(s => ResultType.Win);

        IObservable<ResultType> @case = InSaseOfRain.Merge(InCaseOfLosing).Merge(IfYouWin);

        return @case.First();
    }
}
