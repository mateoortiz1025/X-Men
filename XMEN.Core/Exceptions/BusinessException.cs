﻿using System;

namespace XMEN.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException()
        {

        }
        public BusinessException(string message) : base(message) { }
    }
}