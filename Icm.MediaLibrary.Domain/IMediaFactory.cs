﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icm.MediaLibrary.Domain
{
    public interface IMediaFactory
    {
        Media BuildFromFile(string filePath);
    }
}
