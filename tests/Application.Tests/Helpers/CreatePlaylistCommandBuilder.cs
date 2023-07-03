namespace Application.Tests.Helpers;

public class CreatePlaylistCommandBuilder
{
    /// <summary>
    /// Static factory method that requires no state.
    /// </summary>
    /// <param name="textId"></param>
    /// <returns></returns>
    public static CreatePlaylistCommand WithDummyValues([System.Runtime.CompilerServices.CallerMemberName] string textId = "")
    {
        var name = $"{textId}";
        var description = $"The description of playlist {textId}.";

        return new CreatePlaylistCommand(Name: name, Description: description);
    }
}