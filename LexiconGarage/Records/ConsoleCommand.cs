namespace LexiconGarage.Records;

public record ConsoleCommand( Action<string[]> ConsoleAction,string description);