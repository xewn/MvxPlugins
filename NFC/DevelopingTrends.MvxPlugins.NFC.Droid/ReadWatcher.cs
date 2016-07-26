using System;
using Android.Content;
using MvvmCross.Platform.Core;
using MvvmCross.Plugins.Messenger;

namespace DevelopingTrends.MvxPlugins.NFC.Droid
{
    internal class ReadWatcher : ReadBase, IWatcher
    {
        private bool _currentlyScanning = false;
        private readonly IMvxMessenger _messenger;
        private readonly object _lock = new object();

        public ReadWatcher(IMvxMessenger messenger)
        {
            _messenger = messenger;
        }


        public bool Start()
        {
            if (!IsSupported)
            {
                if (_dontThrowExpceptionWhenNotSupported)
                {
                    return false;
                }
                throw new NotSupportedException("This device does not support NFC (or perhaps it's disabled)");
            }                  

            StartForegroundMonitoring();

            //attach events
            AttachEvents();            

            return true;
        }
             


        public void Stop()
        {
            lock (_lock)
            {
               
                if (IsSupported)
                {
                    
                    StopForegroundDispatch();
                    DetachEvents();
                    

                }
            }
        }

        

        protected override void NewMessage(string tagId,NdefLibrary.Ndef.NdefMessage message)
        {
            
            var nfcMessage = new MessageReceived(tagId, message, this);
            _messenger.Publish(nfcMessage);
        }

        protected override void NewIntent(MvxValueEventArgs<Intent> e)
        {
            throw new NotImplementedException();
        }
    }
}