var directories = (
    Backend: Tuple.Create(
        Tuple.Create(Tuple.Create("C#", "Java", "Python"))
    ),
    Frontend: Tuple.Create(
        Tuple.Create(Tuple.Create("Flutter", "Android/IOS UI", "Frontend(Web)"))
    )
);

Console.WriteLine(directories.Backend.Item1.Item1.Item1);
