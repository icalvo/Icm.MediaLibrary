﻿using Microsoft.AspNet.SignalR;

namespace Icm.MediaLibrary.Web.Hubs
{
    public class MediaWatcherHub : Hub
    {
        public void StartWatching()
        {
            Clients.All.addCreatedFile("asdf");
            Clients.All.addCreatedFile("qwer");
        }
    }
}