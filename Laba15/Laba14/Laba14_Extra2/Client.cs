using System;
using System.Threading;

namespace Laba14_Extra2
{
    public class Client
    {
        private readonly string name;
        public bool isWatchingChannel;

        public Client(string name)
        {
            this.name = name;
            isWatchingChannel = false;
        }

        public void OccupyChannel(object obj)
        {
            //TODO можно таймер потом прикрутить
            var channelPool = obj as ChannelPool;
           var mutex = new Mutex();
            var channels = channelPool.Channels;
            foreach (var currentChannel in channels)
            {
                mutex.WaitOne();
                if (currentChannel.Value == ChannelStatus.Free)
                {
                    Console.WriteLine($"{name} has occupied {currentChannel.Key} channel");
                    channelPool.ChangeChannelStatus(currentChannel.Key, ChannelStatus.Occupied);
                    isWatchingChannel = true;
                    mutex.ReleaseMutex();

                    Thread.Sleep(5000);
                    Console.WriteLine($"{name} has released {currentChannel.Key} channel");
                    channelPool.ChangeChannelStatus(currentChannel.Key, ChannelStatus.Free);
                    isWatchingChannel = false;
                    break;
                }
                mutex.ReleaseMutex();
            }
        }
    }
}