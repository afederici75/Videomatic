﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Videomatic.Drivers.SqlServer;

public static class DbConstants
{
    public const string SequenceName = "IdSequence";

    public static class FieldLengths
    {     
        public const int Url = 1024;
        public const int ProviderId = 20;
        public const int FolderName = 120;
        public const int Title = 200;
        public const int Description = 2048;
        public const int TranscriptLineText = 100;
    }
}
