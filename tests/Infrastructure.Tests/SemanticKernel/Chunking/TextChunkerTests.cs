using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Text;

namespace Infrastructure.Tests.SemanticKernel;

[Collection("DbContextTests")]
public class TextChunkerTests : IClassFixture<DbContextFixture>
{
    public TextChunkerTests(
        ITestOutputHelper output,
        ISemanticTextMemory textMemory)
    {
        Output = output ?? throw new ArgumentNullException(nameof(output));
        TextMemory = textMemory ?? throw new ArgumentNullException(nameof(textMemory));
    }

    public ITestOutputHelper Output { get; }
    public ISemanticTextMemory TextMemory { get; }

    const string AldousHuxley_DancingShiva2 = "when you think of the staggering symbols that the Indians produced I mean they the dancing Shiva for example we've never produced anything as comprehensive as this the dancing Shiva there was a little bronze statues it is the the Shiva with four arms dancing with one foot raised and well I mean I'll go into the details they're really quite extraordinary it's the figure stands within a great circle sort of halo which has flames going out on the symbols of flames and this is the circle of mass energy space-time I mean this is the material world this is a great world of all-embracing material world with its flames within this shiva dances and he's called Nataraja the lord of the dance and he dances he's everywhere in the universe I mean this is his dancer the manifestation of the world his court his Leela his play so I mean he sends his reign upon the just and the unjust and he's not beyond good and evil of course it's all an immense manifestation of play he said he has this long hair which is the hair of the yogi contemplative and it streams out to the limits of the universe really therefore he this sort of yogic knowledge of this contemplation includes everything he has four arms in the upper right arm he holds a little drum which is the drum which summons things into creation who beat upon this drum things come into existence in his left arm he holds a fire which is what destroys everything he both creates and destroys his lower right hand is held up in this attitude which means be not afraid in spite of everything it is all right the other hand points down at his feet and one foot is planted squarely on the back of a repulsive and dwarf this infinitely powerful war called mooyah luck I think good name is who is the heir Diego and he has to break the back of the a cozy the what he's really pointing at is the other foot which is raised and this means this foot is raised against gravitation and is the symbol of spiritual contemplation whole thing is there you see I mean the the world of space and time matter and energy the world of creation and destruction in the world of psychology I mean how do you get out of this I mean you don't break the back of the ego you're lost and if you don't have practice contemplation there will be no liberation for you I mean it did we don't have any approach in such a comprehensive symbol which is both cosmic and psychological and spiritual it is really a most unfortunate that we have such miserable symbols it's part of the regular Hinduism but it is specifically Shiva and then one of the manifestations because it's called by raava of fever where who is also dancing but he dances in cemeteries and I mean to remind us that the dance of life isn't always very jolly I mean that he dances just as much and in misery and death as in life in relation and this has to be accepted no cause again it's only by the lifted for that we can accept it I mean it's actually isn't completely compatible with modern scientific idea I mean it includes world you see of mass energy space and time and the idea of the the infinite energy dancing timeless being forever through this world dancing through human mentality - I mean the world is felt to be of course a kind of outrage because the clay goes on even inside ourselves although we are sentient beings and yet Henri's raised everything is finally alright in spite of everything if as Buddha says I show you sorrow in the end I'm sorry the ending of sorrow is putting effort on the back of the dwarf and raising the for the other putting against gravity into the state of contemplation I mean the whole thing is there as stated in this single extremely elegant I mean the these Shiva images from the south of India beautiful pieces of sculpture the best on but it's a shame we don't have any good symbols like this to remind us of who we are and what we can do about it finding now we're very very poor in it I mean we have some of the Christians it doesn't take into account this on cosmic side of life I mean doesn't take into account mass energy space and time is essentially doesn't take into account I mean as it stands it doesn't take into account the importance of contemplation know it survive I mean we there are other symbols of course within Christianity which do but a single comprehensive say a symbol like the Shiva symbol we do not have and it's very unfortunate this whole business of the organised manipulation of symbols is I mean the human mind is the symbolic instrument immediate exists to manufacture symbols to turn immediate experience into symbols for the purpose of managing it whether in a very convenient way the question is can we get on with early scientific symbols realistic symbols and then concentrate on the immediate experience I don't know I mean I simply don't know whether that this is a possible as a sort of general attitude towards the world I think it's certainly possible in isolated individuals but whether in fact will ever turn out to be something which appeals to great numbers of people have no idea you\r\n";

    [Fact]
    public async Task SemanticSearchOnMemoryStore()
    {        
        var text = AldousHuxley_DancingShiva2; // Lonbg text

        // The following splits nicely!
        // TODO: tweak so the fragments overlap a little bit
        List<string> chunks = TextChunker.SplitPlainTextLines(text, 200);        

        chunks.Should().NotBeEmpty();     
        chunks.Count.Should().Be(8);

        var collName = "Videos";
        var idx = 0;
        foreach (var chunk in chunks)
        {
            var chunkId = (idx++).ToString();

            // Generates and saves the embedding
            await TextMemory.SaveInformationAsync(
                collection: collName,
                text: chunk,
                id: chunkId,
                description: $"Chunk '{chunkId}''s description",                
                additionalMetadata: "metaForEmbedding");

            // Generates and saves the reference to the embedding
            await TextMemory.SaveReferenceAsync(
                collection: collName,
                text: chunk, 
                externalId: "O1pD3Ew_yu8",
                externalSourceName: "YouTube",
                description: $"Some text from Aldous Huxley's video/chunk{chunkId}",
                additionalMetadata: "metaForReference"
                );  
        }

        var q = "Shiva Aldous gravity";
        var expectedResults = 4;
        var cnt = 0;
        await foreach (MemoryQueryResult res in TextMemory.SearchAsync(
            collection: collName, 
            query: q,
            limit: expectedResults,
            minRelevanceScore: 0.7,
            withEmbeddings: false))
        {
            Output.WriteLine($"[{res.Relevance}] {res.Metadata.Text} ({res.Metadata.ExternalSourceName})");
            cnt++;
        }

        Assert.True(cnt==expectedResults, "Should have more results.");
    }
}