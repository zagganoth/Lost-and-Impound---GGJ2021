﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/PickDestination")]
public class PickDestination : BaseState
{
    private Vector2Int possibleDestinations;
    public MapGenerator mapStance;
    protected override void childEnter(ThiefAI cur)
    {
        mapStance = MapGenerator.instance;
    }

    public override IEnumerator Perform()
    {
        yield return null;

        HashSet<Vector2Int> visited = self.getVisitedLocations();

        if(visited.Count == mapStance.destinationLocations.Count)
        {
            self.setVisitedEverything();
            Exit();
        }
        List<Vector2Int> visitable = new List<Vector2Int>();
        foreach (var dest in mapStance.destinationLocations)
        {
            if (!visited.Contains(dest))
            {
                visitable.Add(dest);
            }
        }
        int locInt = Random.Range(0, visitable.Count);
        int curIndex = 0;
        foreach(var loc in visitable)
        {
            if (curIndex == locInt)
            {
                self.setDestination(loc);
                self.visitLocation(loc);
                break;
            }
            curIndex++;
        }
        Exit();
    }

    protected override void Exit()
    {
        if (nextState && !self.hasVisitedEverything())
            self.changeState(nextState);
    }
}
