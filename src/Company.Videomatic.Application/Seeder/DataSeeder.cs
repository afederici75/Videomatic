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
        var transcript = await Sender.Send(new UpdateTranscriptCommand(video.Id, "US", new TranscriptLinePayload[]
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
        var transcript = await Sender.Send(new UpdateTranscriptCommand(video.Id, "US", new TranscriptLinePayload[]
        {
                "Here's a question from Sandeep who lives in Peoria, Illinois. He asks,  ", 
                "If non-dual brahman is the ultimate reality, then why did the ancient rishis say  ", 
                "that Brahma, Vishnu, and Shiva are the gods of creation, sustenance, and dissolution?  ",
                "Questions like this one often arise when we look at conventional Hindu teachings from the  ",  
                "lofty standpoint of non-duality, advaita. The scriptures of advaita Vedanta accept  ", 
                "only one, undifferentiated underlying reality called brahman. But most other scriptures accept  ", 
                "the existence of many gods. These two positions are contradictory and seem irreconcilable.  ", 
                "But, this apparent conflict can be resolved with the help of an important principle  ", 
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