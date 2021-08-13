using System.Collections.Generic;
using System.Threading;

namespace Laba14_Extra2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            /*Создайте пул ресурсов видеоканалов (класс) которых изначально меньше чем клиентов
            (класс), которые хотят ими воспользоваться. Каждый клиент получает доступ к
            каналу, причем пользоваться можно только одним каналом. Если все каналы заняты,
            то клиент ждет заданное время и по его истечении уходит не получив услуги.
            используйте средства синхронизации - семафор*/
            
            //TODO ничего не работает. Проблема первая : не могу проследить,чтобы только один поток(клиент) работал с конкретным каналом в foreach.


            var ChannelPool = new ChannelPool(new Dictionary<string, ChannelStatus>
            {
                {"BBC", ChannelStatus.Free},
                {"RussiaToday", ChannelStatus.Free},
                {"Britain", ChannelStatus.Free}
            });

            var firstClient = new Client("A");
            var secondClient = new Client("B");
            var thirdClient = new Client("C");
            var fourthClient = new Client("D");

            var ClientsPool = new List<Client>
            {
                firstClient,
                secondClient,
                thirdClient,
                fourthClient
            };

            var pool = new Semaphore(3, 3, "ChannelsPool");
            var counter = 0;
            var maxcounter = 10;
            while (true)
            {
                if (counter == maxcounter) break;
                counter++;

                if (!firstClient.isWatchingChannel)
                    new Thread(firstClient.OccupyChannel).Start(ChannelPool);
                if (!secondClient.isWatchingChannel)
                    new Thread(secondClient.OccupyChannel).Start(ChannelPool);
                if (!thirdClient.isWatchingChannel)
                    new Thread(thirdClient.OccupyChannel).Start(ChannelPool);
                if (!fourthClient.isWatchingChannel)
                    new Thread(fourthClient.OccupyChannel).Start(ChannelPool);
                Thread.Sleep(1000);
            }
        }
    }
}