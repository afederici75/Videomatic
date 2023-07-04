# YoutubeTranscriptAPI

This project and the relative test project YoutubeTranscriptApi.Tests are a port of 
the repository https://github.com/BobLd/youtube-transcript-api-sharp

The changes were:
-YoutubeTranscriptAPI.csproj went from 

```
<Project Sdk="Microsoft.NET.Sdk">
	
	<PropertyGroup>
		<TargetFramework>net5.0</TargetFramework>
		<Version>0.4.1</Version>
	</PropertyGroup>
	
	<PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
		<DocumentationFile>C:\Users\Bob\source\repos\youtube-transcript-api-sharp\YoutubeTranscriptApi\YoutubeTranscriptApi.xml</DocumentationFile>
		<GenerateSerializationAssemblies>On</GenerateSerializationAssemblies>
	</PropertyGroup>
	
	<PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
	  <DocumentationFile>C:\Users\Bob\source\repos\youtube-transcript-api-sharp\YoutubeTranscriptApi\YoutubeTranscriptApi.xml</DocumentationFile>
	</PropertyGroup>

	<ItemGroup>
		<AssemblyAttribute Include="System.Runtime.CompilerServices.InternalsVisibleTo">
			<_Parameter1>$(MSBuildProjectName).Tests</_Parameter1>
		</AssemblyAttribute>
	</ItemGroup>

</Project>
```

to 

```
<Project Sdk="Microsoft.NET.Sdk">
	
	<PropertyGroup>
		<TargetFramework>net5.0</TargetFramework>
		<Version>0.4.1</Version>
    <LangVersion>10</LangVersion>
    <Nullable>disable</Nullable>
	</PropertyGroup>
	<ItemGroup>
		<AssemblyAttribute Include="System.Runtime.CompilerServices.InternalsVisibleTo">
			<_Parameter1>$(MSBuildProjectName).Tests</_Parameter1>
		</AssemblyAttribute>
	</ItemGroup>

</Project>
```

The test project only got these additions:
```
    <LangVersion>10</LangVersion>
    <Nullable>disable</Nullable>
```

Without these changes it would not comompile.

Errors included:
Severity	Code	Description	Project	File	Line	Suppression State
Error	NETSDK1005	Assets file doesn't have a target for 'net5.0'. Ensure that restore has run and that you have included 'net5.0' in the TargetFrameworks for your project.	YoutubeTranscriptApi	C:\Program Files\dotnet\sdk\7.0.304\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets	266	

Severity	Code	Description	Project	File	Line	Suppression State
Error		An attempt was made to load an assembly with an incorrect format: C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\Microsoft.CSharp.dll.	YoutubeTranscriptApi	D:\youtube-transcript-api-sharp\YoutubeTranscriptApi\SGEN	1	


