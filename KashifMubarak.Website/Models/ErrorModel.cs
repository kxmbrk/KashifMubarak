﻿using System;

namespace KashifMubarak.Website.Models
{
    public class ErrorModel
    {
        public int HttpStatusCode { get; set; }
        public Exception Exception { get; set; }
    }
}