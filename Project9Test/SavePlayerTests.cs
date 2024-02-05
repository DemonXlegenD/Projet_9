using NSave;

namespace NSavePlayerTests
{
    public class SavePlayerTests
    {
        private static SavePlayer savePlayer;
        [SetUp]
        public static void Setup()
        {
            savePlayer = SavePlayer.GetInstance();
        }

        [Test]
        public static void SavePlayersTestSuccess()
        {
        }
    }
}

