using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data
{
    public static class Seed
    {

        public static async Task SeedData(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.CatalogGenres.Any())
            {
                await context.AddRangeAsync(GetPreconfiguredCatalogGenre());
                await context.SaveChangesAsync();
            }

            if (!context.CatalogPlatforms.Any())
            {
                await context.AddRangeAsync(GetPreconfiguredCatalogPlatform());
                await context.SaveChangesAsync();
            }

            if (!context.CatalogPublishers.Any())
            {
                await context.AddRangeAsync(GetPreconfiguredCatalogPublisher());
                await context.SaveChangesAsync();
            }

            if (!context.CatalogGames.Any())
            {
                await context.AddRangeAsync(GetPreconfiguredCatalogGame());
                await context.SaveChangesAsync();
            }

            if (!context.CatalogGamePlatforms.Any())
            {
                await context.AddRangeAsync(GetPreconfiguredCatalogGamePlatform());
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<CatalogPlatform> GetPreconfiguredCatalogPlatform()
        {
            return new List<CatalogPlatform>()
            {
                new CatalogPlatform() { Platform = "PS4" },
                new CatalogPlatform() { Platform = "PS5" },
                new CatalogPlatform() { Platform = "Xbox" },
                new CatalogPlatform() { Platform = "Nintendo" },
            };
        }

        private static IEnumerable<CatalogGenre> GetPreconfiguredCatalogGenre()
        {
            return new List<CatalogGenre>()
            {
                new CatalogGenre() { Genre = "Horror" },
                new CatalogGenre() { Genre = "Adventure" },
                new CatalogGenre() { Genre = "Action" },
                new CatalogGenre() { Genre = "RPG" },
                new CatalogGenre() { Genre = "Simulation" },
                new CatalogGenre() { Genre = "shooter" },
            };
        }

        private static IEnumerable<CatalogPublisher> GetPreconfiguredCatalogPublisher()
        {
            return new List<CatalogPublisher>()
            {
                new CatalogPublisher() { Publisher = "Ubisoft" },
                new CatalogPublisher() { Publisher = "Activision" },
                new CatalogPublisher() { Publisher = "Blizzard Entertainment" },
                new CatalogPublisher() { Publisher = "Electronic Arts" },
            };
        }

        private static IEnumerable<CatalogGame> GetPreconfiguredCatalogGame()
        {
            return new List<CatalogGame>()
            {
                new CatalogGame() {
                    Title = "Assassin’s Creed Origins",
                    Description = "The events of the game develop simultaneously in the present and past. In 2017, the Templar Order, thanks to the Shroud of Eden obtained two years ago, is ready to recreate the Forerunner in order to gain control of the planet. Violet da Costa turned out to be a member of the Instruments of the First Will and all this time helped Juno to be revived. The Assassins, according to the Bishop, plan to take measures to return the Piece of Eden. The player controls Egyptian researcher Layla Hassan as she investigates the memory of the last Medjay, Bayek of Siwa and his wife Aya of Alexandria. Together they help Cleopatra in the civil war against her brother Ptolemy XIII and lay the foundations of the Brotherhood of Assassins.",
                    Year = 2017,
                    PublisherId = 7,
                    GenreId = 2,
                    InStock  = true,
                    Price = 20,
                    PictureFileName = "1.png",
                },
                new CatalogGame()
                {
                    Title = "Assassin’s Creed Valhalla",
                    Description = "The game begins in 873 during the Viking conquests. The player will take on the role of a Viking named Eivor, leading his relatives from the shores of cold Norway to the fertile lands of England in search of a new home. The Eivor clan is opposed by the leaders of four Anglo-Saxon kingdoms: Wessex, Northumbria, East Anglia and Mercia, led by Alfred the Great. Eivor will also have to meet the Unseen and help them in the fight against the Order of the Ancients[comm. 1]. As in the previous part of the series, the story of Leila Hassan continues in the 21st century[4]. In addition to England and Norway, the game also takes place in North America, called Vinland, and in fantasy worlds: Asgard, Jotunheim and Valhalla",
                    Year = 2020,
                    PublisherId = 7,
                    GenreId = 2,
                    InStock = true,
                    Price = 24,
                    PictureFileName = "2.png",
                },
                new CatalogGame()
                {
                    Title = "Far Cry 6",
                    Description = "In 1967, a revolution took place on Yar, as a result of which President Gabriel Castillo was overthrown. Because of this, Yara was isolated from the world for decades, and its economy was on the verge of collapse. In 2014, the son of the former president, Anton Castillo, won the election. He promises to create a paradise on earth in the country through the trade of Viviro, an experimental cancer treatment made from local tobacco. It, however, requires an extremely toxic fertilizer, which is bad for the health of workers.",
                    Year = 2021,
                    PublisherId = 7,
                    GenreId = 3,
                    InStock = true,
                    Price = 30,
                    PictureFileName = "3.png",
                },
                new CatalogGame()
                {
                    Title = "Call of Duty 4: Modern Warfare",
                    Description = "The plot of the game is connected with military operations at the beginning of the 21st century. The single-player campaign gives the player the opportunity to play as members of the US Marine Corps, the British Special Air Service and regular units. The game takes place in Azerbaijan, Ukraine and the Middle East.",
                    Year = 2007,
                    PublisherId = 8,
                    GenreId = 6,
                    InStock = true,
                    Price = 19,
                    PictureFileName = "4.png",

                },
                new CatalogGame()
                {
                    Title = "Call of Duty: Modern Warfare II (2022)",
                    Year = 2022,
                    PublisherId = 8,
                    GenreId = 6,
                    InStock = true,
                    Price = 29,
                    PictureFileName = "5.png",
                },
                new CatalogGame()
                {
                    Title = "Crash: Mind over Mutant",
                    Description = "The events of Crash: Mind over Mutant games take place a year after Crash of the Titans. The main character of the game, Crash Bandicoot, learns that Dr. Neo Cortex, the villain of the Crash Bandicoot series of games, once again intends to take over the world. To carry out their plan, the doctor and his friend, N. Brio, are going to use a new invention - envy, which (which is a parody of the iPhone[3] and BlackBerry[4] devices) allows you to control the minds of mutants. Thus, two scientists form an army of aggressive creatures. Crash and Aku-Aku find themselves alone outside the device's power and must now free their friends and put an end to Doctor Cortex.",
                    Year = 2008,
                    GenreId = 2,
                    PublisherId = 8,
                    InStock  = true,
                    Price = 18,
                    PictureFileName = "6.png",
                },
                new CatalogGame()
                {
                    Title = "Diablo IV",
                    Description = "Diablo IV takes place 50[2] years after the events of the third game, after millions of people have been killed by the actions of both High Heaven and the Burning Hell. In the vacuum of power, a legendary name emerges: Lilith, the ancestor of humanity, daughter of Mephisto, the succubi queen Lilith, who first appeared in Diablo II[3][4]. Her grip on Sanctuary cuts deep into the hearts of men and women, cultivating the worst in its inhabitants and leaving the world in a dark, hopeless place.",
                    Year = 2023,
                    PublisherId = 9,
                    GenreId = 4,
                    InStock = true,
                    Price = 35,
                    PictureFileName = "7.png",
                },
                new CatalogGame()
                {
                    Title = "Overwatch",
                    Year = 2016,
                    PublisherId = 9,
                    GenreId = 6,
                    InStock  = true,
                    Price = 21,
                    PictureFileName = "8.png",
                },
                new CatalogGame()
                {
                    Title = "Battlefield 4",
                    Year = 2013,
                    PublisherId = 10,
                    GenreId = 6,
                    InStock = true,
                    Price = 19,
                    PictureFileName = "9.png",
                },
                new CatalogGame()
                {
                    Title = "EA SPORTS FC 24",
                    Description = "EA SPORTS FC 24 is the next chapter in a more innovative future for football. A new part of the football simulator EA SPORTS FC (formerly called FIFA). The version on the switch is now a full-fledged part. Previously, it was released with the Legacy Edition prefix, in which the composition was only updated. There were no innovations like on other platforms.",
                    Year = 2023,
                    GenreId = 5,
                    PublisherId = 10,
                    InStock = true,
                    Price = 30,
                    PictureFileName = "10.png",
                },

            };
        }

        public static IEnumerable<CatalogGamePlatform> GetPreconfiguredCatalogGamePlatform()
        {
            return new List<CatalogGamePlatform>()
            {
                new CatalogGamePlatform() {CatalogGameId = 1, CatalogPlatformId = 1 },
                new CatalogGamePlatform() {CatalogGameId = 1, CatalogPlatformId = 3 },
                new CatalogGamePlatform() {CatalogGameId = 2, CatalogPlatformId = 1 },
                new CatalogGamePlatform() {CatalogGameId = 2, CatalogPlatformId = 2 },
                new CatalogGamePlatform() {CatalogGameId = 2, CatalogPlatformId = 3 },
                new CatalogGamePlatform() {CatalogGameId = 3, CatalogPlatformId = 1 },
                new CatalogGamePlatform() {CatalogGameId = 3, CatalogPlatformId = 3 },
                new CatalogGamePlatform() {CatalogGameId = 4 , CatalogPlatformId = 3 },
                new CatalogGamePlatform() {CatalogGameId = 4 , CatalogPlatformId = 4 },
                new CatalogGamePlatform() {CatalogGameId = 5 , CatalogPlatformId = 1 },
                new CatalogGamePlatform() {CatalogGameId = 5 , CatalogPlatformId = 2 },
                new CatalogGamePlatform() {CatalogGameId = 5 , CatalogPlatformId = 3 },
                new CatalogGamePlatform() {CatalogGameId = 6 , CatalogPlatformId = 4 },
                new CatalogGamePlatform() {CatalogGameId = 7 , CatalogPlatformId = 1 },
                new CatalogGamePlatform() {CatalogGameId = 7 , CatalogPlatformId = 2 },
                new CatalogGamePlatform() {CatalogGameId = 7 , CatalogPlatformId = 3 },
                new CatalogGamePlatform() {CatalogGameId = 8 , CatalogPlatformId = 1 },
                new CatalogGamePlatform() {CatalogGameId = 8 , CatalogPlatformId = 3 },
                new CatalogGamePlatform() {CatalogGameId = 8 , CatalogPlatformId = 4 },
                new CatalogGamePlatform() {CatalogGameId = 9 , CatalogPlatformId = 1 },
                new CatalogGamePlatform() {CatalogGameId = 9 , CatalogPlatformId = 3 },
                new CatalogGamePlatform() {CatalogGameId = 10 , CatalogPlatformId = 1 },
                new CatalogGamePlatform() {CatalogGameId = 10 , CatalogPlatformId = 2 },
                new CatalogGamePlatform() {CatalogGameId = 10 , CatalogPlatformId = 3 },
                new CatalogGamePlatform() {CatalogGameId = 10 , CatalogPlatformId = 4 },
            };
        }
    }
}
