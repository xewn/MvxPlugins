using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;

namespace DevelopingTrends.MvxPlugins.NFC.Droid
{
    public class Plugin : IMvxPlugin
    {

        public void Load()
        {
            Mvx.RegisterType<IWatcher, ReadWatcher>();
            Mvx.RegisterType<IReadTask, ReadTask>();
            Mvx.RegisterType<IWriteTask,MvxNFCWriteTask>();
        }
    }
}