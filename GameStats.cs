using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
public class GameStats
{
    [LoadColumn(0)]
    public short Location;

    [LoadColumn(1)]
    public short TwoShots;

    [LoadColumn(2)]
    public short ThreeShots;

    [LoadColumn(3)]
    public short Reboundings;

    [LoadColumn(4)]
    public short Interceptions;

    [LoadColumn(5)]
    public short Losses;

    [LoadColumn(6)]
    public short Fouls;

    [LoadColumn(7)]
    public float ScoresDif;
}

public class GameResultPrediction
{
    [ColumnName("Score")]
    public float ScoresDif;
}
