using System.Collections.Generic;

namespace Laba14_Extra2
{
    public class ChannelPool
    {
        public ChannelPool(Dictionary<string, ChannelStatus> channels)
        {
            Channels = channels;
        }

        public Dictionary<string, ChannelStatus> Channels { get; }

        public void ChangeChannelStatus(string channel, ChannelStatus status)
        {
            Channels[channel] = status;
        }
    }
}