# DeleteVideo

## DeleteVideoCommand

The DeleteVideo Command is used to delete a video from the repository.

### Properties
- VideoId (int): The id of the video to delete.

## DeleteVideoResponse
The response is a DeleteVideoResponse.
If the Success property is true, the video was deleted successfully.
If the Success property is false, the video was not found and could not be deleted.

## Example(s)

- Deletes the video with the id of 1
```csharp
var command = new DeleteVideoCommand { VideoId = 1 };

var response = await _mediator.Send(command);

if (response.Success)
{
	// Handle success	
}
else
{
	// Handle failure
}
```