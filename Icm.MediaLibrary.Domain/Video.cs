﻿using System;

namespace Icm.MediaLibrary.Domain
{
    public class Video : Media
    {
        protected Video()
        {
        }

        public Video(string hash, string filePath, long size, TimeSpan duration, int width, int height) : base(hash, filePath, size)
        {
            this.Duration = duration;
        }

        public TimeSpan Duration { get; protected set; }
    }
}
