using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Options;
using System.Text;

namespace Infrastructure.AzureSpeech;

public interface ITranscriber
{
    Task TranscribeAsync(string fileName, CancellationToken token = default);

    Func<TranscriptionChunk, Task<bool>>? OnTranscribed { get; set; }
}

public record TranscriptionChunk(string Text, ulong Offset);

public class Transcriber : ITranscriber
{
    private readonly AzureSpeechOptions _options;

    public Transcriber(IOptions<AzureSpeechOptions> options)
    {
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public Func<TranscriptionChunk, Task<bool>>? OnTranscribed { get; set; }

    public async Task TranscribeAsync(string fileName, CancellationToken token)
    {
        var config = SpeechConfig.FromSubscription(_options.ApiKey, _options.ServiceRegion);

        var stopRecognition = new TaskCompletionSource<int>();

        var recognizedText = new StringBuilder();

        // Creates a speech recognizer using file as audio input.
        using (var audioInput = AudioConfig.FromWavFileInput(fileName))
        {
            using (var recognizer = new SpeechRecognizer(config, audioInput))
            {
                // Subscribes to events.
                recognizer.Recognizing += (s, e) =>
                {
                    //Console.WriteLine($"RECOGNIZING: Text={e.Result.Text}");
                };

                recognizer.Recognized += async (s, e) =>
                {
                    if (e.Result.Reason == ResultReason.RecognizedSpeech)
                    {
                        recognizedText.Append(e.Result.Text);
                        //Console.WriteLine($"RECOGNIZED: Text={e.Result.Text}");                        

                        var ok = await OnTranscribed!.Invoke(new TranscriptionChunk(e.Result.Text, e.Offset));
                        if (!ok)
                        {
                            await recognizer.StopContinuousRecognitionAsync();
                        }
                    }
                    else if (e.Result.Reason == ResultReason.NoMatch)
                    {
                        //Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                    }
                };

                recognizer.Canceled += (s, e) =>
                {
                    Console.WriteLine($"CANCELED: Reason={e.Reason}");

                    if (e.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={e.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails={e.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                    }

                    stopRecognition.TrySetResult(0);
                };

                recognizer.SessionStarted += (s, e) =>
                {
                    Console.WriteLine("\n    Session started event.");
                };

                recognizer.SessionStopped += (s, e) =>
                {
                    Console.WriteLine("\n    Session stopped event.");
                    Console.WriteLine("\nStop recognition.");
                    stopRecognition.TrySetResult(0);
                };

                // Starts continuous recognition. Uses StopContinuousRecognitionAsync() to stop recognition.
                await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);

                // Waits for completion.
                // Use Task.WaitAny to keep the task rooted.
                Task.WaitAny(new[] { stopRecognition.Task });

                // Stops recognition.
                await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
            }
        }
    }
}