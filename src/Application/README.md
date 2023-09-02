# Application

The main functionality of Videomatic.

This project follows the principles of [Clean Architecture](https://ardalis.com/clean-architecture-asp-net-core/).
It defines application 'Features' (i.e. 'Use Cases') which are the main building blocks of the application.

<img src="https://miro.medium.com/v2/resize:fit:1100/format:webp/0*cKlf8Eymfs0hu8-2.png" width="400"/>

The project also follows the principles of CQRS [Command Query Responsibility Segregation](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)

## Dependencies

1. Domain
1. ShardKernel

## Features

They are located in the `Features` folder and are divided in groups:

1. Arifacts
1. Playlists
1. Transcripts 
1. Videos

Browse each folder to see the available features.
