# Infrastructure.SemanticKernel

This project contains the implementation of all AI-dependent components of the system. 
It is based on [Semantic Kernel](https://github.com/microsoft/semantic-kernel).

Currently it contains the following components (experimental):

1. SemanticKernelArtifactProducer: This component is responsible for producing the semantic kernel artifact using AI.
Summarize, generate TLDR, extract key phrases, etc. will be driven by SK Skills, Connectors and plugins in general.
1. 
1. Currently I am using the REDIS connector. Weaveite is another interesting option.

1. GetVideosQueryHandler: Handles the `GetVideosQuery` and returns the videos using semantic kernel/Vector DB.

# Missing

-I've done several tests in other repos and I left some of the old code in files such as SemanticKernelArtifactProducer.cs and GetVideosQueryHandler.cs

-Prompts will go here

# Notes

-Semantic Memory seems to work at least with the Serverless mode (NASA docs) but I had issues with my txt transcripts:
	-Was it because of QDrant or something else? 
	-001-dotnet-Serverless seems to work with just the API key
	
-QDrant and Weaveite boot seem cool but they don't seem 'complete' (far from it?)
	-Should I just use the MS stuff for now **OR** should I try to see if I can create a REDIS IMemoryStorage
		-There are only two big questions really (the 2 List methods)

-I need to learn Azure Queues anyways so I might as well use them for the time being
	-Azure Cognitive Search might be the most active, ready-to-use. I should R&D
