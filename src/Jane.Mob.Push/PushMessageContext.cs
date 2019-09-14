using Jane.Push;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jane.Mob.Push
{
    public class PushMessageContext
    {
        public string GroupKey { get; set; }

        public PushMessage Message { get; set; }

        public List<PushPlatform> PushPlatforms { get; set; }

        public TaskCompletionSource<PushMessageOutput> TaskCompletionSource { get; set; }
    }
}