﻿using Android.Content;
using Android.Nfc;
using MvvmCross.Platform.Core;

namespace DevelopingTrends.MvxPlugins.NFC.Droid
{
    public abstract class ReadBase : DroidBase
    {             
        protected override void NewIntent(MvxValueEventArgs<Intent> e)
        {
            string id=GetIdFromTag(e.Value);
            var tagAsNdefMessage = e.Value.GetParcelableArrayExtra(NfcAdapter.ExtraNdefMessages);
            if (tagAsNdefMessage != null)
            {

                var tag = tagAsNdefMessage[0] as NdefMessage;
                byte[] message = tag.ToByteArray();
                var ndefMessage = NdefLibrary.Ndef.NdefMessage.FromByteArray(message);
                NewMessage(id,ndefMessage);
            }
        }


        protected abstract void NewMessage(string tagID,NdefLibrary.Ndef.NdefMessage ndefMessage);
    }
}