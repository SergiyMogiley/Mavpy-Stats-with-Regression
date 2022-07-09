using Microsoft.ML;

string _trainDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "Mavpy_14_15_train.csv");
string _testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "Mavpy_14_15_test.csv");
string _modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "Model_Mavpy.zip");

MLContext mlContext = new MLContext(seed: 0);

var model = Train(mlContext, _trainDataPath);

ITransformer Train(MLContext mlContext, string dataPath)
{
    IDataView dataView = mlContext.Data.LoadFromTextFile<GameStats>(dataPath, hasHeader: true, separatorChar: ',');

    var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "ScoresDif")

        .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "LocationEncoded", inputColumnName: "Location"))
        .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "TwoShotsEncoded", inputColumnName: "TwoShots"))
        .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "ThreeShotsEncoded", inputColumnName: "ThreeShots"))
        .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "ReboundingsEncoded", inputColumnName: "Reboundings"))
        .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "InterceptionsEncoded", inputColumnName: "Interceptions"))
        .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "LossesEncoded", inputColumnName: "Losses"))
        .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "FoulsEncoded", inputColumnName: "Fouls"))

        .Append(mlContext.Transforms.Concatenate("Features", "LocationEncoded", "TwoShotsEncoded", "ThreeShotsEncoded",
                                                 "ReboundingsEncoded", "InterceptionsEncoded", "LossesEncoded", "FoulsEncoded"))

    .Append(mlContext.Regression.Trainers.FastTree());

    var model = pipeline.Fit(dataView);

    mlContext.Model.Save(model, dataView.Schema, _modelPath);

    return model;
}

Evaluate(mlContext, model);

void Evaluate(MLContext mlContext, ITransformer model)
{
    IDataView dataView = mlContext.Data.LoadFromTextFile<GameStats>(_testDataPath, hasHeader: true, separatorChar: ',');

    var predictions = model.Transform(dataView);

    var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");

    Console.WriteLine();
    Console.WriteLine($"*************************************************");
    Console.WriteLine($"*       Model quality metrics evaluation         ");
    Console.WriteLine($"*------------------------------------------------");

    Console.WriteLine($"*       RSquared Score:      {metrics.RSquared:0.##}");

    Console.WriteLine($"*       Root Mean Squared Error:      {metrics.RootMeanSquaredError:#.##}");
}

TestSinglePrediction(mlContext, model);

void TestSinglePrediction(MLContext mlContext, ITransformer model)
{
    var predictionFunction = mlContext.Model.CreatePredictionEngine<GameStats, GameResultPrediction>(model);

    var GameStatsSample = new GameStats()
    {
        Location = 0,
        TwoShots = 48,
        ThreeShots = 27,
        Reboundings = 46,
        Interceptions = 5,
        Losses = 6,
        Fouls = 15,
        ScoresDif = 0
    };

    var prediction = predictionFunction.Predict(GameStatsSample);

    Console.WriteLine($"**********************************************************************");
    Console.WriteLine($"Predicted scores dif: {prediction.ScoresDif:0.####}, actual scores dif: -9");
    Console.WriteLine($"**********************************************************************");
}