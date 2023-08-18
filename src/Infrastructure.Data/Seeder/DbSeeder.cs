using Domain;
using Domain.Videos;
using SharedKernel.Model;

namespace Infrastructure.Data.Seeder;

public class DbSeeder : IDbSeeder
{
    readonly IMyRepository<Video> VideoRepository;
    readonly IMyRepository<Playlist> PlaylistRepository;
    readonly IMyRepository<Artifact> ArtifactRepository;
    readonly IMyRepository<Transcript> TranscriptRepository;

    public DbSeeder(
        IMyRepository<Video> videoRepository,
        IMyRepository<Playlist> playlistRepository,
        IMyRepository<Artifact> artifactRepository,
        IMyRepository<Transcript> transcriptRepository)
    {
        VideoRepository = videoRepository ?? throw new ArgumentNullException(nameof(videoRepository));
        PlaylistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));
        ArtifactRepository = artifactRepository ?? throw new ArgumentNullException(nameof(artifactRepository));
        TranscriptRepository = transcriptRepository ?? throw new ArgumentNullException(nameof(transcriptRepository));
    }

    public async Task SeedAsync()
    {
        await CreateEasternPhilosophyPlaylist();
        await CreateBurningManPlaylist();
    }

    async Task<int> CreateBurningManPlaylist()
    {
        var playlist = new Playlist("Burning Man", "Videos about Burning Man");
        await PlaylistRepository.AddAsync(playlist);        

        return playlist.Id;
    }

    async Task<long> CreateEasternPhilosophyPlaylist()
    {
        var playlist = new Playlist("Eastern Philosophy", "Videos about Eastern Philosophy");
        await PlaylistRepository.AddAsync(playlist);

        // Aldous Huxley - The Dancing Shiva
        var shivaVideo = await CreateAldousHuxleyTheDancingShivaVideo();
        
        // If RealityIsNotDual
        var realityNotDualVideo = await CreateIfRealityIsNonDualVideo();

        //
        await PlaylistRepository.LinkPlaylistToVideos(playlist.Id, new[] { shivaVideo.Id});
        await PlaylistRepository.LinkPlaylistToVideos(playlist.Id, new[] { realityNotDualVideo.Id });

        return playlist.Id.Value;
    }

    async Task<Video> CreateAldousHuxleyTheDancingShivaVideo()
    {
        var origin = new EntityOrigin(
                    providerId: "YOUTUBE",
                    providerItemId: "n1kmKpjk_8E",
                    etag: "",
                    channelId: "UNKNOWN",
                    channelName: "James Dearden Bush",                   
                    publishedOn: new DateTime(2019, 12, 12), // Bogus date
                    thumbnail: new ImageReference("https://i.ytimg.com/vi/n1kmKpjk_8E/default.jpg", 120, 90),
                    picture: new ImageReference("https://i.ytimg.com/vi/n1kmKpjk_8E/maxresdefault.jpg", 1280, 720),
                    embedHtml: null,
                    defaultLanguage: "EN",
                    //
                    name: "Aldous Huxley - The Dancing Shiva",
                    description: "Aldous Huxley beautifully describes the 'The Dancing Shiva' symbol " +
                                 "(Nataraja, Nataraj, nət̪əˈraːdʒ) of the Hindu spiritual tradition.  Aldous Huxley " +
                                 "was the author of many excellent books and essays including; Brave New World, " +
                                 "Island, The Perennial Philosophy and The Doors of Perception. " +
                                 "\n\n🎬 A RevolutionLoveEvolve production\nAudio clip is taken from an interview " +
                                 "which took place in London, England in 1961 entitled 'Aldous Huxley - Speaking " +
                                 "Personally'." +
                                 "\n\nhttps://en.wikipedia.org/wiki/Aldous_Huxley" +
                                 "\nhttps://en.wikipedia.org/wiki/Nataraja" +
                                 "\nhttps://en.wikipedia.org/wiki/Shiva" +
                                 "\n" +
                                 "\nSupport the channel:" +
                                 "\n🎵 Download my music: https://jamesdeardenbush.bandcamp.com" +
                                 "\n👕 Buy a T-Shirt etc: https://www.redbubble.com/people/revoloveevolve/shop " +
                                 "\n" +
                                 "\nWeb:" +
                                 "\n🌐 Website: https://www.jamesdeardenbush.com" +
                                 "\n🎵 Music: https://jamesdeardenbush.bandcamp.com" +
                                 "\n👕 Merch: https://www.redbubble.com/people/jdeardenbush" +
                                 "\n📹 LBRY: https://lbry.tv/$/invite/@jamesdeardenbush:a" +
                                 "\n📺 YouTube https://www.youtube.com/jamesdeardenbush" +
                                 "\n🎧 Spotify: https://open.spotify.com/artist/0Nv6I1nNV1Fb7hW5rKGHR1" +
                                 "\n🍏 Apple Music: https://music.apple.com/gb/artist/james-dearden-bush/1537309293" +
                                 "\n📷 Instagram: https://www.instagram.com/jdeardenbush" +
                                 "\n🤮 Facebook: https://www.facebook.com/jamesdeardenbush" +
                                 "\n🐦 Twitter: https://twitter.com/jdeardenbush" +
                                 "\n" +
                                 "\nNo copyright is claimed and to the extent that material may appear to be infringed, I " +
                                 "assert that such alleged infringement is permissible under fair use principles in U.S. " +
                                 "copyright laws. This video is fair use under U.S. copyright law because it is (1) noncommercial " +
                                 "(2) transformative in nature, (3) does not compete with the original work or have any negative " +
                                 "effect on its market and (4) uses no more of the original work than necessary for the video's " +
                                 "purpose. If you believe material has been used in an unauthorized manner, please contact the " +
                                 "poster.");

        // Video
        var video = new Video(origin.Name, origin.Description);                
        video.SetOrigin(origin);    

        // Tags
        video.SetTags("HINDUISM", "HUXLEY");

        await VideoRepository.AddAsync(video);        

        // Transcript
        await CreateAldousHuxleyTheDancingShivaTranscription(video.Id);

        // Artifacts
        await CreateAldousHuxleyTheDancingShivaArtifacts(video.Id);

        return video;
    }

    private async Task CreateAldousHuxleyTheDancingShivaArtifacts(VideoId videoId)
    {
        var summaryArtifact = new Artifact(videoId, "Summary", "SUMMARY", "AI generated summary (#ShivaVideo)");
        var contentsArtifact = new Artifact(videoId, "Contents", "CONTENTS_LIST", "AI generated contents (#ShivaVideo)");

        await ArtifactRepository.AddRangeAsync(new[] { summaryArtifact, contentsArtifact });
    }

    private async Task CreateAldousHuxleyTheDancingShivaTranscription(VideoId videoId)
    {
        // Transcripts
        var transcript = new Transcript(videoId, "US", new[]
        {
            "when you think of the",
            "staggering symbols that the Indians produced. I mean the the dancing Shiva for example.",
            "We've never produced anything as comprehensive as this.",
            "The dancing Shiva... There's this little bronze statues. It is the Shiva with four arms dancing with one foot raised.",
            "And well, I mean I'll go into the details: They are really quite extraordinary. It's said that the figure stands",
            "within a great circle - sort of halo which has flames going out on the symbols of flames and this is the",
            "circle of mass energy space-time. I mean this is the material world, the great world of the",
            "all-embracing",
            "Material world with its flames. Within this Shiva dances. He's called Nataraja, the Lord of the dance and",
            "he dances.",
            "He's everywhere in the universe. I mean they said this is his dance.",
            "Manifestation of the world is called his 'Lila', his play.",
        });

        await TranscriptRepository.AddAsync(transcript);
    }

    async Task<Video> CreateIfRealityIsNonDualVideo()
    {
        var origin = new EntityOrigin(
                    providerId: "YOUTUBE",
                    providerItemId: "BBd3aHnVnuE",
                    etag: "",
                    channelId: "UNKNOWN",
                    channelName: "ArshaBodha - Swami Tadatmananda",
                    publishedOn: new DateTime(2021, 3, 23), // Bogus date
                    thumbnail: new ImageReference("https://i.ytimg.com/vi/BBd3aHnVnuE/default.jpg", 120, 90),
                    picture: new ImageReference("https://i.ytimg.com/vi/BBd3aHnVnuE/maxresdefault.jpg", 1280, 720),
                    embedHtml: null,
                    defaultLanguage: "EN",
                    //
                    name: "If Reality is NON-DUAL, Why are there so many GODS in Hinduism?",
                    description: "Hindu scriptures offer different teachings to meet the individual needs of different " +
                                 "people - according to the crucial principle of adhikari-bheda (individualism). Q&A with Swamiji #8. " +
                                 "\n" +
                                 "\n" +
                                 "The teachings of the rishis, the sages of ancient India, are found in the Vedas. The Vedas are a " +
                                 "compilation of their teachings, teaching meant for different groups of people. Thus the Vedas include " +
                                 "the karma kanda, the section that teaches prayer and rituals for those who want contentment in this " +
                                 "life and heaven (swarga) in the next live, and it also includes the jnana kanda, the section on pure " +
                                 "spiritual teachings, for those who recognize the limitations of worldly life (samsara) and seek moksha, " +
                                 "liberation or enlightenment, instead." +
                                 "\n" +
                                 "\nSwami Tadatmananda is a traditionally-trained teacher of Advaita Vedanta, meditation, and Sanskrit. " +
                                 "For more information, please see: https://www.arshabodha.org/");

        // Video
        var video = new Video(origin.Name, origin.Description);
        video.SetOrigin(origin);

        // Tags
        video.SetTags("HINDUISM");

        // Thumbnails
        //video.SetThumbnail(ThumbnailResolution.Default, "https://i.ytimg.com/vi/BBd3aHnVnuE/default.jpg", 90, 120);
        //video.SetThumbnail(ThumbnailResolution.Medium, "https://i.ytimg.com/vi/BBd3aHnVnuE/mqdefault.jpg", 180, 320);
        //video.SetThumbnail(ThumbnailResolution.High, "https://i.ytimg.com/vi/BBd3aHnVnuE/hqdefault.jpg", 360, 480);
        //video.SetThumbnail(ThumbnailResolution.Standard, "https://i.ytimg.com/vi/BBd3aHnVnuE/sddefault.jpg", 480, 640);
        //video.SetThumbnail(ThumbnailResolution.MaxRes, "https://i.ytimg.com/vi/BBd3aHnVnuE/maxresdefault.jpg", 720, 1280);


        await VideoRepository.AddAsync(video);

        // Transcripts
        await CreateIfRealityIsNotDualTranscription(video.Id);

        // Artifacts
        await CreateIfRealityIsNotDualArtifacts(video.Id);

        return video;
    }

    private async Task CreateIfRealityIsNotDualTranscription(VideoId videoId)
    {
        // Transcripts
        var transcript = new Transcript(videoId, "US", new[]
        {
            "when you think of the",
            "staggering symbols that the Indians produced. I mean the the dancing Shiva for example.",
            "We've never produced anything as comprehensive as this.",
            "The dancing Shiva... There's this little bronze statues. It is the Shiva with four arms dancing with one foot raised.",
            "And well, I mean I'll go into the details: They are really quite extraordinary. It's said that the figure stands",
            "within a great circle - sort of halo which has flames going out on the symbols of flames and this is the",
            "circle of mass energy space-time. I mean this is the material world, the great world of the",
            "all-embracing",
            "Material world with its flames. Within this Shiva dances. He's called Nataraja, the Lord of the dance and",
            "he dances.",
            "He's everywhere in the universe. I mean they said this is his dance.",
            "Manifestation of the world is called his 'Lila', his play.",
        });

        await TranscriptRepository.AddAsync(transcript);
    }

    private async Task CreateIfRealityIsNotDualArtifacts(VideoId videoId)
    {
        var summaryArtifact = new Artifact(videoId, "Summary", "SUMMARY", "Summary generated by AI (#IfRealityVideo)");
        var contentsArtifact = new Artifact(videoId, "Contents", "CONTENTS_LIST", "Contents generated by AI (#IfRealityVideo)");

        await ArtifactRepository.AddRangeAsync(new[] { summaryArtifact, contentsArtifact });
    }
}
