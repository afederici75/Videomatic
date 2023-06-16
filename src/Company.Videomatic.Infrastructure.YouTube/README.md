# Company.Videomatic.Infrastructure.YouTube.API

This project contains utility classes to access the YouTube API.
It exports the IYouTubeHelper interface which provides wrappers for the few APIs we use in Videomatic.

## JsonModels
The folder \JsonModels includes several classes that I've built using Visual Studio Paste Special/Paste As JSON classes.
The JSON was build by executing requests using https://developers.google.com/youtube/v3/docs

## TranscriptAPI
The folder \TranscriptAPI contains source code I found at https://github.com/BobLd/youtube-transcript-api-sharp
I could not find a simpler way to download transcripts so this will have to do right now.