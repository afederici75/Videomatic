using Company.Videomatic.Application.Features.Videos.Commands;

namespace Company.Videomatic.Infrastructure.Data.Seeder;

public interface IDataSeeder
{
    public Task CreateData();
}

public class DataSeeder : IDataSeeder
{
    public DataSeeder(ISender sender)
    {
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }

    public ISender Sender { get; }

    public async Task CreateData()
    { 
        await CreateEasternPhilosophyPlaylist();
        await CreateBurningManPlaylist();
    }

    async Task<long> CreateEasternPhilosophyPlaylist()
    {
        var playlist = await Sender.Send(new CreatePlaylistCommand("Eastern Philosophy", "Videos about Eastern Philosophy"));

        long videoId1 = await CreateAldousHuxleyTheDancingShivaVideo();
        long videoId2 = await CreateIfRealityIsNonDualVideo();

        var resp = await Sender.Send(new LinkVideoToPlaylistsCommand(playlist.Id, new[] { videoId1, videoId2 }));

        return playlist.Id;
    }

    async Task<long> CreateBurningManPlaylist()
    {
        var resp = await Sender.Send(new CreatePlaylistCommand("Burning Man", "Videos about Burning Man"));
        return resp.Id;
    }

    async Task<long> CreateAldousHuxleyTheDancingShivaVideo()
    {
        // Video
        var video = await Sender.Send(
            CreateVideoCommandBuilder.WithEmptyVideoDetails(
                location: "https://www.youtube.com/watch?v=n1kmKpjk_8E", 
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
                "poster."));
        
        // Tags
        var tagsResp = await Sender.Send(new AddTagsToVideoCommand(video.Id, new[] { "HINDUISM", "HUXLEY" }));
        
        // Thumbnails
        var thumbs = await Sender.Send(
            new AssignVideoThumnbailsCommand(video.Id, 
            new ThumbnailInfo[]
            {
                new ("https://i.ytimg.com/vi/n1kmKpjk_8E/default.jpg", ThumbnailResolutionDTO.Default, 90, 120),
                new ("https://i.ytimg.com/vi/n1kmKpjk_8E/mqdefault.jpg", ThumbnailResolutionDTO.Medium, 180, 320),
                new ("https://i.ytimg.com/vi/n1kmKpjk_8E/hqdefault.jpg", ThumbnailResolutionDTO.High, 360, 480),
                new ("https://i.ytimg.com/vi/n1kmKpjk_8E/sddefault.jpg", ThumbnailResolutionDTO.Standard, 480, 640),
                new ("https://i.ytimg.com/vi/n1kmKpjk_8E/maxresdefault.jpg", ThumbnailResolutionDTO.MaxRes, 720, 1280)
            }));

        // Transcripts
        var transcript = await Sender.Send(new AddTranscriptsToVideoCommand(video.Id, new []
        {
            new TranscriptPayload("US", new TranscriptLinePayload[]
            {
                new ("when you think of the", new (0,0,0,0, 669), new (0, 0, 0, 0, 0)),
                new ("staggering symbols that the Indians produced. I mean the the dancing Shiva for example.", new (0,0,0,2,8599998), new (0,0,0,5,7199997)),
                new ("We've never produced anything as comprehensive as this.", new (0,0,0,8,5799999), new (0,0,0,3,7000000)),
                new ("The dancing Shiva... There's this little bronze statues. It is the Shiva with four arms dancing with one foot raised.", new (0,0,0,7,2600002), new (0,0,0,12,7399997)),
                new ("And well, I mean I'll go into the details: They are really quite extraordinary. It's said that the figure stands", new (0,0,0,20,7999992), new (0,0,0,05,6599998)),
                new ("within a great circle - sort of halo which has flames going out on the symbols of flames and this is the", new (0,0,0,27,3390007), new (0,0,0,06,7399997)),
                new ("circle of mass energy space-time. I mean this is the material world, the great world of the", new (0,0,0,34,7500000), new (0,0,0,06,8889999)),
                new ("all-embracing", new (0,0,0,42,9399986), new (0,0,0,1,0490000)),
                new ("Material world with its flames. Within this Shiva dances. He's called Nataraja, the Lord of the dance and", new (0,0,0,43,9889984), new (0,0,0,6,9790000)),
                new ("he dances.", new (0,0,0,52,0299987), new (0,0,0,01,7990000)),
                new ("He's everywhere in the universe. I mean they said this is his dance.", new (0,0,0,53,8289985), new (0,0,0,3,9790000)),
                new ("Manifestation of the world is called his 'Lila', his play.", new (0,0,0,58,6290016), new (0,0,0,2,4200000)),
            })
        }));

        // Artifacts
        var summaryArtifact = await Sender.Send(new AddArtifactToVideoCommand(video.Id,
            "Summary",
            "SUMMARY",
            "This will be an AI generated artifact"
            ));

        var contentsArtifact = await Sender.Send(new AddArtifactToVideoCommand(video.Id,
            "Contents",
            "CONTENTS_LIST",
            "This will be an AI generated artifact listing all the topics discussed in this video"
            ));

        return video.Id;
    }

    async Task<long> CreateIfRealityIsNonDualVideo()
    {
        // Video
        var video = await Sender.Send(
            CreateVideoCommandBuilder.WithEmptyVideoDetails(
                location: "https://www.youtube.com/watch?v=BBd3aHnVnuE",
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
                "For more information, please see: https://www.arshabodha.org/"));

        // Tags
        var tagsResp = await Sender.Send(new AddTagsToVideoCommand(video.Id, new[] { "HINDUISM" }));

        // Thumbnails
        var thumbs = await Sender.Send(
            new AssignVideoThumnbailsCommand(video.Id,
            new ThumbnailInfo[]
            {
                new ("https://i.ytimg.com/vi/BBd3aHnVnuE/default.jpg", ThumbnailResolutionDTO.Default, 90, 120),
                new ("https://i.ytimg.com/vi/BBd3aHnVnuE/mqdefault.jpg", ThumbnailResolutionDTO.Medium, 180, 320),
                new ("https://i.ytimg.com/vi/BBd3aHnVnuE/hqdefault.jpg", ThumbnailResolutionDTO.High, 360, 480),
                new ("https://i.ytimg.com/vi/BBd3aHnVnuE/sddefault.jpg", ThumbnailResolutionDTO.Standard, 480, 640),
                new ("https://i.ytimg.com/vi/BBd3aHnVnuE/maxresdefault.jpg", ThumbnailResolutionDTO.MaxRes, 720, 1280)
            }));

        // Transcripts
        var transcript = await Sender.Send(new AddTranscriptsToVideoCommand(video.Id, new[]
        {
            new TranscriptPayload("US", new TranscriptLinePayload[]
            {
                new TranscriptLinePayload("Here's a question from Sandeep who lives in Peoria, Illinois. He asks,  ", new TimeSpan(0,0,0,21,4400005), new (0,0,0,6,7199997)),
                new TranscriptLinePayload("If non-dual brahman is the ultimate reality, then why did the ancient rishis say  ", new TimeSpan(0,0,0,28,7199993), new (0,0,0,7,6799998)),
                new TranscriptLinePayload("that Brahma, Vishnu, and Shiva are the gods of creation, sustenance, and dissolution?  ", new TimeSpan(0,0,0,36,9599990), new (0,0,0,8,0799999)),
                new TranscriptLinePayload("Questions like this one often arise when we look at conventional Hindu teachings from the  ",  new TimeSpan(0,0,0,47,4399986), new (0,0,0,6,8000001)),
                new TranscriptLinePayload("lofty standpoint of non-duality, advaita. The scriptures of advaita Vedanta accept  ", new TimeSpan(0,0,0,54,2400016), new (0,0,0,8,3199996)),
                new TranscriptLinePayload("only one, undifferentiated underlying reality called brahman. But most other scriptures accept  ", new TimeSpan(0,0,1,2,5600013), new (0,0,0,10,0)),
                new TranscriptLinePayload("the existence of many gods. These two positions are contradictory and seem irreconcilable.  ", new TimeSpan(0,0,1,2,5600013), new (0,0,0,10,0)),
                new TranscriptLinePayload("But, this apparent conflict can be resolved with the help of an important principle  ", new TimeSpan(0,0,1,22,6399993), new (0,0,0,8,0799999)),                
            })
        }));

        // Artifacts
        var summaryArtifact = await Sender.Send(new AddArtifactToVideoCommand(video.Id,
            "Summary",
            "SUMMARY",
            "This will be another AI generated artifact"
            ));

        var contentsArtifact = await Sender.Send(new AddArtifactToVideoCommand(video.Id,
            "Contents",
            "CONTENTS_LIST",
            "This will be another AI generated artifact listing all the topics discussed in this video"
            ));

        return video.Id;
    }
}