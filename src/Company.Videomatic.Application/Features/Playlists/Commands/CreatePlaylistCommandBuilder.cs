namespace Company.Videomatic.Application.Features.Playlists.Commands;

public class CreatePlaylistCommandBuilder
{ 
    public CreatePlaylistCommand WithDummyValues([System.Runtime.CompilerServices.CallerMemberName] string textId = "")
    {
        var name = $"{textId}";
        var description = $"The description of playlist {textId}.";

        return new CreatePlaylistCommand(Name: name, Description: description);
    }
}